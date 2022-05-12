using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardUnit : MonoBehaviour
{
    public Image icon;
    public Text nameText;
    public Text descText;

    public CardData cardData;
    public void AddCard(CardData data)
    {
        cardData = data;

        icon.sprite = data.icon;
        nameText.text = data.name;
        descText.text = data.desc;
    }
    public void UpdateUI()
    {
        icon.sprite = cardData.icon;
        nameText.text = cardData.name;
        descText.text = cardData.desc;
    }
}
