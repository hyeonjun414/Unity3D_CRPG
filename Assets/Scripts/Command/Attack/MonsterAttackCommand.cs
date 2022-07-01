using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterAttackCommand : AttackCommand
{
    protected Monster monster;
    protected Animator anim;
    public override void Setup(LivingEntity entity)
    {
        monster = (Monster)entity;
        anim = GetComponentInChildren<Animator>();
    }
    public override void Excute()
    {
        if (monster.target == null || monster.target.isVanished || monster.isAttacking || monster.isCasting 
            || monster.target.nextTile != null || monster.isCC)
        {
            return;
        }
        StartCoroutine("AttackRoutine");
    }
    public virtual void Attack()
    {
        monster.MP += 5;
        monster.statusBar?.UpdateUI();
    }
    public virtual IEnumerator AttackRoutine()
    {
        yield return null;
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
