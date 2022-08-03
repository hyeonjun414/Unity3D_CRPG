using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerObject : MonoBehaviour
{
    // 플레이어가 접근하여 특정 이벤트를 발동시키는 오브젝트이다.

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            CameraManager.Instance.SwitchCam(CameraType.BATTLESTAGE);
            UIManager.Instance.battleInfoUI.StageStart();
            BattleManager.Instance.BattlePrepare();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            CameraManager.Instance.SwitchCam(CameraType.PLAYER);
            UIManager.Instance.battleInfoUI.StageEnd();
        }
    }
}
