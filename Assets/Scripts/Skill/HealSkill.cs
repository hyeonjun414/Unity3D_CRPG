using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealSkill : Skill
{
    public GameObject HealEffect;

    [Header("Level Variable")]
    public int healCount;
    public int healAmount;


    public override void SetUp(Monster monster, SkillData sd)
    {
        base.SetUp(monster, sd);

        switch (skillLevel)
        {
            case 0:
                healCount = 1;
                healAmount = 50;
                break;
            case 1:
                healCount = 2;
                healAmount = 100;
                break;
            case 2:
                healCount = 2;
                healAmount = 200;
                break;
        }

        StartCoroutine("CastingRoutine");

    }
    public override void Casting()
    {
        StartCoroutine("HealRoutine");
    }
    public IEnumerator HealRoutine()
    {
        List<Monster> list;
        if(monster.owner == MonsterOwner.Player)
        {
            list = BattleManager.Instance.allyMonster;
        }
        else
        {
            list = BattleManager.Instance.enemyMonster;
        }
        for(int i = 0; i < healCount; i++)
        {
            foreach (Monster m in list)
            {
                m.HP += healAmount;
                GameManager.Instance.CreateText(50, m.transform.position, TextType.Heal);
                Effect healEffect = ObjectPoolManager.Instance.UseObj(HealEffect).GetComponent<Effect>();
                healEffect.transform.SetParent(null, true);
                healEffect.transform.position = m.transform.position + Vector3.up * 0.5f;
                healEffect.transform.rotation = Quaternion.identity;
            }
            yield return new WaitForSeconds(1f);
        }
        
        UIManager.Instance.battleInfoUI.UpdateUI();
    }
}
