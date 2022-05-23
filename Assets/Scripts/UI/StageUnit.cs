using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageUnit : MonoBehaviour
{
    public Image icon;

    public StageData stageData;

    public void UpdateUI()
    {
        icon.sprite = stageData.icon;
    }

    public void ResetUI()
    {
        
    }
}
