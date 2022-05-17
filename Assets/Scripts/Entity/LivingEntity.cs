using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LivingEntity : MonoBehaviour, IAttackable, IDamageable
{
    [Header("Normal Status")]
    public int maxHp;
    public int _curHp;
    public int HP
    {
        get { return _curHp; }
        set 
        { 
            _curHp = value;
            if (_curHp <= 0)
            {
                isDead = true;
                _curHp = 0;
                Die();
            }
            else if(_curHp >= maxHp)
            {
                _curHp = maxHp;
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
        
        Destroy(gameObject);
    }

    public virtual void SetUp()
    {
        _curHp = maxHp;
    }
}
