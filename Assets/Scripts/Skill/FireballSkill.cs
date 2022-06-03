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

        Monster target = null;
        MonsterOwner targetOwner = MonsterOwner.Player;
        string targetLayer = null;
        switch (monster.owner)
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
        Collider[] hits = Physics.OverlapBox(transform.position, Vector3.one * (monster.range * 2 + 1), Quaternion.identity, LayerMask.GetMask(targetLayer));
        if (hits.Length > 0)
        {
            foreach (Collider hit in hits)
            {
                target = hit.gameObject.GetComponent<Monster>();
                if (target != null && target.owner == targetOwner)
                {
                    Projectile proj = ObjectPoolManager.Instance.UseObj(fireball).GetComponent<Projectile>();
                    proj.transform.position = transform.position;
                    proj.transform.rotation = Quaternion.identity;
                    proj.SetUp(monster, target, 3, ProjectileMoveType.Indirect);
                }
            }
        }
    }

    public void ShotFireball()
    {
        
    }



}
