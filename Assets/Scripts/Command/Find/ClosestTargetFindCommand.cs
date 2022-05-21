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
        if (monster.target != null)
            return;
        monster.target = Find();
    }

    public Monster Find()
    {
        Monster target = null;
        MonsterOwner targetOwner = MonsterOwner.Player;
        switch(monster.owner)
        {
            case MonsterOwner.Player:
                targetOwner = MonsterOwner.Enemy;
                break;
            case MonsterOwner.Enemy:
                targetOwner = MonsterOwner.Player;
                break;
        }
        Collider[] hits = Physics.OverlapBox(transform.position, Vector3.one * 50, Quaternion.identity,LayerMask.GetMask("Enemy"));
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
