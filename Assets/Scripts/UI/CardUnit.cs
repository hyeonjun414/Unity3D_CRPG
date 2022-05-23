using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardUnit : MonoBehaviour
{
    [Header("Card UI")]
    public Image icon;
    public Text nameText;
    public Text costText;

    public virtual void AddCard(CardData data)
    {
        gameObject.SetActive(true);

        icon.sprite = data.icon;
        nameText.text = data.name;
        costText.text = data.cost.ToString();
    }
    public virtual void DeleteCard()
    {
        gameObject.SetActive(false);
    }
}
