using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonManager : Singleton<SummonManager>
{
    

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
    }

    public Monster SummonMonster(MonsterData md, BattleTile bt, MonsterOwner owner)
    {
        Vector3 dir;
        if (owner == MonsterOwner.Player)
            dir = Vector3.right;
        else
            dir = Vector3.left;
        Monster monster = null;
        monster = Instantiate(md.monster, bt.transform.position, Quaternion.LookRotation(dir)).GetComponent<Monster>();
        if (monster != null)
        {
            monster.curTile = bt;
            monster.owner = owner;
            // 몬스터에 데이터를 넣고 초기 설정을 진행한다.
            monster.InputData(md);
            monster.anim.SetTrigger("Spawn");
            bt.state = TileState.STAY;
            return monster;
        }
        else
            return monster;
        
    }
}
