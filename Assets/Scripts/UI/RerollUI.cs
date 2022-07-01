using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RerollUI : MonoBehaviour
{
    public Text rerollCostText;

    private Player player;

    public void SetUp(Player player)
    {
        this.player = player;
        UpdateUI();
    }
    public void UpdateUI()
    {
        rerollCostText.text = player.rerollCost.ToString();
    }
}
