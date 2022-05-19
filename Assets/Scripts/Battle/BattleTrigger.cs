using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            CameraManager.Instance.SwitchCam(1);
            UIManager.Instance.battleInfoUI.StageStart();
            StageManager.Instance.BattlePrepare();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CameraManager.Instance.SwitchCam(0);
            UIManager.Instance.battleInfoUI.StageEnd();
        }
    }
}
