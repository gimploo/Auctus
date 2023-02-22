using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;

public class Stack : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;

    private int top = -1;
    private Vector3 lastPos = default;

    void Start()
    {
        lastPos = AuctusBaseConfig.Instance.placementPose.position;
    }

    public void push()
    {
        top = top + 1;

        Instantiate(
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

        lastPos = lastPos + new Vector3(0.0f, 0.5f, 0.0f);
    }

    public void pop()
    {
        if (top == -1) return;
    }
}
