using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;
using TMPro;

public class PriorityQueue : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] GameObject PopUp;
    [SerializeField] TMP_InputField cinputText;
    [SerializeField] TMP_InputField prioInputText;

    private string val = "";
    private string prio = "";
    private int top = -1;
    private Vector3 lastPos = default;

    List<GameObject> data = new List<GameObject>();

    public void updateInputText()
    {
        val = cinputText.text;
    }

    public void updatePrioInputText()
    {
        prio = prioInputText.text;
    }

    void Start()
    {
        lastPos = AuctusBaseConfig.Instance.placementPose.position;
    }

    private void SortDataBasedOnPrio() 
    {
        var n = data.Count;
        for (int i = 0; i < n - 1; i++)
            for (int j = 0; j < n - i - 1; j++)
            {
                int prio1 = int.Parse(data[j].transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.GetComponentInChildren<TMP_Text>().text);
                int prio2 = int.Parse(data[j+1].transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.GetComponentInChildren<TMP_Text>().text);
                if (prio1 > prio2)
                {
                    // var tempVar = NumArray[j];
                    // NumArray[j] = NumArray[j + 1];
                    // NumArray[j + 1] = tempVar;

                    Vector3 tmp = data[j].transform.position;
                    data[j].transform.position = data[j+1].transform.position;
                    data[j+1].transform.position = tmp;

                    GameObject obj = data[j];
                    data[j] = data[j+1];
                    data[j+1] = obj;
                }

            }
    }

    private bool IsDigitsOnly(string str)
    {
        foreach (char c in str)
        {
            if (c < '0' || c > '9')
                return false;
        }

        return true;
    }

    public void enqueue()
    {
        if (val == "" || val == " " || prio == " " || prio == "" || !IsDigitsOnly(prio)) {
            PopUp.SetActive(true);
            return;
        }

        top = top + 1;
        GameObject newobj = Instantiate(prefab, lastPos, Quaternion.identity);
        newobj.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.GetComponentInChildren<TMP_Text>().text = val;
        newobj.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.GetComponentInChildren<TMP_Text>().text = prio;

        data.Insert(
            top,
            newobj
        );
        lastPos = lastPos + new Vector3(newobj.transform.localScale.x, 0.0f, 0.0f);

        //clears input field
        val = cinputText.text = "";
        prio = prioInputText.text = "";

        SortDataBasedOnPrio();
    }

    public void reset()
    {
        top = -1;
        foreach(GameObject obj in data)
            Destroy(obj);
        
        lastPos = AuctusBaseConfig.Instance.placementPose.position;
        val = "";
    }

    public void dequeue()
    {
        if (top == -1) return;
        Destroy(data[0]);
        data.RemoveAt(0);
        lastPos = lastPos - new Vector3(prefab.transform.localScale.x, 0.0f, 0.0f);
        for (int i = 0; i < data.Count; i++)
            data[i].transform.position -= new Vector3(prefab.transform.localScale.x, 0.0f, 0.0f);
        top = top - 1;
    }

    void Update()
    {
        if (top == -1) {
            Start();
            return;
        }
    }
}
