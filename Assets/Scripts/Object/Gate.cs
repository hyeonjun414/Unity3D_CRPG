using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gate : MonoBehaviour
{
    public string destination;
    public Stage connectedStage;

    public void SetUp(Stage stage)
    {
        connectedStage = stage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            StageManager.Instance.curStage = connectedStage;
            StageManager.Instance.curStageLevel = connectedStage.xPos;
            StageManager.Instance.curStagePos = connectedStage.yPos;
            SceneManager.LoadSceneAsync(destination);
        }
    }

}
