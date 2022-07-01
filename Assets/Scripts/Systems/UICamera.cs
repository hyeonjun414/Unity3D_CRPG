using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICamera : MonoBehaviour
{
    public  Camera mainCam;
    public Camera uiCam;
    public void Start()
    {
        mainCam = Camera.main;
        uiCam = GetComponent<Camera>();
    }
    private void LateUpdate()
    {
        uiCam.orthographicSize = mainCam.orthographicSize;
    }
}
