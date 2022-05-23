using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class StageManager : Singleton<StageManager>
{
    public Stage stageObj;
    public int stageWidth;
    public int stageHeight;
    public List<Stage> stages;
    public Stage endStage;

    public int curStageLevel; // y 좌표
    public int curStagePos; // x 좌표

    public StageData[] stageDatas;
    public Transform stageMapUIPos;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
    }
    private void Start()
    {
        stages = new List<Stage>();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            GenerateStage();
            DetectAndMerge();
        }
    }

    public void GenerateStage()
    {
        if(stages != null)
            ResetStage();

        Stage tempStage = null;
        for(int i = 0; i < stageHeight; i++)
        {
            for(int j = 0; j < stageWidth; j++)
            {
                tempStage = Instantiate(stageObj);
                tempStage.transform.SetParent(stageMapUIPos.transform, false);
                tempStage.yPos = i;
                tempStage.xPos = j;
                stages.Add(tempStage);
            }
        }

        // 보스 노드 만들기
        tempStage = Instantiate(stageObj);
        tempStage.transform.SetParent(stageMapUIPos.transform, false);
        tempStage.yPos = stageHeight;
        tempStage.xPos = stageWidth/2;
        tempStage.transform.localScale = Vector3.one * 2f;
        tempStage.lr.enabled = false;
        endStage = tempStage;

        for (int j = 0; j < stageWidth; j++)
        {
            int offsetX = 0;
            Color color = new Color(Random.Range(0.5f, 1f), Random.Range(0.5f, 1f), Random.Range(0.5f, 1f));
            for (int i = 0; i < stageHeight; i++)
            {
                offsetX += Random.Range(-1, 2);
                if (j + offsetX < 0)
                {
                    stages[stageWidth * i + j].xPos = 0;
                }
                else if (j + offsetX > stageWidth - 1)
                {
                    stages[stageWidth * i + j].xPos = stageWidth - 1;
                }
                else
                {
                    stages[stageWidth * i + j].xPos = j + offsetX;
                }
                stages[stageWidth * i + j].gameObject.GetComponent<LineRenderer>().startColor = color;
                stages[stageWidth * i + j].gameObject.GetComponent<LineRenderer>().endColor = color;
            }
        }

        for (int j = 0; j < stageWidth; j++)
        {
            for (int i = 0; i < stageHeight; i++)
            {
                if (i + 1 < stageHeight)
                {
                    stages[stageWidth * i + j].stageNode.nextNode.Add(stages[stageWidth * (i+1) + j]);
                    stages[stageWidth * (i + 1) + j].stageNode.prevNode.Add(stages[stageWidth * i + j]);
                }
            }
            stages[stageWidth * (stageHeight - 1) + j].stageNode.nextNode.Add(endStage);
            endStage.stageNode.prevNode.Add(stages[stageWidth * (stageHeight - 1) + j]);
        }

        MoveStage();

    }
    public void DetectAndMerge()
    {
        List<Stage> tempList = null;
        int count = 0;
        for (int i = 0; i < stageHeight; i++)
        {
            for (int j = 0; j < stageWidth; j++)
            {
                tempList = stages.FindAll((stage) => stage.xPos == j && stage.yPos == i);
                if(tempList.Count > 0)
                {
                    count++;
                    print($"Merge Count : {count}");
                    StageData targetData = RandomStageType(tempList[0].yPos);
                    for (int k = 0; k < tempList.Count; k++)
                    {
                        tempList[k].SetUp(targetData);
                        //tempList[k].transform.Translate(Vector3.forward * k);
                    } 
                }
            }
        }
    }
    public void MoveStage()
    {
        foreach (Stage stage in stages)
        {
            stage.transform.localPosition = new Vector3(stage.yPos * 100f, stage.xPos* 100f, 0);
        }
        endStage.transform.localPosition = new Vector3(endStage.yPos * 100f + 50, endStage.xPos * 100f, 0);
        for (int j = 0; j < stageWidth; j++)
        {
            for (int i = 0; i < stageHeight; i++)
            {
                if (i + 1 < stageHeight)
                {
                    stages[stageWidth * i + j].lr.SetPosition(0, stages[stageWidth * i + j].transform.position);
                    stages[stageWidth * i + j].lr.SetPosition(1, stages[stageWidth * (i+1) + j].transform.position);
                    
                }
            }
            stages[stageWidth * (stageHeight - 1) + j].lr.SetPosition(0, stages[stageWidth * (stageHeight - 1) + j].transform.position);
            stages[stageWidth * (stageHeight - 1) + j].lr.SetPosition(1, endStage.transform.position);
        }
    }
    public void ResetStage()
    {
        foreach (Stage stage in stages)
        {
            Destroy(stage.gameObject);
        }
        stages.Clear();
    }

    public StageData RandomStageType(int level)
    {
        // 만약 첫 단계면 몬스터랑 배틀 확정
        if (level == 0)
            return stageDatas[(int)StageType.Enemy];
        else if (level == stageHeight - 1)
            return stageDatas[(int)StageType.Rest];
        else if (level == stageHeight)
            return stageDatas[(int)StageType.Boss];

        StageData randomData = null;
        // 이후에는 랜덤 인카운트
        // 
        int rand = Random.Range(0, 101);
        if(rand < 75)
            randomData = stageDatas[(int)StageType.Enemy];
        else if (rand < 82)
            randomData = stageDatas[(int)StageType.Shop];
        else if (rand < 89)
            randomData = stageDatas[(int)StageType.Rest];
        else
            randomData = stageDatas[(int)StageType.Event];

        return randomData;
    }
}
