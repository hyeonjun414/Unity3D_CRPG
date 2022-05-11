using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LivingEntity : MonoBehaviour, IAttackable, IDamageable
{
    public string entityName;
    public float maxHp;
    public float curHp;
    public float moveSpeed;
    public float roteSpeed;
    public float damage;
    public float armor;

    public float attackDelayTime;
    public float hitDelayTime;

    public bool isAttack = false;
    public bool isHit= false;

    public abstract void Attack(int damage);

    public virtual IEnumerator AttackDelay()
    {
        isAttack = true;
        yield return new WaitForSeconds(attackDelayTime);
         isAttack = false;
    }

    public abstract void Hit(int damage);

    public virtual IEnumerator HitDelay()
    {
        isHit = true;
        yield return new WaitForSeconds(hitDelayTime);
        isHit = false;
    }
}
