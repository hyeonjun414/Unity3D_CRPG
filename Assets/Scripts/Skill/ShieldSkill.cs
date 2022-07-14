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

    [Header("Level Variable")]
    public int counterDamage;

    public override void SetUp(Monster monster, SkillData sd)
    {
        base.SetUp(monster, sd);

        switch (skillLevel)
        {
            case 0:
                counterDamage = 50;
                break;
            case 1:
                counterDamage = 100;
                break;
            case 2:
                counterDamage = 200;
                break;
        }

        monster.OnHit += HitCounter;
    }
    public override void Casting()
    {
        PlaySfx();
        monster.isCasting = false;
    }

    public void HitCounter(Monster target)
    {
        //monster.HP += damage;
        target.HP -= counterDamage;
        GameManager.Instance.CreateText(counterDamage, target.transform.position, TextType.Counter);
        Effect go = ObjectPoolManager.Instance.UseObj(counterEffect.gameObject).GetComponent<Effect>();
        go.transform.rotation = Quaternion.identity;
        go.transform.SetParent(null, true);
        go.transform.position = target.transform.position + Vector3.up;
        shieldHit.Play();
    }

    protected override void OnDisable()
    {
        monster.OnHit -= HitCounter;
    }
}
