using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LivingEntity : MonoBehaviour, IAttackable, IDamageable
{
    [Header("Normal Status")]
    public float maxHp;
    public float _curHp;
    public float HP
    {
        get { return _curHp; }
        set 
        { 
            _curHp = value;
            if (_curHp <= 0)
            {
                isDead = true;
                Die();
            }
            else if(_curHp >= maxHp)
            {
                _curHp = maxHp;
            }
        }
    }
    public float moveSpeed;
    public float roteSpeed;

    [Header("Battle Status")]
    public float damage;
    public float armor;
    public float attackDelayTime;
    public float hitDelayTime;

    [Header("Flag")]
    public bool isAttack = false;
    public bool isHit= false;
    public bool isDead = false;

    public abstract void Attack();

    public virtual IEnumerator AttackDelay()
    {
        isAttack = true;
        yield return new WaitForSeconds(attackDelayTime);
        isAttack = false;
    }

    public abstract void Hit(float damage);

    public virtual IEnumerator HitDelay()
    {
        isHit = true;
        yield return new WaitForSeconds(hitDelayTime);
        isHit = false;
    }

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
