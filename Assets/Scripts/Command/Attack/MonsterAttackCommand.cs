using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterAttackCommand : AttackCommand
{
    private Monster monster;
    private Animator anim;
    public override void Setup(LivingEntity entity)
    {
        monster = (Monster)entity;
        anim = GetComponentInChildren<Animator>();
    }
    public override void Excute()
    {
        if (monster.target == null || monster.isAttacking || monster.target.nextTile != null)
        {
            return;
        }
        StartCoroutine("AttackRoutine");
    }

    public IEnumerator AttackRoutine()
    {
        monster.isAttacking = true;
        if (monster.range >= CalculateRange(monster, monster.target) && !monster.target.isDead)
        {
            anim.SetTrigger("Attack");
            anim.speed = monster.attackSpeed;
            monster.target.Hit(monster.damage);
            UIManager.Instance.battleInfoUI.UpdateUI();
            transform.LookAt(monster.target.transform.position);
            yield return new WaitForSeconds(1f / monster.attackSpeed);
            anim.speed = 1f;
        }
        monster.isAttacking = false;
        
    }
    public int CalculateRange(Monster my, Monster target)
    {
        Vector2 start = my.curTile.tilePos;
        Vector2 end = target.curTile.tilePos;

        int xSize = (int)Mathf.Abs(start.x - end.x);
        int ySize = (int)Mathf.Abs(start.y - end.y);
        int line = Mathf.Abs(xSize - ySize);
        int cross = xSize > ySize ? xSize - line : ySize - line;

        return line + cross;
    }
}
