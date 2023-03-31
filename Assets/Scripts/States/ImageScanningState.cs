using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;

public class ImageScanningState : IState
{
    private GameObject imageScanningCanvas;
    private ARTrackedImageManager arTrackedImageMgr;
    private ARTapToPlace arTapToPlace;

    private Button homeBaseBtn;

    public ImageScanningState(GameObject ImgScannningCanvas)
    {
        this.imageScanningCanvas = ImgScannningCanvas;
    }

    public void EnterState()
    {
        Debug.Log("--Entering Image Scanning State--");
        arTrackedImageMgr = GameObject.FindObjectOfType<ARTrackedImageManager>();
        arTapToPlace = GameObject.FindObjectOfType<ARTapToPlace>();
        arTapToPlace.OnHomeBaseInstantiated += EnableHomeBaseBtn;
        imageScanningCanvas.SetActive(true);

        homeBaseBtn = imageScanningCanvas.GetComponentInChildren<Button>();
        //homeBaseBtn.gameObject.SetActive(false);
    }

    private void EnableHomeBaseBtn()
    {
        homeBaseBtn.gameObject.SetActive(true); 
    }

    public void ExecuteState()
    {
        Debug.Log("--Execute ImageScanningState--");
        arTrackedImageMgr.trackedImagesChanged += OnImagesChanged;
    }

    private void OnImagesChanged(ARTrackedImagesChangedEventArgs objdata)
    {
        foreach (var image in objdata.added)
        {
            Debug.Log("Image Found");
            arTapToPlace.enabled = true;
            imageScanningCanvas.GetComponentInChildren<TMPro.TMP_Text>().text = "Tap to place";
        }
    }

    public void ExitState()
    {
        Debug.Log("--Exiting ImageScanningState--");
        imageScanningCanvas.SetActive(false);
        arTapToPlace.enabled = false;
    }
}
