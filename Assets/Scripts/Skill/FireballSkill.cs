using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballSkill : Skill
{
    [Header("Fireball Prefab")]
    public GameObject fireball;


    public override void SetUp(Monster monster, SkillData sd)
    {
        base.SetUp(monster, sd);
    }
    public override IEnumerator CastingRoutine()
    {
        return base.CastingRoutine();
    }
    public override void Casting()
    {
        AllFireball();
    }
    public void AllFireball()
    {
        print("Fireball Casting");

        Monster target = null;
        MonsterOwner targetOwner = MonsterOwner.Player;
        switch (monster.owner)
        {
            case MonsterOwner.Player:
                targetOwner = MonsterOwner.Enemy;
                break;
            case MonsterOwner.Enemy:
                targetOwner = MonsterOwner.Player;
                break;
        }


        Collider[] hits = Physics.OverlapBox(transform.position, Vector3.one * (monster.range * 2 + 1), Quaternion.identity, LayerMask.GetMask("Enemy"));
        if(hits.Length > 0)
        {
            foreach(Collider hit in hits)
            {
                target = hit.gameObject.GetComponent<Monster>();
                if (target != null && target.owner == targetOwner)
                {
                    Projectile proj = Instantiate(fireball, transform.position, Quaternion.identity).GetComponent<Projectile>();
                    proj.transform.localScale = Vector3.one * 2;
                    proj.SetUp(monster, target, 3, ProjectileMoveType.Indirect);
                }
            }
        }
    }

    public void ShotFireball()
    {
        
    }



}
