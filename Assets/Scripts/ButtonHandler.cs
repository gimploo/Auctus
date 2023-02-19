using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    [SerializeField]
    ARSessionOrigin arOrigin;
    private PlaneSet planeSetScript;
    public Button resetButton;
    public Button MovePhonePrompt;

    private void Awake()
    {
        planeSetScript = arOrigin.GetComponent<PlaneSet>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!planeSetScript.getIsPermanentlySetPose()) {
            resetButton.enabled = false;
            resetButton.interactable = false;
            resetButton.gameObject.SetActive(false);
        } else {
            resetButton.interactable = true;
            resetButton.enabled = true;
            resetButton.gameObject.SetActive(true);
        }

        if (!planeSetScript.isSetValidPose()) {
            MovePhonePrompt.gameObject.SetActive(true);
        } else {
            MovePhonePrompt.gameObject.SetActive(false);
        }
    }
}
