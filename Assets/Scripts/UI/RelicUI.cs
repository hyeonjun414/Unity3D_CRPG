using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicUI : MonoBehaviour
{
    public RelicUnit[] relicUnits;

    public void UpdateUI()
    {
        List<RelicData> playerRelicList = RelicManager.Instance.playerRelics;
        for(int i = 0; i < relicUnits.Length; i++)
        {
            if(i < playerRelicList.Count)
            {
                relicUnits[i].UpdateUI(playerRelicList[i]);
            }
            else
            {
                relicUnits[i].ResetUI();
            }
        }
    }
}
