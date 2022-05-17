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
    }
    public override void DeleteCard()
    {
        base.DeleteCard();
    }
}
