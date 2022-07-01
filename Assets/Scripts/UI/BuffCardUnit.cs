using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffCardUnit : CardUnit
{
    [Header("BuffCard UI")]
    public Text descText;
    public override void AddCard(CardData data)
    {
        base.AddCard(data);
        SpellData bd = (SpellData)data;
        descText.text = bd.desc;
    }
    public override void DeleteCard()
    {
        base.DeleteCard();
    }
}
