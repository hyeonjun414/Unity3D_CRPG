using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathRaySkill : Skill
{
    [Header("Ray Prefab")]
    public GameObject ray;

    [Header("Level Variable")]
    public int rayCount;
    public int rayDamage;

    public override void SetUp(Monster monster, SkillData sd)
    {
        base.SetUp(monster, sd);

        switch (skillLevel)
        {
            case 0:
                rayCount = 4;
                rayDamage = 50;
                break;
            case 1:
                rayCount = 8;
                rayDamage = 80;
                break;
            case 2:
                rayCount = 12;
                rayDamage = 110;
                break;
        }
    }
    public override IEnumerator CastingRoutine()
    {
        return base.CastingRoutine();
    }
    public override void Casting()
    {
        StartCoroutine("ShotRayRoutine");
    }
    public IEnumerator ShotRayRoutine()
    {
        Monster target = null;
        List<Monster> targetList = null;
        switch (monster.owner)
        {
            case MonsterOwner.Player:
                targetList = BattleManager.Instance.enemyMonster;
                break;
            case MonsterOwner.Enemy:
                targetList = BattleManager.Instance.allyMonster;
                break;
        }
        for(int i = 0; i < rayCount; i++)
        {
            target = targetList[Random.Range(0, targetList.Count)];

            Projectile proj = ObjectPoolManager.Instance.UseObj(ray).GetComponent<Projectile>();
            proj.transform.position = transform.position + Vector3.up * 2;
            proj.transform.rotation = Quaternion.identity;
            proj.SetUp(monster, target, rayDamage, 2, ProjectileMoveType.Indirect);
            proj.projHeight = 20;
            yield return new WaitForSeconds(0.2f);
        }
    }

}
