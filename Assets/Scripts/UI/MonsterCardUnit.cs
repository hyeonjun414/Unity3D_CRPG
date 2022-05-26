using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterCardUnit : CardUnit
{
    [Header("Monster Unit")]
    public Text hp;
    public Text mp;
    public Text damage;
    public Text armor;
    public Text range;
    public Text attackSpeed;

    public Transform monsterPos;
    public Monster monster;

    public override void AddCard(CardData data)
    {
        base.AddCard(data);
        MonsterData md = (MonsterData)data;
        hp.text = md.hp.ToString();
        mp.text = md.mp.ToString();
        damage.text = md.damage.ToString();
        armor.text = md.armor.ToString();
        range.text = md.range.ToString();
        attackSpeed.text = md.attackSpeed.ToString();

        if (monster != null &&
            monster.monsterData.name != md.name ||
            (monster != null && monster.monsterData.name == md.name &&
            monster.monsterData.level != md.level))
        {
            //Destroy(monster.gameObject);
            monster.ReturnPool();
            monster = null;
        }
        if (monster == null)
        {
            monster = ObjectPoolManager.Instance.UseObj(md.monster).GetComponent<Monster>();
            monster.transform.SetParent(monsterPos, true);
            monster.transform.localScale = new Vector3(1,1,0.1f);
            monster.transform.position = monsterPos.position;
            monster.transform.rotation = Quaternion.LookRotation(-monsterPos.transform.forward);
            monster.monsterData = md;
        }
    }
    public override void DeleteCard()
    {
        base.DeleteCard();
        
    }
}
