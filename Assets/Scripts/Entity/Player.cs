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
    public Vector3 rayPos;

    [Header("Command")]
    public PlayerMoveCommand moveCmd;
    public PlayerDodgeCommand dodgeCmd;
    public PlayerAttackCommand attackCmd;

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
        AddCommand();
    }

    public void AddCommand()
    {
        moveCmd = gameObject.AddComponent<PlayerMoveCommand>();
        moveCmd.Setup(this);
        attackCmd = gameObject.AddComponent<PlayerAttackCommand>();
        attackCmd.Setup(this);

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

    public void Move()
    {
        moveCmd.Excute();
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
        attackCmd.Excute();
        StartCoroutine("AttackDelay");
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
    public override void Hit(float damage)
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
