using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LivingEntity : MonoBehaviour, IAttackable, IDamageable
{
    [Header("Normal Status")]
    public string entityName;
    public int maxHp;
    public int curHp;
    public int HP
    {
        get { return curHp; }
        set 
        { 
            curHp = value;
            if (curHp <= 0)
            {
                isDead = true;
                curHp = 0;
                Die();
            }
            else if(curHp >= maxHp)
            {
                curHp = maxHp;
            }
        }
    }
    public int maxMp;
    public int curMp;

    [Header("Battle Status")]
    public int damage;
    public int armor;

    [Header("Flag")]
    public bool isDead = false;

    public abstract int Attack();
    public abstract void Hit(int damage);

    public virtual void Die()
    {
        if (!isDead) return;
    }

    public virtual void SetUp()
    {
        curHp = maxHp;
    }
}
