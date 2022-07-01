using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : Singleton<CameraManager>
{
    [SerializeField]
    CinemachineVirtualCamera[] cineCams;


    private void Awake()
    {
        if(_instance == null)
            _instance = this;
    }

    public void SwitchCam(int idx)
    {
        for (int i = 0; i < cineCams.Length; i++)
        {
            if(i == idx)
            {
                cineCams[i].Priority = 1;
            }
            else
            {
                cineCams[i].Priority = 0;
            }
        }
    }
}
