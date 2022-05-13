using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : LivingEntity
{
    private static Player instance = null;
    [Header("Player")]
    public float maxMp;
    public float curMp;
    public float mpRegenerate = 0.1f;

    [Header("Projectile")]
    public Projectile projectile;
    public Transform projPos;

    private Camera mainCamera; // 메인 카메라
    private Animator anim;
    private Vector3 rayPos;
    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        SceneManager.sceneLoaded += FindingMainCam;
    }
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    public void FindingMainCam(Scene scene, LoadSceneMode mode)
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        InputMouse();

        Move();

        Looting();


    }
    private void FixedUpdate()
    {
        GetMouseDir();
    }
    void InputMouse()
    {
        if(Input.GetMouseButton(1))
        {
            Attack();
        }
    }

    public void GetMouseDir()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 200, LayerMask.GetMask("Ground")))
        {
            rayPos = raycastHit.point;
            rayPos.y = transform.position.y;
        }
    }

    void Move()
    {

        Vector3 dir = Vector3.zero;

        if(!isAttack)
        {
            if (Input.GetKey(KeyCode.W))
            {
                dir += Vector3.forward;
            }
            if (Input.GetKey(KeyCode.S))
            {
                dir += Vector3.back;
            }
            if (Input.GetKey(KeyCode.A))
            {
                dir += Vector3.left;
            }
            if (Input.GetKey(KeyCode.D))
            {
                dir += Vector3.right;
            }

            dir = Quaternion.Euler(0, 30, 0) * dir.normalized;

            transform.Translate(dir * moveSpeed * Time.deltaTime, Space.World);

        }

        if (dir.sqrMagnitude > 0.2f)
        {
            anim.SetBool("Run", true);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), roteSpeed*Time.deltaTime);
        }
        else
            anim.SetBool("Run", false);


        
    }
    public override IEnumerator AttackDelay()
    {
        isAttack = true;
        anim.SetBool("isAttack", isAttack);
        yield return new WaitForSeconds(attackDelayTime);
        isAttack = false;
        anim.SetBool("isAttack", isAttack);
    }

    public override void Attack()
    {
        if (isAttack) return;

        Collider[] detects = Physics.OverlapSphere(transform.position, 5f, LayerMask.GetMask("Enemy"));
        if(detects.Length > 0)
        {
            float minDist = 5f;
            Vector3 targetPos = rayPos;
            for (int i = 0; i < detects.Length; i++)
            {
                float dist = Vector3.Distance(transform.position, detects[i].transform.position);
                if (Vector3.Distance(transform.position, detects[i].transform.position) < minDist)
                {
                    minDist = dist;
                    targetPos = detects[i].transform.position;
                }
            }
            targetPos = (targetPos - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(targetPos);
        }
        else
        {
            transform.LookAt(rayPos);
        }
        
        anim.SetTrigger("Attack");
        StartCoroutine("AttackDelay");
        Instantiate(GameManager.Instance.mouseVfx, rayPos + Vector3.up * 0.1f, Quaternion.identity);
        if (projectile != null)
            Instantiate(projectile, projPos.position + transform.forward, transform.rotation);
    }

    public void Looting()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, 3f, LayerMask.GetMask("Reward"));
            if(hits.Length > 0)
            {
                RewardItem item = null;
                foreach (Collider hit in hits)
                {
                    item = hit.gameObject.GetComponent<RewardItem>();
                    item.RewardGet();
                }
            }
        }
    }
    public void MpReGenarate()
    {
        if (curMp >= maxMp) return;

        curMp += mpRegenerate * Time.deltaTime;
        

    }
    public override void Hit(int damage)
    {
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 5f);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 5f);
    }
}
