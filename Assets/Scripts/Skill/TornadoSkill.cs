using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoSkill : Skill
{
    [Header("Tornado")]
    public GameObject tornadoEffect;
    public Effect hitEffect;

    [Header("Level Variable")]
    public int hitDamage;
    public int hitCount;
    public float hitInterval;
    

    public override void SetUp(Monster monster, SkillData sd)
    {
        base.SetUp(monster, sd);

        switch (skillLevel)
        {
            case 0:
                hitDamage = 10;
                hitCount = 5;
                hitInterval = 0.5f;
                break;
            case 1:
                hitDamage = 15;
                hitCount = 10;
                hitInterval = 0.3f;
                break;
            case 2:
                hitDamage = 20;
                hitCount = 15;
                hitInterval = 0.1f;
                break;
        }
    }
    public override void Casting()
    {
        StartCoroutine(TornadoRoutine());
    }
    IEnumerator TornadoRoutine()
    {
        tornadoEffect.SetActive(true);
        tornadoEffect.transform.SetParent(null);
        tornadoEffect.transform.rotation = Quaternion.identity;
        tornadoEffect.transform.position = monster.target.curTile.transform.position;
        for(int i = 0; i < hitCount; i++)
        {
            if (monster.target == null) break;
            Vector3 lookPos = monster.target.transform.position;
            lookPos.y = 0;
            monster.transform.rotation = Quaternion.LookRotation(lookPos);
            GameObject go = ObjectPoolManager.Instance.UseObj(hitEffect.gameObject);
            go.transform.SetParent(null, true);
            go.transform.position = monster.target.transform.position + Vector3.up;
            monster.target.Hit(hitDamage);
            PlaySfx();
            yield return new WaitForSeconds(hitInterval);
        }
        tornadoEffect.SetActive(false);
        tornadoEffect.transform.SetParent(gameObject.transform);
    }

    protected override void OnDisable()
    {
        tornadoEffect.SetActive(false);
    }

}
