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
    Range,
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

    public MonsterType  type;
    public MonsterState state;
    public MonsterOwner owner;
    public MonsterLevel level;
    public MonsterData monsterData;

    [Header("BattleTile")]
    public BattleTile curTile;
    public BattleTile nextTile;
    public Monster target;
    

    [Header("Effect")]
    public GameObject allyEffect;
    public GameObject enemyEffect;

    [Header("Command")]
    public MoveCommand moveCommand;
    public AttackCommand attackCommand;
    public FindCommand findCommand;
    public SkillCommand skillCommand;
    public bool isAttacking = false;
    public bool isMoving= false;
    public bool isCasting = false;

    [Header("UI")]
    public MonsterStatusBar statusBar;

    public Animator anim;
    public Collider cc;
    private Rigidbody rb;

    public override void SetUp()
    {
        base.SetUp();
        anim = GetComponentInChildren<Animator>();
        cc = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();


        // 소유자에 따라 이펙트를 활성화한다.
        if (owner == MonsterOwner.Player)
        {
            allyEffect.gameObject.SetActive(true);
        }
        else
        {
            enemyEffect.gameObject.SetActive(true);
        }

        findCommand = anim.gameObject.AddComponent<ClosestTargetFindCommand>();
        findCommand.Setup(this);

        moveCommand = anim.gameObject.AddComponent<MonsterMoveCommand>();
        moveCommand.Setup(this);
        if(range > 1)
        {
            attackCommand = anim.gameObject.AddComponent<MonsterRangeAttackCommand>();
            attackCommand.Setup(this);
        }
        else
        {
            attackCommand = anim.gameObject.AddComponent<MonsterMeleeAttackCommand>();
            attackCommand.Setup(this);
        }
        if(monsterData.skillData != null)
        {
            skillCommand = anim.gameObject.AddComponent<MonsterSkillCommand>();
            skillCommand.Setup(this);
        }


        statusBar?.AddMonster(this);
        statusBar?.UpdateUI();
        
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
        statusBar?.UpdateUI();
        GameManager.Instance.CreateDamage(damage, transform.position);
    }
    public void FindTurn()
    {
        findCommand.Excute();
    }
    public void AttackTurn()
    {
        if(MP == maxMp && skillCommand != null)
        {
            skillCommand?.Excute();
        }
        else
        {
            attackCommand.Excute();
        }
    }
    public void MoveTurn()
    {
        moveCommand.Excute();
    }
    public bool IsActing()
    {
        return isMoving || isAttacking;
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
