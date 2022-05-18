using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterState
{
    Idle,
    Move,
    Attack
}
public enum MonsterOwner
{
    None,
    Player,
    Enemy
}
public enum MonsterType
{
    Melee,
    Rangefarther
}
public enum MonsterLevel
{
    LV1,
    LV2,
    LV3,
}
public enum TargetStrategy
{
    Closest,
    Farthest,
    LowHp,
    HighHp,
    Random
    
}
public class Monster : LivingEntity
{
    [Header("Monster Status")]
    public float attackSpeed;
    public float moveSpeed = 1f;
    public int range;

    public MonsterState state;
    public MonsterOwner owner;
    public MonsterLevel level;
    public MonsterData monsterData;

    [Header("BattleTile")]
    public BattleTile returnTile;
    public Monster target;
    public BattleTile curTile;

    [Header("Effect")]
    public GameObject allyEffect;
    public GameObject enemyEffect;

    [Header("Command")]
    public MoveCommand moveCommand;
    public AttackCommand attackCommand;
    public FindCommand findCommand;
    public bool isAttacking = false;
    public bool isMoving= false;

    public Animator anim;
    public Collider cc;
    private Rigidbody rb;

    public override void SetUp()
    {
        base.SetUp();
        anim = GetComponentInChildren<Animator>();
        cc = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();

        // 현재 타일을 돌아올 타일로 설정해준다.
        curTile = returnTile;

        // 소유자에 따라 이펙트를 활성화한다.
        if (owner == MonsterOwner.Player)
        {
            allyEffect.gameObject.SetActive(true);
        }
        else
        {
            enemyEffect.gameObject.SetActive(true);
        }

        moveCommand = gameObject.AddComponent<MonsterMoveCommand>();
        moveCommand.Setup(this);
        attackCommand = gameObject.AddComponent<MonsterAttackCommand>();
        attackCommand.Setup(this);
        findCommand = gameObject.AddComponent<ClosestTargetFindCommand>();
        findCommand.Setup(this);
    }
    
    public void InputData(MonsterData data)
    {
        // 데이터에 맞춰서 몬스터의 초기설정을 한다.
        monsterData = data;
        entityName = data.name;
        maxHp = data.hp;
        maxMp = data.mp;
        damage = data.damage;
        armor = data.armor;
        range = data.range;
        attackSpeed = data.attackSpeed;
        moveSpeed = data.moveSpeed;

        SetUp();
    }

    public override int Attack()
    {
        return damage;
    }

    public override void Hit(int damage)
    {
        HP -= damage;
        GameManager.Instance.CreateDamage(damage, transform.position);
    }
    public void FindTurn()
    {
        findCommand.Excute();
    }
    public void AttackTurn()
    {
        attackCommand.Excute();
    }
    public void MoveTurn()
    {
        moveCommand.Excute();
    }
    public void BattleStart()
    {
        findCommand.Excute();
        if (target == null) return;
        attackCommand.Excute();
    }
    public void BattleEnd()
    {
        StopAllCoroutines();
        if(owner == MonsterOwner.Player)
        {
            Destroy(gameObject, 1f);
            CardManager.Instance.MoveCard(CardSpace.Field, CardSpace.Graveyard, monsterData);
            anim.SetTrigger("Die");
        }
        else
        {
            Destroy(gameObject, 1f);
            anim.SetTrigger("Die");
        }
    }

    public override void Die()
    {
        base.Die();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, Vector3.one * range);
    }
}
