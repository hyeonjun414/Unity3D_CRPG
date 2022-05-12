using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LivingEntity : MonoBehaviour, IAttackable, IDamageable
{
    [Header("Normal Status")]
    public string entityName;
    public float maxHp;
    public float curHp;
    public float moveSpeed;
    public float roteSpeed;

    [Header("Battle Status")]
    public float damage;
    public float armor;
    public float attackDelayTime;
    public float hitDelayTime;

    public bool isAttack = false;
    public bool isHit= false;

    public abstract void Attack();

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
