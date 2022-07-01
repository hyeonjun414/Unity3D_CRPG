using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusUI : MonoBehaviour
{
    Player owner;

    public Text hpText;
    public Text mpText;
    public Text mpRegenText;
    public Text rerollText;
    public Slider hpBar;
    public Slider mpBar;

    public void SetUp(Player player)
    {
        owner = player;
        UpdateUI();
    }

    public void UpdateUI()
    {
        hpText.text = $"{owner.curHp} / {owner.maxHp}";
        mpText.text = $"{owner.curMp} / {owner.maxMp}";
        hpBar.value = (float)owner.curHp / owner.maxHp;
        mpBar.value = (float)owner.curMp / owner.maxMp;
        mpRegenText.text = owner.mpRegen.ToString();
        rerollText.text = owner.rerollCost.ToString();
    }
}
