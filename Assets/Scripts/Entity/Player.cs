using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : LivingEntity
{
    private static Player instance = null;
    [Header("Player")]

    public float moveSpeed;
    public float roteSpeed;

    private Animator anim;
    public Vector3 rayPos;

    public Transform linePos;

    [Header("Command")]
    public PlayerMoveCommand moveCmd;

    private StatusUI statusUI;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
        {
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += ResetPosition;
            instance = this;
        }
        

        
    }
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        AddCommand();
        statusUI = UIManager.Instance.statusUI;
        SetUp();
    }

    public void AddCommand()
    {
        moveCmd = gameObject.AddComponent<PlayerMoveCommand>();
        moveCmd.Setup(this);

    }

    public void ResetPosition(Scene scene, LoadSceneMode mode)
    {
        transform.position = new Vector3(-12, 0.5f, 0);
    }

    void Update()
    {

        Move();

        Looting();

    }

    public void Move()
    {
        moveCmd.Excute();
    }

    public override LivingEntity Attack()
    {
        anim.SetTrigger("Attack");
        return this;
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
    public override void Hit(LivingEntity entity)
    {
        HP -= entity.damage;
        anim.SetTrigger("Hit");
        GameManager.Instance.CreateText(entity.damage, transform.position, TextType.Damage);
    }
}
