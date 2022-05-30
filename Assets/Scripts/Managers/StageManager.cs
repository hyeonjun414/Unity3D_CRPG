using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StageManager : Singleton<StageManager>
{
    public Stage stageObj;
    public int stageHeight;
    public int stageWidth;
    public List<Stage> stages;
    public Stage endStage;
    public Stage curStage;

    public Gate gatePrefab;

    public int curStageLevel; // x 좌표
    public int curStagePos; // y 좌표

    public StageData[] stageDatas;
    public Transform stageMapUIPos;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }

    }
    private void Start()
    {
        stages = new List<Stage>();
        GenerateStage();
        DetectAndMerge();
        curStage = stages[0];
        curStagePos = stages[0].yPos;
        curStageLevel = stages[0].xPos;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            GenerateGate();
        }
    }
    public void GenerateGate()
    {
        if (curStage == endStage) return;

        List<Stage> nextStages = stages.FindAll((sameStage) => 
        sameStage.xPos == curStage.xPos && sameStage.yPos == curStage.yPos &&
        curStage.stageNode.nextNode.yPos != sameStage.stageNode.nextNode.yPos);
        nextStages.Add(curStage);
        nextStages.Sort(delegate (Stage a, Stage b)
            {
                if(a.stageNode.nextNode.yPos > b.stageNode.nextNode.yPos)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            );
        Vector3 gatePos = new Vector3(14, 0.5f, 0);
        for(int i = 0; i < nextStages.Count; i++)
        {
            Gate gate = Instantiate(gatePrefab, gatePos + Vector3.forward * (nextStages[i].stageNode.nextNode.yPos - curStage.yPos) * 5f, gatePrefab.transform.rotation);
            gate.SetUp(nextStages[i].stageNode.nextNode);
        }
    }

    public void GenerateScene(Scene scene, LoadSceneMode mode)
    {
        print("Scene Making...");
        int rand = 0;
        switch (curStage.stageData.type)
        {
            case StageType.Enemy:
                {
                    EnemyStageData data = (EnemyStageData)curStage.stageData;
                    rand = Random.Range(0, data.enemyMaps.Length);
                    Instantiate(data.enemyMaps[rand]);
                    Instantiate(data.enemyBattleStages[rand]);
                    Instantiate(data.Enemy, new Vector3(11, 0.5f, 0), data.Enemy.transform.rotation);
                }
                break;
            case StageType.Rest:
                if (curStage.stageData.optionalMap != null)
                {
                    Instantiate(curStage.stageData.optionalMap);
                    GenerateGate();
                }
                break;
            case StageType.Event:
                if (curStage.stageData.optionalMap != null)
                {
                    Instantiate(curStage.stageData.optionalMap);
                    GenerateGate();
                }
                break;
            case StageType.Shop:
                if (curStage.stageData.optionalMap != null)
                {
                    Instantiate(curStage.stageData.optionalMap);
                    GenerateGate();
                }
                break;
            case StageType.Boss:
                {
                    EnemyStageData data = (EnemyStageData)curStage.stageData;
                    rand = Random.Range(0, data.enemyMaps.Length);
                    Instantiate(data.enemyMaps[rand]);
                    Instantiate(data.enemyBattleStages[rand]);
                    Instantiate(data.Enemy, new Vector3(11, 0.5f, 0), data.Enemy.transform.rotation);
                }
                break;
        }
    }

    public void GenerateStage()
    {
        if(stages != null)
            ResetStage();

        Stage tempStage = null;
        for(int i = 0; i < stageWidth; i++)
        {
            for(int j = 0; j < stageHeight; j++)
            {
                tempStage = Instantiate(stageObj);
                tempStage.transform.SetParent(stageMapUIPos.transform, false);
                tempStage.xPos = i;
                tempStage.yPos = j;
                stages.Add(tempStage);
            }
        }
        // 보스 노드 만들기
        tempStage = Instantiate(stageObj);
        tempStage.transform.SetParent(stageMapUIPos.transform, false);
        tempStage.xPos = stageWidth;
        tempStage.yPos = stageHeight/2;
        tempStage.transform.localScale = Vector3.one * 2f;
        tempStage.lr.enabled = false;
        tempStage.SetUp(RandomStageType(tempStage.xPos));
        endStage = tempStage;

        // 노드 경로와 색상 지정
        for (int j = 0; j < stageHeight; j++)
        {
            int offsetX = 0;
            Color color = new Color(Random.Range(0.5f, 1f), Random.Range(0.5f, 1f), Random.Range(0.5f, 1f));
            for (int i = 0; i < stageWidth; i++)
            {
                offsetX += Random.Range(-1, 2);
                if (j + offsetX < 0)
                {
                    stages[stageHeight * i + j].yPos = 0;
                }
                else if (j + offsetX > stageHeight - 1)
                {
                    stages[stageHeight * i + j].yPos = stageHeight - 1;
                }
                else
                {
                    stages[stageHeight * i + j].yPos = j + offsetX;
                }
                stages[stageHeight * i + j].gameObject.GetComponent<LineRenderer>().startColor = color;
                stages[stageHeight * i + j].gameObject.GetComponent<LineRenderer>().endColor = color;
            }
        }

        // 이전 노드와 다음 노드간에 연결해주기
        for (int j = 0; j < stageHeight; j++)
        {
            for (int i = 0; i < stageWidth; i++)
            {
                if (i + 1 < stageWidth)
                {
                    stages[stageHeight * i + j].stageNode.nextNode = stages[stageHeight * (i+1) + j];
                    stages[stageHeight * (i + 1) + j].stageNode.prevNode = stages[stageHeight * i + j];
                }
            }
            stages[stageHeight * (stageWidth - 1) + j].stageNode.nextNode = endStage;
            endStage.stageNode.prevNode = stages[stageHeight * (stageWidth - 1) + j];
        }

        MoveStage();

    }

    // 위치가 중첩되는 스테이지 노드를 동기화
    public void DetectAndMerge()
    {
        List<Stage> tempList = null;
        int count = 0;
        for (int i = 0; i < stageWidth; i++)
        {
            for (int j = 0; j < stageHeight; j++)
            {
                tempList = stages.FindAll((stage) => stage.yPos == j && stage.xPos == i);
                if(tempList.Count > 0)
                {
                    count++;
                    //print($"Merge Count : {count}");
                    StageData targetData = RandomStageType(tempList[0].xPos);
                    for (int k = 0; k < tempList.Count; k++)
                    {
                        tempList[k].SetUp(targetData);
                    } 
                }
            }
        }

        // 생성된 유닛을 스테이지 UI가 알도록 함
        UIManager.Instance.stageUI.FindStageUnits();
    }
    
    // 생성한 스테이지 노드들을 각각 위치에 맞게 이동시킨다.
    public void MoveStage()
    {
        foreach (Stage stage in stages)
        {
            stage.transform.localPosition = new Vector3(stage.xPos * 100f, stage.yPos* 100f, 0);
        }
        endStage.transform.localPosition = new Vector3(endStage.xPos * 100f + 50, endStage.yPos * 100f, 0);
    }
    public void ResetStage()
    {
        foreach (Stage stage in stages)
        {
            Destroy(stage.gameObject);
        }
        stages.Clear();
    }

    // 스테이지의 특성을 지정하는 로직
    public StageData RandomStageType(int level)
    {
        // 만약 첫 단계면 몬스터랑 배틀 확정
        if (level == 0)
            return stageDatas[(int)StageType.Enemy];
        else if (level == stageWidth - 1)
            return stageDatas[(int)StageType.Rest];
        else if (level == stageWidth)
            return stageDatas[(int)StageType.Boss];

        StageData randomData = null;
        // 이후에는 랜덤 인카운트
        // 
        int rand = Random.Range(0, 101);
        if(rand < 30)
            randomData = stageDatas[(int)StageType.Event];
        else if (rand < 70)
            randomData = stageDatas[(int)StageType.Shop];
        else if (rand < 89)
            randomData = stageDatas[(int)StageType.Rest];
        else
            randomData = stageDatas[(int)StageType.Event];

        return randomData;
    }
}
