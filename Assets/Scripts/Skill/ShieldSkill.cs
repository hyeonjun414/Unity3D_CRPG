using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldSkill : Skill
{
    [Header("Shield")]
    public ParticleSystem shieldLoop;
    public ParticleSystem shieldHit;

    [Header("Counter Effect")]
    public GameObject counterEffect;

    public override void SetUp(Monster monster, SkillData sd)
    {
        base.SetUp(monster, sd);
        monster.OnHit += HitCounter;
    }
    public override void Casting()
    {
        monster.isCasting = false;
    }

    public void HitCounter(Monster target)
    {
        int damage = target.damage;
        monster.HP += damage;
        target.HP -= damage;
        GameManager.Instance.CreateText(damage, target.transform.position, TextType.Counter);
        Effect go = ObjectPoolManager.Instance.UseObj(counterEffect.gameObject).GetComponent<Effect>();
        go.transform.position = Vector3.zero;
        go.transform.rotation = Quaternion.identity;
        go.transform.SetParent(target.transform, false);
        shieldHit.Play();
        go.transform.position += Vector3.up;
    }

    protected override void OnDisable()
    {
        monster.OnHit -= HitCounter;
    }
}
