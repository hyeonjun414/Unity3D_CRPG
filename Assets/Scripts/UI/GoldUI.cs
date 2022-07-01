using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldUI : MonoBehaviour
{
    public Text goldText;

    public void UpdateUI()
    {
        goldText.text = ShopManager.Instance.GOLD.ToString();
    }
}
