using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;

public class LinkedList : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] GameObject arrowprefab;
    [SerializeField] TMP_InputField cinputText;
    [SerializeField] GameObject memoryLayout;

    private string val = "";
    private int top = -1;
    private Vector3 lastPos = default;

    List<GameObject> data = new List<GameObject>();
    List<GameObject> arrows = new List<GameObject>();

    private int MAXCOLS = 9;
    private int MAXROWS = 5;

    private GameObject lastSelectedNode = null;

    public void updateInputText()
    {
        val = cinputText.text;
    }

    void Start()
    {
        lastPos = AuctusBaseConfig.Instance.placementPose.position + new Vector3(0.0f, prefab.transform.localScale.y, 0.0f);
        memoryLayout.SetActive(false);
    }

    private int getIndexOfGameObjectFromList(GameObject target)
    {
        for (int i = 0; i < data.Count; i++)
            if (target == data[i])
                return i;

        Debug.Assert(false, "Invalid target ");
        return -1;
    }

    public void reset()
    {
        foreach(GameObject obj in arrows)
            Destroy(obj);
        foreach(GameObject obj in data)
            Destroy(obj);

        lastPos = AuctusBaseConfig.Instance.placementPose.position + new Vector3(0.0f, prefab.transform.localScale.y, 0.0f);
        top = -1;
        val = "";
    }

    void Update()
    {
        if (top == -1) {
            Start();
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began) {

                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hitObject;
                if (Physics.Raycast(ray, out hitObject)) {

                    if (lastSelectedNode != null) {
                        lastSelectedNode.GetComponent<Renderer>().material.color = prefab.GetComponent<Renderer>().material.color;
                    }

                    GameObject obj = hitObject.transform.gameObject;
                    if (obj.tag == "node") {
                        if (obj == lastSelectedNode) {
                            lastSelectedNode.GetComponent<Renderer>().material.color = prefab.GetComponent<Renderer>().material.color;
                            lastSelectedNode = null;
                            return;
                        }
                        lastSelectedNode = obj;
                        obj.GetComponent<Renderer>().material.color = Color.blue;
                    } 
                }
            }
        }
    }

    public void UpdateMemoryLayout()
    {
        //TODO: find a better way to do this
        if (data.Count == 0) return; 

        GameObject[] rows = {
            memoryLayout.transform.GetChild(0).gameObject,
            memoryLayout.transform.GetChild(1).gameObject,
            memoryLayout.transform.GetChild(2).gameObject,
            memoryLayout.transform.GetChild(3).gameObject,
            memoryLayout.transform.GetChild(4).gameObject,
        };

        // foreach(GameObject row in rows)
        // {
        //     for (int i = 0; i < MAXCOLS; i++)
        //     {
        //         GameObject button = row.transform.GetChild(i).gameObject;
        //         button.GetComponent<Renderer>().material.color = Color.black;
        //         button.GetComponentInChildren<TMP_Text>().text = "0";
        //     }
        // }

        int row_index = 0;
        int col_index = 0;

        foreach(GameObject obj in data)
        {
            GameObject button = rows[row_index].transform.GetChild(col_index).gameObject;
            button.GetComponentInChildren<TMP_Text>().text = 
                obj.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponentInChildren<TMP_Text>().text;
            button.GetComponent<Image>().color = Color.blue;

            row_index++;
            col_index++;

            if (row_index >= MAXROWS) row_index = 0;
            if (col_index >= MAXCOLS) col_index = 0;
        }
    }

    private void insert_at_index(int index)
    {
        GameObject obj1 = Instantiate(
            prefab, 
            data[index].transform.position,
            Quaternion.identity
        );
        obj1.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponentInChildren<TMP_Text>().text = val;
        GameObject obj2 = Instantiate(
            arrowprefab,
            arrows[index].transform.position,
            Quaternion.identity
        );

        for (int i = index; i < (data.Count - 1); i++)
        {
            data[i].transform.position = data[i+1].transform.position;
            arrows[i].transform.position = arrows[i+1].transform.position;
        }

        data.Insert(
            index,
            obj1
        );

        arrows.Insert(
            index,
            obj2
        );

        Vector3 dx = new Vector3(prefab.transform.localScale.x + arrowprefab.transform.localScale.x, 0.0f, 0.0f);
        data[data.Count - 1].transform.position += dx;
        arrows[arrows.Count - 1].transform.position += dx;

        lastPos = lastPos + dx;
        top = top + 1;
        cinputText.text = "";
        val = "";
    }

    public void insertion()
    {
        if (val == "" || val == " " ) return;

        if (lastSelectedNode != null) {
            int index = getIndexOfGameObjectFromList(lastSelectedNode);
            insert_at_index(index);
            return;
        }

        if (arrows.Count != 0) {
            arrows[arrows.Count-1].SetActive(true);
        }

        top = top + 1;
        GameObject obj1 = Instantiate(
            prefab, 
            lastPos,
            Quaternion.identity
        );
        obj1.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponentInChildren<TMP_Text>().text = val;
        data.Insert(
            top,
            obj1
        );
        lastPos = lastPos + new Vector3(obj1.transform.localScale.x, 0.0f, 0.0f);

        GameObject obj2 = Instantiate(
            arrowprefab, 
            lastPos,
            Quaternion.identity
        );
        arrows.Insert(
            top,
            obj2
        );
        lastPos = lastPos + new Vector3(obj2.transform.localScale.x, 0.0f, 0.0f);

        obj2.SetActive(false);
        cinputText.text = "";
        val = "";

    }

    public void deletion()
    {
        if (top == -1) return;
        if (lastSelectedNode != null) {
            int index = getIndexOfGameObjectFromList(lastSelectedNode);
            delete_at_index(index);
            return;
        }
        lastPos = lastPos - new Vector3(arrowprefab.transform.localScale.x, 0.0f, 0.0f) - new Vector3(prefab.transform.localScale.x, 0.0f, 0.0f);
        Destroy(data[top]);
        Destroy(arrows[top]);
        data.RemoveAt(top);
        arrows.RemoveAt(top);

        arrows[arrows.Count - 1].SetActive(false);
        top = top - 1;
    }

    private void delete_at_index(int index)
    {
        lastPos = lastPos - new Vector3(arrowprefab.transform.localScale.x, 0.0f, 0.0f) - new Vector3(prefab.transform.localScale.x, 0.0f, 0.0f);
        for (int i = index; i < (data.Count - 1); i++)
        {
            data[i+1].transform.position = data[i].transform.position;
            arrows[i+1].transform.position = arrows[i].transform.position;
        }
        Destroy(data[index]);
        Destroy(arrows[index]);
        data.RemoveAt(index);
        arrows.RemoveAt(index);

        arrows[arrows.Count - 1].SetActive(false);
        top = top - 1;
    }
}
