using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;

[RequireComponent(typeof(ARRaycastManager))]
public class PlaneSet : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;
    private ARRaycastManager arRaycastManager;
    private Pose placementPose;
    private bool placementPoseIsValid = false;

    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private bool isPermanentlySetPose = false;

    private void Awake()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
    }

    public void resetPlacementPose()
    {
        isPermanentlySetPose = false;
        EnhancedTouch.Touch.onFingerDown += FingerDownSetPlane;
    }

    public bool getIsPermanentlySetPose()
    {
        return isPermanentlySetPose;
    }

    public bool isSetValidPose()
    {
        return placementPoseIsValid;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPermanentlySetPose) return;

        updatePlacementPose();    
        updatePlacementIndicator();
    }

    private void OnEnable()
    {
        EnhancedTouch.EnhancedTouchSupport.Enable();
        EnhancedTouch.Touch.onFingerDown += FingerDownSetPlane;
    }

    private void OnDisable()
    {
        EnhancedTouch.EnhancedTouchSupport.Disable();
        EnhancedTouch.Touch.onFingerDown -= FingerDownSetPlane;
    }

    private void FingerDownSetPlane(EnhancedTouch.Finger finger)
    {
        if (isPermanentlySetPose && placementPoseIsValid) return;
        if (placementPoseIsValid) {
            isPermanentlySetPose = true;
            EnhancedTouch.Touch.onFingerDown -= FingerDownSetPlane;
        }
    }

    private void updatePlacementPose()
    {
        var screenCenter = Camera.main.ViewportToScreenPoint(
            new Vector3(0.5f, 0.5f)
        );
        arRaycastManager.Raycast(
            screenCenter, 
            hits,
            TrackableType.Planes
        );
        placementPoseIsValid = (hits.Count > 0);
        if (placementPoseIsValid)
        {
            placementPose = hits[0].pose;
            var cameraForward = Camera.main.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
            placementPose.rotation = Quaternion.LookRotation(cameraBearing);
        }
    }


    private void updatePlacementIndicator()
    {
        if (placementPoseIsValid) {

            prefab.SetActive(true);

            prefab.transform.SetPositionAndRotation(
                placementPose.position, 
                placementPose.rotation
            );
        } else {
            prefab.SetActive(false);
        }
    }
}
