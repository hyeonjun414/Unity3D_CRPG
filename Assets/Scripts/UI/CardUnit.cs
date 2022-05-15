using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardUnit : MonoBehaviour
{
    public Image icon;
    public Text nameText;
    public Text descText;
    public Text costText;

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
        transform.Translate(Vector3.right * 800);
        gameObject.SetActive(false);
    }
}
