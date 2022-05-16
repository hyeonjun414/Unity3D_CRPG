using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardUnit : MonoBehaviour
{
    [Header("Card UI")]
    public Image icon;
    public Text nameText;
    public Text descText;
    public Text costText;

    [Header("Card Data")]
    public CardData cardData;
    public void AddCard(CardData data)
    {
        cardData = data;

        icon.sprite = data.icon;
        nameText.text = data.name;
        descText.text = data.desc;
        costText.text = data.cost.ToString();

        gameObject.SetActive(true);
    }
    public void DeleteCard()
    {
        cardData = null;
        transform.localPosition = new Vector3(1280, -80, 0);
        transform.localScale = Vector3.one * 0.1f;
        gameObject.SetActive(false);
    }
}
