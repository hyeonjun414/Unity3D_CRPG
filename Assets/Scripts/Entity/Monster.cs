using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public enum MonsterState
{
    Idle,
    Move,
    Attack
}
public enum MonsterOwner
{
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
    End
}
public enum TargetStrategy
{
    Closest,
    Farthest,
    LowHp,
    HighHp,
    Random
    
}
public class Monster : LivingEntity, IPoolable
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
    public bool isCC = false;
    public bool isVanished = false;

    [Header("Action")]
    public UnityAction<Monster> OnAttack;
    public UnityAction<Monster> OnHit;

    [Header("Skill")]
    public Skill skill;


    [Header("UI")]
    public MonsterStatusBar statusBar;

    public Animator anim;
    public Collider cc;
    private Rigidbody rb;

    public override void SetUp()
    {
        base.SetUp();
        isAttacking = false;
        isMoving = false;
        isCasting = false;
        isCC = false;
        isVanished = false;

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

        if(findCommand == null)
        {
            findCommand = anim.gameObject.AddComponent<ClosestTargetFindCommand>();
            findCommand.Setup(this);
        }
    
        if(moveCommand == null)
        {
            moveCommand = anim.gameObject.AddComponent<MonsterMoveCommand>();
            moveCommand.Setup(this);
        }

        if(attackCommand == null)
        {
            if (range > 1)
            {
                attackCommand = anim.gameObject.AddComponent<MonsterRangeAttackCommand>();
                attackCommand.Setup(this);
            }
            else
            {
                attackCommand = anim.gameObject.AddComponent<MonsterMeleeAttackCommand>();
                attackCommand.Setup(this);
            }
            
        }
        if (monsterData.skillData != null)
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
        level = data.level;


        SetUp();
    }

    public override LivingEntity Attack()
    {
        OnAttack?.Invoke(this);
        return this;
    }

    public override void Hit(LivingEntity enemy)
    {
        OnHit?.Invoke((Monster)enemy);
        HP -= GameManager.Instance.ApplyRandomValue(enemy.damage);
        MP += 3;
        
        statusBar?.UpdateUI();
        GameManager.Instance.CreateText(enemy.damage, transform.position, TextType.Damage);
    }
    public void Hit(int damage)
    {
        HP -= damage;
        MP += 3;

        statusBar?.UpdateUI();
        GameManager.Instance.CreateText(damage, transform.position, TextType.Damage);
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
        Gizmos.DrawWireCube(transform.position, new Vector3(1f, 0.1f, 1f)* (range * 2 +1));
    }

    public void ReturnPool()
    {
        if (skill != null)
        {
            skill.ReturnPool();
            skill = null;
        }
        ResetMonster();
        gameObject.SetActive(false);
        ObjectPoolManager.Instance.ReturnObj(gameObject);
    }

    public void ResetMonster()
    {
        target = null;
        allyEffect.gameObject.SetActive(false);
        enemyEffect.gameObject.SetActive(false);
        statusBar.gameObject.SetActive(false);
    }
    public void ApplyCC(float duration)
    {
        StopCoroutine("CCRoutine");
        StartCoroutine("CCRoutine", duration);
    }
    IEnumerator CCRoutine(float duration)
    {
        isCC = true;
        yield return new WaitForSeconds(duration);
        isCC = false;
    }

}
