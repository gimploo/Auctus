using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;
using TMPro;

public class CircularQueue : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] TMP_InputField cinputText;
    [SerializeField] GameObject PopUp;

    private string val = "";
    private int top = -1;
    private Vector3 lastPos = default;
    private Vector3 defaultPos;

    private float theta;
    private float radius;

    List<GameObject> data = new List<GameObject>();

    public void updateInputText()
    {
        val = cinputText.text;
    }

    void Start()
    {
        defaultPos = AuctusBaseConfig.Instance.placementPose.position;
        theta = 0.0f;
        radius = 0.2f;
    }

    public void enqueue()
    {
        if (val == "" || val == " " ) {
            PopUp.SetActive(true);
            return;
        }
        if (data.Count == 9) return;

        top = top + 1;
        theta += 125.0f;
        GameObject newobj = Instantiate(
            prefab, 
            defaultPos + new Vector3(radius * Mathf.Cos(theta), 0.0f, radius * Mathf.Sin(theta)), 
            Quaternion.identity
        );
        newobj.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponentInChildren<TMP_Text>().text = val;
        data.Insert(
            top,
            newobj
        );
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

    public void dequeue()
    {
        if (top == -1) return;

        Destroy(data[0]);
        data.RemoveAt(0);
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
