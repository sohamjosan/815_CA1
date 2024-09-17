using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.InputSystem.EnhancedTouch;
using System.Collections.Generic;

public class InstantianteObjectScript : MonoBehaviour
{

    public GameObject objectToPlacePrefab;
    private List<ARRaycastHit> hitList = new List<ARRaycastHit>();

    public ARPlaneManager planeManager;
  
    public ARRaycastManager raycastManager;

    private void Awake()
    {
       
        planeManager = GetComponent<ARPlaneManager>();
       
        raycastManager = GetComponent<ARRaycastManager>();
    }
    private void OnEnable()
    {
        Debug.Log("Enable");

        UnityEngine.InputSystem.EnhancedTouch.TouchSimulation.Enable();
        UnityEngine.InputSystem.EnhancedTouch.EnhancedTouchSupport.Enable();
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown += FingerTouchDetected;
    }

    private void OnDisable()
    {
        Debug.Log("Disable");
        UnityEngine.InputSystem.EnhancedTouch.TouchSimulation.Disable();
        UnityEngine.InputSystem.EnhancedTouch.EnhancedTouchSupport.Disable();
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown -= FingerTouchDetected;
    }
   
    void Start()
    {

    }

 
    void Update()
    {

    }
    void FingerTouchDetected(UnityEngine.InputSystem.EnhancedTouch.Finger fingerTouch)
    {

        if (fingerTouch.index != 0)
        {
          
            return;
        }


        if (raycastManager.Raycast(fingerTouch.currentTouch.screenPosition, hitList, TrackableType.PlaneWithinPolygon))
        
        {
            
            foreach (ARRaycastHit hit in hitList)
            
            {
                Pose orintation = hit.pose;
               
                GameObject spawnObject = Instantiate(objectToPlacePrefab, orintation.position, orintation.rotation);

            }
        }
    }
}