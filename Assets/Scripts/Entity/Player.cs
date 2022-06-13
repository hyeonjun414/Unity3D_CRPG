﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : LivingEntity
{
    public static Player Instance = null;
    [Header("Player")]
    public int mpRegen = 10;
    public int rerollCost = 5;
    public float moveSpeed;
    public float roteSpeed;

    private Animator anim;
    public Vector3 rayPos;

    public Transform linePos;

    [Header("Command")]
    public PlayerMoveCommand moveCmd;

    public StatusUI statusUI;

    private void Awake()
    {
        if (Instance != null)
            DestroyImmediate(gameObject);
        else
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        

        
    }
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        AddCommand();
        
        SetUp();
        statusUI = UIManager.Instance.statusUI;
        statusUI.SetUp(this);

        GameManager.Instance.player = this;
    }

    public override void SetUp()
    {
        base.SetUp();
        curMp = maxMp;
    }
    public void AddCommand()
    {
        moveCmd = gameObject.AddComponent<PlayerMoveCommand>();
        moveCmd.Setup(this);

    }

    public void ResetPlayer(Scene scene, LoadSceneMode mode)
    {
        //if (scene.name != "SceneChangeTest") return;
        transform.position = new Vector3(-14, 0.5f, 0);
        MP = maxMp;
        statusUI.UpdateUI();
    }

    void Update()
    {

        Move();

        Interaction();
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

    public void Interaction()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, 3f, LayerMask.GetMask("Reward"));
            if(hits.Length > 0)
            {
                Item item = null;
                foreach (Collider hit in hits)
                {
                    item = hit.gameObject.GetComponent<Item>();
                    item.RewardGet();
                    return;
                }
            }

            hits = Physics.OverlapSphere(transform.position, 3f, LayerMask.GetMask("Shop"));
            if (hits.Length > 0)
            {
                Shop shop = null;
                foreach (Collider hit in hits)
                {
                    shop = hit.gameObject.GetComponent<Shop>();
                    shop.OpenShop();
                    return;
                }
            }
            hits = Physics.OverlapSphere(transform.position, 3f, LayerMask.GetMask("Event"));
            if (hits.Length > 0)
            {
                EventObj obj = null;
                foreach (Collider hit in hits)
                {
                    obj = hit.gameObject.GetComponent<EventObj>();
                    obj.PlayEvent();
                    return;
                }
            }

            hits = Physics.OverlapSphere(transform.position, 3f, LayerMask.GetMask("Rest"));
            if (hits.Length > 0)
            {
                RestObj obj = null;
                foreach (Collider hit in hits)
                {
                    obj = hit.gameObject.GetComponent<RestObj>();
                    obj.Resting(this);
                    return;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Reward"))
        {
            UIManager.Instance.keyUI.ItemEnter();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Reward"))
        {
            UIManager.Instance.keyUI.ItemExit();
        }
    }
    public void UseMp(int cost)
    {
        MP -= cost;
        statusUI.UpdateUI();
    }
    public void RegenMp()
    {
        MP += mpRegen;
        statusUI.UpdateUI();
    }
    public override void Hit(LivingEntity entity)
    {
        HP -= entity.damage;
        anim.SetTrigger("Hit");
        statusUI.UpdateUI();
        GameManager.Instance.CreateText(entity.damage, transform.position, TextType.Damage);
    }
    public void Hit(int damage)
    {
        HP -= damage;
        anim.SetTrigger("Hit");
        statusUI.UpdateUI();
        GameManager.Instance.CreateText(damage, transform.position, TextType.Damage);
    }
    public void Heal(int value)
    {
        HP += value;
        statusUI.UpdateUI();
        GameManager.Instance.CreateText(value, transform.position, TextType.Heal);
    }
}
