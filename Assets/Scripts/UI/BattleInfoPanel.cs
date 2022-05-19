using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleInfoPanel : MonoBehaviour
{
    public MonsterOwner owner;
    public BattleInfoUnit[] infoUnits;

    List<Monster> monList;

    private void Start()
    {
        infoUnits = GetComponentsInChildren<BattleInfoUnit>();

        if(owner == MonsterOwner.Player)
        {
            monList = StageManager.Instance.AllyMonster;
        }
        else if(owner == MonsterOwner.Enemy)
        {
            monList = StageManager.Instance.EnemyMonster;
        }
    }

    public void UpdateUI()
    {
        for(int i = 0; i < infoUnits.Length; i++)
        {
            if(i < monList.Count)
            {
                infoUnits[i].UpdateUI(monList[i]);
            }
            else
            {
                infoUnits[i].ResetUI();
            }
        }
    }
}
