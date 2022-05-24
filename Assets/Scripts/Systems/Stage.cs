using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StageNode
{
    public Stage nextNode;
    public Stage prevNode;

}

public class Stage : MonoBehaviour
{
    [Header("Stage Node")]
    [SerializeField]
    public StageNode stageNode = new StageNode();
    public int xPos;
    public int yPos;

    public LineRenderer lr;

    [Header("Stage Data")]
    public StageData stageData;


    public void SetUp(StageData data)
    {
        stageData = data;
    }

    private void Update()
    {
        if(stageNode.nextNode != null)
        {
            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, stageNode.nextNode.transform.position);
        }

    }
}
