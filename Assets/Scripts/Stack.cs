using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;
using TMPro;

public class Stack : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] GameObject PopUp;
    [SerializeField] TMP_InputField cinputText;

    private string val = "";
    private int top = -1;
    private Vector3 lastPos = default;

    List<GameObject> data = new List<GameObject>();

    public void updateInputText()
    {
        val = cinputText.text;
    }

    void Start()
    {
        lastPos = AuctusBaseConfig.Instance.placementPose.position;
    }

    void Update()
    {
        if (top == -1) {
            Start();
            return;
        }
    }

    public void push()
    {
        if (val == "" || val == " " ) {
            PopUp.SetActive(true);
            return;
        }

        top = top + 1;
        GameObject newobj = Instantiate(
            prefab, 
            lastPos,
            Quaternion.LookRotation(
                new Vector3(
                    Camera.main.transform.forward.x, 
                    0, 
                    Camera.main.transform.forward.z
                ).normalized
            )
        );

        // sets the text
        // newobj.transform.Find("Button").gameObject.GetComponentInChildren<Text>().text = val;
        newobj.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponentInChildren<TMP_Text>().text = val;
        data.Insert(
            top,
            newobj
        );
        lastPos = lastPos + new Vector3(0.0f, newobj.transform.localScale.y, 0.0f);

        //clears input field
        val = cinputText.text = "";
    }

    public void reset()
    {
        top = -1;
        foreach(GameObject obj in data)
            Destroy(obj);
        
        lastPos = AuctusBaseConfig.Instance.placementPose.position;
        val = "";
    }

    public void pop()
    {
        if (top == -1) return;
        lastPos = lastPos - new Vector3(0.0f, prefab.transform.localScale.y, 0.0f);
        Destroy(data[top]);
        data.RemoveAt(top);
        top = top - 1;
    }
}
