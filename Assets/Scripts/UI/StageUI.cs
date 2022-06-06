using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageUI : MonoBehaviour
{
    [Header("Stage UI")]
    public List<Stage> stages;
    public Animator anim;
    public StageUnit[] stageUnits;
    public GameObject curStageIndicator;

    [Header("Unit Position")]
    public RectTransform stageGroup;
    public Transform startPos;
    public Transform endPos;
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
        stageUnits[stageUnits.Length - 2].UpdateUI(StageManager.Instance.startStage);
        stageUnits[stageUnits.Length - 1].UpdateUI(StageManager.Instance.endStage);
        int level = StageManager.Instance.curStageLevel;
        int pos = StageManager.Instance.curStagePos;
        if(level == -1)
        {
            curStageIndicator.transform.position = startPos.position;
        }
        else if(level == StageManager.Instance.stageWidth)
        {
            curStageIndicator.transform.position = endPos.position;
        }
        else
        {
            curStageIndicator.transform.position = stageList.Find((x) => x.xPos == level && x.yPos == pos).transform.position;
        }
       
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

    public void BtnExit()
    {
        gameObject.SetActive(false);
    }

    public void SetLineAndPosition()
    {
        SetStagePos();
    }
    // 생성한 스테이지 노드들을 각각 위치에 맞게 이동시킨다.
    public void SetStagePos()
    {
        StageManager sm = StageManager.Instance;
        float x = sm.stageMapUIPos.sizeDelta.x;
        float y = sm.stageMapUIPos.sizeDelta.y;
        x /= sm.stageWidth-1;
        y /= sm.stageHeight - 1;
        foreach (Stage stage in sm.stages)
        {
            stage.transform.localPosition = new Vector3(stage.xPos * x, stage.yPos * y, 0);
        }
        sm.startStage.transform.position = startPos.position;
        sm.endStage.transform.position = endPos.position;

        UpdateUI();
    }
}
