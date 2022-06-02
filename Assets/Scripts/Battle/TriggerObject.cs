﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TriggerType
{
    Player,
    BattleStage,
    Map,
}
public class TriggerObject : MonoBehaviour
{
    // 플레이어가 접근하여 특정 이벤트를 발동시키는 오브젝트이다.
    public TriggerType type;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            switch(type)
            {
                case TriggerType.BattleStage:
                    CameraManager.Instance.SwitchCam(1);
                    UIManager.Instance.battleInfoUI.StageStart();
                    BattleManager.Instance.BattlePrepare();
                    break;
                case TriggerType.Map:
                    CameraManager.Instance.SwitchCam(2);
                    UIManager.Instance.stageUI.ActiveMap();
                    break;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            switch (type)
            {
                case TriggerType.BattleStage:
                    CameraManager.Instance.SwitchCam(0);
                    UIManager.Instance.battleInfoUI.StageEnd();
                    break;
                case TriggerType.Map:
                    CameraManager.Instance.SwitchCam(0);
                    UIManager.Instance.stageUI.InactiveMap();
                    break;
            }
        }
    }
}