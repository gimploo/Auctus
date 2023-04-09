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

    private string val = "";
    private int top = -1;
    private Vector3 lastPos = default;

    List<GameObject> data = new List<GameObject>();
    List<GameObject> arrows = new List<GameObject>();

    private GameObject lastSelectedNode = null;

    public void updateInputText()
    {
        val = cinputText.text;
    }

    private void Awake()
    {
        lastPos = AuctusBaseConfig.Instance.placementPose.position + new Vector3(0.0f, prefab.transform.localScale.y, 0.0f);
    }
    private GameObject getReferenceToGameObjectFromTouch()
    {
        //TODO: raycast code
        return null;
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
                        lastSelectedNode = obj;
                        obj.GetComponent<Renderer>().material.color = Color.blue;
                    } 
                }
            }
        }
    }

    public void insertion()
    {
        if (val == "" || val == " " ) return;

        top = top + 1;
        GameObject obj1 = Instantiate(
            prefab, 
            lastPos,
            Quaternion.identity
        );

        // sets the text
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

        //clears input field
        cinputText.text = "";

        val = "";
    }

    public void deletion()
    {
        if (top == -1) return;
        lastPos = lastPos - new Vector3(arrowprefab.transform.localScale.x, 0.0f, 0.0f) - new Vector3(prefab.transform.localScale.x, 0.0f, 0.0f);
        Destroy(data[top]);
        Destroy(arrows[top]);
        data.RemoveAt(top);
        arrows.RemoveAt(top);
        top = top - 1;
    }
}
