using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleInfoUI : MonoBehaviour
{
    public BattleInfoPanel allyPanel;
    public BattleInfoPanel enemyPanel;

    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void UpdateUI()
    {
        allyPanel.UpdateUI();
        enemyPanel.UpdateUI();
    }

    public void StageStart()
    {
        gameObject.SetActive(true);
    }
    public void StageEnd()
    {
        anim.SetTrigger("DeActive");
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
