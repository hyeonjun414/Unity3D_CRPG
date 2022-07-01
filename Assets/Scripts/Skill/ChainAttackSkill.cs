using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChainAttackSkill : Skill
{
    [Header("ChainAttack")]
    public GameObject hitEffect;
    public LineRenderer lr;

    [Header("Level Variable")]
    public int chainCount;
    public int chainDamage;

    public override void SetUp(Monster monster, SkillData sd)
    {
        lr = GetComponent<LineRenderer>();
        
        base.SetUp(monster, sd);
        monster.OnAttack += ChainAttack;

        switch(skillLevel)
        {
            case 0:
                chainCount = 2;
                chainDamage = 20;
                break;
            case 1:
                chainCount = 3;
                chainDamage = 40;
                break;
            case 2:
                chainCount = 4;
                chainDamage = 60;
                break;
        }

    }

    public override void Casting()
    {
        monster.isCasting = false;
    }

    public void ChainAttack(Monster owner)
    {
        StartCoroutine("ChainAttackRoutine", owner);
    }
    public IEnumerator ChainAttackRoutine(Monster owner)
    {
        lr.enabled = true;
        Monster target = null;
        string targetLayer = null;
        switch (monster.owner)
        {
            case MonsterOwner.Player:
                targetLayer = "Enemy";
                break;
            case MonsterOwner.Enemy:
                targetLayer = "Ally";
                break;
        }
        List<Collider> hits = Physics.OverlapBox(transform.position, Vector3.one * (monster.range * 10 + 1), Quaternion.identity, LayerMask.GetMask(targetLayer)).ToList();
        
        hits.Sort(delegate (Collider a, Collider b)
        {
            if (Vector3.Distance(monster.transform.position, a.transform.position) <= Vector3.Distance(monster.transform.position, b.transform.position))
            {
                return 1;
            }
            else
                return -1;
        });
        if (hits.Count > 0)
        {
            
            lr.positionCount = 1;
            lr.SetPosition(0, transform.position + Vector3.up);
            for (int i = 0; i < hits.Count; i++)
            {
                if (i > chainCount) break;

                lr.positionCount++;
                target = hits[i].gameObject.GetComponent<Monster>();
                lr.SetPosition(i+1, target.transform.position + Vector3.up);
                target.Hit(chainDamage);
                Effect hit = ObjectPoolManager.Instance.UseObj(hitEffect).GetComponent<Effect>();
                hit.transform.SetParent(null, true);
                hit.transform.position = target.transform.position + Vector3.up;
                yield return null;

            }

        }
        lr.enabled = false;

    }
    protected override void OnDisable()
    {
        StopCoroutine("ChainAttackRoutine");
        monster.OnAttack -= ChainAttack;
        lr.enabled = false;
    }

}
