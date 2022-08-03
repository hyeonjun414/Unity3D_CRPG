using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public enum CameraType
{
    PLAYER,
    BATTLESTAGE,
}

public class CameraManager : Singleton<CameraManager>
{
    [SerializeField]
    CinemachineVirtualCamera[] virtualCameras;


    private void Awake()
    {
        if(_instance == null)
            _instance = this;
    }

    public void SwitchCam(CameraType targetCamera)
    {
        for (CameraType cameraIndex = 0; cameraIndex < (CameraType)virtualCameras.Length; cameraIndex++)
        {
            if(cameraIndex == targetCamera)
                virtualCameras[(int)cameraIndex].Priority = 1;
            else
                virtualCameras[(int)cameraIndex].Priority = 0;
        }
    }
}
