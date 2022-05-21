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
        GameObject go = Instantiate(counterEffect, Vector3.zero, Quaternion.identity);
        shieldHit.Play();
        go.transform.SetParent(target.transform, true);
        go.transform.position += Vector3.up;
        Destroy(go, 2f);
    }

    private void OnDestroy()
    {
        monster.OnHit -= HitCounter;
    }
}
