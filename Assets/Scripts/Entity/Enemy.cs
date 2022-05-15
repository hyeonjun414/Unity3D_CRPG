using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : LivingEntity
{
    [Header("Enemy")]
    public Transform target = null;
    public float detectRaduis = 5f;
    public float attackRange = 1f;
    public bool isTrace;
    public float idleDelay = 2f;
    public float idleDuration = 2f;
    public float traceSpeed = 3f;


    private Animator anim;
    private SphereCollider sc;
    private CapsuleCollider cc;
    private Rigidbody rb;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        sc = GetComponent<SphereCollider>();
        cc = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
        sc.radius = detectRaduis;
        

        StartCoroutine("EnemyIdleRoutine");
        sc.enabled = true;
        SetUp();
    }


    private void Update()
    {
        if (isDead) return;

        Trace();
    }
    public void Trace()
    {
        if (!isTrace || isAttack)
        {
            return;
        }

        if (Vector3.Distance(target.position, transform.position) < attackRange)
        {
            if(!isAttack)
                Attack();
            return;
        }

        Vector3 dir = (target.position - transform.position).normalized;

        transform.Translate(dir * traceSpeed * Time.deltaTime, Space.World);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), roteSpeed*Time.deltaTime);

    }

    public override void Attack()
    {
        StopCoroutine("EnemyIdleRoutine");
        StartCoroutine("AttackDelay");
        transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
        anim.SetTrigger("Attack");
    }

    public override void Hit(float damage)
    {
        HP -= damage;
        GameManager.Instance.CreateDamage((int)damage, transform.position);
        if (isDead) return;

        anim.SetTrigger("Hit");
    }

    public override void Die()
    {
        base.Die();
        StopAllCoroutines();
        anim.SetTrigger("Die");
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        cc.enabled = false;
        sc.enabled = false;
    }


    IEnumerator EnemyIdleRoutine()
    {
        float curTime = Time.time;
        float x = Random.Range(-1f, 1f);
        float z = Random.Range(-1f, 1f);

        anim.SetBool("IsMove", true);

        Vector3 dir = new Vector3(x, 0, z).normalized;

        while(curTime+idleDuration > Time.time)
        {
            transform.Translate(dir * moveSpeed * Time.deltaTime, Space.World);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), roteSpeed * Time.deltaTime);
            yield return null;
        }

        anim.SetBool("IsMove", false);

        yield return new WaitForSeconds(idleDelay);
        
        StartCoroutine("EnemyIdleRoutine");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") == true)
        {
            target = other.gameObject.transform;
            isTrace = true;
            detectRaduis *= 2f;
            sc.radius = detectRaduis;
            anim.SetBool("IsMove", false);
            anim.SetBool("IsTrace", isTrace);
            StopCoroutine("EnemyIdleRoutine");

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            target = null;
            isTrace = false;
            detectRaduis *= 0.5f;
            sc.radius = detectRaduis;
            anim.SetBool("IsTrace", isTrace);
            StartCoroutine("EnemyIdleRoutine");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectRaduis);
    }
}
