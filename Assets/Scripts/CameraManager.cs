using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance { get; private set; }

    public CinemachineVirtualCamera mainCam;

    public float zoomSpeed = 100f;

    private void Awake()
    {
        if (Instance != null)
            DestroyImmediate(this);
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    private void Update()
    {
        Zoom();
    }
    
    private void Zoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        mainCam.m_Lens.OrthographicSize -= scroll * zoomSpeed * Time.deltaTime;
        //uiCam.orthographicSize -= scroll * zoomSpeed * Time.deltaTime;
    }
}
