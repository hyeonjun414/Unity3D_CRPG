using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageUI : MonoBehaviour
{
    [Header("Stage UI")]
    public Animator anim;
    public StageUnit[] stageUnits;
    public GameObject curStageIndicator;
    public void FindStageUnits()
    {
        stageUnits = GetComponentsInChildren<StageUnit>();
    }
    public void UpdateUI()
    {
        List<Stage> stageList = StageManager.Instance.stages;
        for(int i = 0; i < stageUnits.Length; i++)
        {
            if(i < stageList.Count)
            {
                stageUnits[i].UpdateUI(stageList[i]);
            }
            else
            {
                stageUnits[i].ResetUI();
            }
        }
        stageUnits[stageUnits.Length - 1].UpdateUI(StageManager.Instance.endStage);
        int level = StageManager.Instance.curStageLevel;
        int pos = StageManager.Instance.curStagePos;
        curStageIndicator.transform.position = stageList.Find((x) => x.xPos == level && x.yPos == pos).transform.position;
    }
    public void ActiveMap()
    {
        anim.SetBool("IsActive", true);
        UpdateUI();
    }
    public void InactiveMap()
    {
        anim.SetBool("IsActive", false);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
