using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;
[RequireComponent(requiredComponent: typeof(ARRaycastManager), requiredComponent2:typeof(ARPlaneManager))]
public class raycastScript : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;
    private ARPlaneManager aRPlaneManager;
    private ARRaycastManager aRRaycastManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private bool isPlaced;
    private void Awake()
    {
        isPlaced = false;
        aRPlaneManager = GetComponent<ARPlaneManager>();
        aRRaycastManager = GetComponent<ARRaycastManager>();

    }

    private void OnEnable()
    {
        EnhancedTouch.TouchSimulation.Enable();
        EnhancedTouch.EnhancedTouchSupport.Enable();
        EnhancedTouch.Touch.onFingerDown += FingerDown;
    }
    private void OnDisable()
    {
        EnhancedTouch.TouchSimulation.Disable();
        EnhancedTouch.EnhancedTouchSupport.Disable();
        EnhancedTouch.Touch.onFingerDown -= FingerDown;

    }

    private void FingerDown(EnhancedTouch.Finger finger) {
        if (finger.index != 0) return;
        if (!isPlaced)
        {
            Place_Prefab(finger);
        }
    }

    private void Place_Prefab(EnhancedTouch.Finger finger) {
        if (aRRaycastManager.Raycast(finger.currentTouch.screenPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            foreach (ARRaycastHit hit in hits)
            {
                Pose pose = hit.pose;
                GameObject obj = Instantiate(prefab, pose.position, pose.rotation);
                isPlaced = true;
            }
            LevelSpawner.levelSet = true;
            Time.timeScale = 1;
            var planes = aRPlaneManager.trackables;
            foreach (var plane in planes)
            {
                plane.gameObject.SetActive(false);
            }
            //aRPlaneManager.enabled = false;
            LevelManager.Fill_lists();
        }
    }

    
}
