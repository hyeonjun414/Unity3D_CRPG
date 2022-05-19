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
        monster.curMp += 5;
        
        Shoting(monster, monster.target, 1f / monster.attackSpeed);
    }
    public void Shoting(Monster my, Monster target, float duration)
    {
        Projectile proj = Instantiate(projectile, projectile.transform.position + monster.transform.position, Quaternion.identity).GetComponent<Projectile>();
        proj.SetUp(my, target, duration);
    }
}
