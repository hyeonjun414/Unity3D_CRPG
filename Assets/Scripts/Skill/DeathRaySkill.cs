using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathRaySkill : Skill
{
    [Header("Ray Prefab")]
    public GameObject ray;


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
        ShotOneRay();
    }
    public void ShotOneRay()
    {
        print("DeathRay Casting");

        Monster target = null;
        List<Monster> targetList = null;
        switch (monster.owner)
        {
            case MonsterOwner.Player:
                targetList = StageManager.Instance.EnemyMonster;
                break;
            case MonsterOwner.Enemy:
                targetList = StageManager.Instance.AllyMonster;
                break;
        }

        target = targetList[Random.Range(0, targetList.Count)];

        Projectile proj = ObjectPoolManager.Instance.UseObj(ray).GetComponent<Projectile>();
        proj.transform.position = transform.position + Vector3.up * 2;
        proj.transform.rotation = Quaternion.identity;
        proj.SetUp(monster, target, 2, ProjectileMoveType.Indirect);
        proj.projHeight = 20;
    }

}
