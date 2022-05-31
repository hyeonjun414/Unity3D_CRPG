using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoSkill : Skill
{
    [Header("Tornado")]
    public GameObject tornadoEffect;

    public override void SetUp(Monster monster, SkillData sd)
    {
        base.SetUp(monster, sd);
    }
    public override void Casting()
    {
        FloatingTarget();
    }
    public void FloatingTarget()
    {
        StartCoroutine(FloatingRoutine(2f));
    }
    IEnumerator FloatingRoutine(float time)
    {
        tornadoEffect.SetActive(true);
        tornadoEffect.transform.SetParent(null);
        tornadoEffect.transform.rotation = Quaternion.identity;
        tornadoEffect.transform.position = monster.target.curTile.transform.position;
        for(int i = 0; i < 10; i++)
        {
            monster.transform.LookAt(monster.target.transform);
            monster.target.Hit(monster);
            yield return new WaitForSeconds(0.2f);
        }
        tornadoEffect.SetActive(false);
        tornadoEffect.transform.SetParent(gameObject.transform);
    }

    protected override void OnDisable()
    {
        tornadoEffect.SetActive(false);
    }

}
