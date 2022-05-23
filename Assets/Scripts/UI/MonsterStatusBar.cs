using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterStatusBar : MonoBehaviour
{
    [Header("Monster Status Bar")]
    public Monster monster;
    public Text monsterName;
    public Slider hpBar;
    public Slider mpBar;

    private void Update()
    {
        transform.LookAt(-Camera.main.transform.position);
    }
    public void AddMonster(Monster monster)
    {
        this.monster = monster;
        gameObject.SetActive(true);
    }
    public void UpdateUI()
    {
        monsterName.text = monster.entityName;
        hpBar.value = (float)monster.curHp / monster.maxHp;
        mpBar.value = (float)monster.curMp / monster.maxMp;

    }
    public void ResetUI()
    {
        gameObject.SetActive(false);
    }
}
