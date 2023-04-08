using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;
using TMPro;

public class LinkedList : MonoBehaviour
{
    [SerializeField] GameObject prefab;
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

    public void insertion()
    {
        if (val == "" || val == " " ) return;

        top = top + 1;
        GameObject newobj = Instantiate(
            prefab, 
            lastPos,
            Quaternion.identity
        );

        // sets the text
        newobj.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponentInChildren<TMP_Text>().text = val;
        data.Insert(
            top,
            newobj
        );
        lastPos = lastPos + new Vector3(newobj.transform.localScale.x, 0.0f, 0.0f);

        //clears input field
        cinputText.Select();
        cinputText.text = "";

        val = "";
    }

    public void deletion()
    {
        if (top == -1) return;
        lastPos = lastPos - new Vector3(prefab.transform.localScale.x, 0.0f, 0.0f);
        Destroy(data[top]);
        data.RemoveAt(top);
        top = top - 1;
    }
}
