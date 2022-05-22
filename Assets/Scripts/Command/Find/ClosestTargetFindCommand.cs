using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosestTargetFindCommand : FindCommand
{
    private Monster monster;
    public override void Setup(LivingEntity entity)
    {
        monster = (Monster)entity;
    }
    public override void Excute()
    {
        if (monster.target == null || monster.target.isDead)
        {
            monster.target = Find();
        }
    }

    public Monster Find()
    {
        Monster target = null;
        MonsterOwner targetOwner = MonsterOwner.Player;
        string targetLayer = null;
        switch(monster.owner)
        {
            case MonsterOwner.Player:
                targetOwner = MonsterOwner.Enemy;
                targetLayer = "Enemy";
                break;
            case MonsterOwner.Enemy:
                targetOwner = MonsterOwner.Player;
                targetLayer = "Ally";
                break;
        }
        Collider[] hits = Physics.OverlapBox(transform.position, Vector3.one * 50, Quaternion.identity,LayerMask.GetMask(targetLayer));
        if(hits.Length > 0)
        {
            float minDist = 500;
            Monster tempMonster;
            float dist;
            for(int i = 0; i < hits.Length; i++)
            {
                tempMonster = hits[i].gameObject.GetComponent<Monster>();
                if(tempMonster != null && tempMonster.owner == targetOwner)
                {
                    dist = Vector3.Distance(tempMonster.transform.position, transform.position);
                    if(minDist > dist)
                    {
                        minDist = dist;
                        target = tempMonster;
                    }
                }
            }
        }

        return target;
    }

}
