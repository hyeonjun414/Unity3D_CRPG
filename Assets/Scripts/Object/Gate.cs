using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gate : MonoBehaviour
{
    [Header("Stage")]
    public string destination;
    public Stage connectedStage;

    [Header("Portals")]
    public GameObject[] portals;

    public void SetUp(Stage stage)
    {
        connectedStage = stage;
        SetPortal((int)stage.stageData.type);

    }
    public void SetPortal(int idx)
    {
        portals[idx].SetActive(true);
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            StageManager.Instance.curStage = connectedStage;
            StageManager.Instance.curStageLevel = connectedStage.xPos;
            StageManager.Instance.curStagePos = connectedStage.yPos;
            LoadingManager.LoadScene(destination);
        }
    }

}
