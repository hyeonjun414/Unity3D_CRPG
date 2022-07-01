using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleInfoUnit : MonoBehaviour
{
    [Header("Monster Info")]
    public Text infoName;
    public Text hpText;
    public Text mpText;
    public Slider hpBar;
    public Slider mpBar;

    public void AddMonster(Monster monster)
    {
    }
    public void UpdateUI(Monster monster)
    {
        gameObject.SetActive(true);
        infoName.text = monster.entityName;
        hpText.text = $"{monster.curHp} / {monster.maxHp}";
        mpText.text = $"{monster.curMp} / {monster.maxMp}";
        hpBar.value = (float)monster.curHp / monster.maxHp;
        mpBar.value = (float)monster.curMp / monster.maxMp;

}
    public void ResetUI()
    {
        gameObject.SetActive(false);
    }
}
