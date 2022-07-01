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
        int layerNum = 0;
        if (owner == MonsterOwner.Player)
        {
            dir = Vector3.right;
            layerNum = LayerMask.NameToLayer("Ally");
        }
        else
        {
            dir = Vector3.left;
            layerNum = LayerMask.NameToLayer("Enemy");
        }
            
            
        Monster monster = null;
        monster = ObjectPoolManager.Instance.UseObj(md.monster).GetComponent<Monster>();
        monster.transform.position = bt.transform.position;
        monster.transform.rotation = Quaternion.LookRotation(dir);
        monster.transform.localScale = Vector3.one;
        monster.gameObject.layer = layerNum;
        monster.transform.SetParent(GameManager.Instance.objectSpace, true);
        if (monster != null)
        {
            switch(owner)
            {
                case MonsterOwner.Player:
                    dir = Vector3.right;
                    BattleManager.Instance.allyMonster.Add(monster);
                    break;
                case MonsterOwner.Enemy:
                    dir = Vector3.left;
                    BattleManager.Instance.enemyMonster.Add(monster);
                    break;
            }
            monster.transform.rotation = Quaternion.LookRotation(dir);
            monster.curTile = bt;
            monster.owner = owner;
            // 몬스터에 데이터를 넣고 초기 설정을 진행한다.
            monster.InputData(md);
            monster.anim.SetTrigger("Spawn");
            bt.state = TileState.STAY;
            bt.monster = monster;
            UIManager.Instance.battleInfoUI.UpdateUI();
            
            return monster;
        }
        else
            return monster;
        
    }
}
