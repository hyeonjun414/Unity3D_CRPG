using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StageNode
{
    public List<Stage> nextNode;
    public List<Stage> prevNode;

    public StageNode()
    {
        nextNode = new List<Stage>();
        prevNode = new List<Stage>();
    }
}

public class Stage : MonoBehaviour
{
    [Header("Stage Node")]
    [SerializeField]
    public StageNode stageNode = new StageNode();
    public int yPos;
    public int xPos;

    public LineRenderer lr;

    [Header("Stage Data")]
    public StageData stageData;

    [Header("Stage Unit")]
    public StageUnit stageUnit;

    public void SetUp(StageData data)
    {
        stageData = data;
        stageUnit.stageData = data;
        stageUnit.UpdateUI();
    }

}
