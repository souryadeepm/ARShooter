using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System;
using UnityEngine.EventSystems;

public class ARTapToPlace : MonoBehaviour
{
    [SerializeField] private GameObject homeBase;

    public event Action OnHomeBaseInstantiated;

    private ARRaycastManager arRaycastMgr;
    private GameObject homeBasePrefab;

    private Vector2 touchPosition;

    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private bool isOverUI;

    private void Start()
    {

        arRaycastMgr = FindObjectOfType<ARRaycastManager>();
    }

    private void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Began)
            {
                Vector2 touchPos = touch.position;

                isOverUI = isOverUIObect(touchPos);
            }
            if (/*!isOverUI &&*/ arRaycastMgr.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
            {
                Pose hitpose = hits[0].pose;

                if (homeBase == null)
                {
                    homeBase = Instantiate(homeBasePrefab, hitpose.position, hitpose.rotation);
                    OnHomeBaseInstantiated.Invoke();
                }
                else
                {
                    homeBase.transform.position = hitpose.position;
                }
            }
        } 
    }

    private bool isOverUIObect(Vector2 pos)
    {   
        if(EventSystem.current.IsPointerOverGameObject())
        return false;

        PointerEventData eventPosition = new PointerEventData(EventSystem.current);
        eventPosition.position= new Vector2(pos.x, pos.y);


        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventPosition, results);

        return results.Count > 0;
        
    }

    public void HomeBaseLocked()
    {
        GameManager.instance.HomeBase = homeBase;
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Vector2 touchPos = touch.position;

                isOverUI = isOverUIObect(touchPos);
            }
            if (/*!isOverUI &&*/ arRaycastMgr.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
            {
                Pose hitpose = hits[0].pose;

                if (homeBase == null)
                {
                    homeBase = Instantiate(homeBasePrefab, hitpose.position, hitpose.rotation);
                    OnHomeBaseInstantiated.Invoke();
                }
                else
                {
                    homeBase.transform.position = hitpose.position;
                }
            }
        }
    }

    
}
