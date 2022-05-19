using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMeleeAttackCommand : MonsterAttackCommand
{

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
        monster.target.Hit(monster.damage);
        monster.curMp += 5;
        UIManager.Instance.battleInfoUI.UpdateUI();
        
    }
}
