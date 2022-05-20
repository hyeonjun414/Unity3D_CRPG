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
        monster.transform.SetParent(GameManager.Instance.worldCanvas.transform, true);
        if (monster != null)
        {
            switch(owner)
            {
                case MonsterOwner.Player:
                    dir = Vector3.right;
                    StageManager.Instance.AllyMonster.Add(monster);
                    break;
                case MonsterOwner.Enemy:
                    dir = Vector3.left;
                    StageManager.Instance.EnemyMonster.Add(monster);
                    break;
            }
            monster.transform.rotation = Quaternion.LookRotation(dir);
            monster.curTile = bt;
            monster.owner = owner;
            // 몬스터에 데이터를 넣고 초기 설정을 진행한다.
            monster.InputData(md);
            monster.anim.SetTrigger("Spawn");
            bt.state = TileState.STAY;
            UIManager.Instance.battleInfoUI.UpdateUI();
            return monster;
        }
        else
            return monster;
        
    }
}
