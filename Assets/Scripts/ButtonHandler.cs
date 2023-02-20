using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
    [SerializeField]
    ARSessionOrigin arOrigin;
    private AuctusBaseConfig planeSetScript;
    public Button resetButton;
    public Button submitButton;
    public Button MovePhonePrompt;
    public GameObject DataStructures;

    private int scene_no = 0;

    private void Awake()
    {
        planeSetScript = arOrigin.GetComponent<AuctusBaseConfig>();
        DataStructures.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        switch (scene_no)
        {
            case 0: 
                if (!planeSetScript.getIsPermanentlySetPose()) {
                    resetButton.enabled = false;
                    resetButton.interactable = false;
                    resetButton.gameObject.SetActive(false);
                    submitButton.enabled = false;
                    submitButton.interactable = false;
                    submitButton.gameObject.SetActive(false);
                } else {
                    resetButton.interactable = true;
                    resetButton.enabled = true;
                    resetButton.gameObject.SetActive(true);
                    submitButton.interactable = true;
                    submitButton.enabled = true;
                    submitButton.gameObject.SetActive(true);
                }

                if (!planeSetScript.isSetValidPose()) {
                    MovePhonePrompt.gameObject.SetActive(true);
                } else {
                    MovePhonePrompt.gameObject.SetActive(false);
                }
            break;
        }
    }

    public void onSubmit()
    {
        resetButton.enabled = false;
        submitButton.enabled = false;
        resetButton.interactable = false;
        submitButton.interactable = false;
        submitButton.gameObject.SetActive(false);
        resetButton.gameObject.SetActive(false);
        scene_no = 1;
        DataStructures.SetActive(true);
    }
}
