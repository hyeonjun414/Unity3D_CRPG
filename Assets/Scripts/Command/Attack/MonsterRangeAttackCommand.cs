using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterRangeAttackCommand : MonsterAttackCommand
{
    public GameObject projectile;

    public override void Setup(LivingEntity entity)
    {
        base.Setup(entity);
        projectile = monster.monsterData.projectile;
    }

    public override IEnumerator AttackRoutine()
    {
        monster.isAttacking = true;
        if (monster.range >= CalculateRange(monster, monster.target) && !monster.target.isDead)
        {
            anim.SetTrigger("Attack");
            anim.speed = monster.attackSpeed;
            monster.transform.LookAt(monster.target.transform.position);
            yield return new WaitForSeconds(1f / monster.attackSpeed);
            anim.speed = 1f;
        }
        monster.isAttacking = false;
    }
    public override void Attack()
    {
        base.Attack();

        Shoting(monster, monster.target, 1f / monster.attackSpeed);
    }
    public void Shoting(Monster my, Monster target, float duration)
    {
        Projectile proj = ObjectPoolManager.Instance.UseObj(projectile).GetComponent<Projectile>();
        proj.transform.SetParent(null, true);
        proj.transform.position = projectile.transform.position + monster.transform.position;
        proj.transform.rotation = Quaternion.identity;
        proj.SetUp(my, target, 0.5f, ProjectileMoveType.Direct);
    }

}
