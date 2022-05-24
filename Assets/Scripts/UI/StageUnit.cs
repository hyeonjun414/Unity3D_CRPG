using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageUnit : MonoBehaviour
{
    public Image icon;

    public StageData stageData;
    public Stage stage;

    public void UpdateUI(Stage stage)
    {
        this.stage = stage;
        icon.sprite = stage.stageData.icon;
        stage.lr.enabled = true;
    }

    public void ResetUI()
    {
        
    }
}
