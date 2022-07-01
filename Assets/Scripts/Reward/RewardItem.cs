using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardItem : MonoBehaviour
{
    [Header("Item Data")]
    public ItemData itemData;

    [Header("Relic Item")]
    public RelicItem relicItem;

    [Header("Card Item")]
    public CardItem cardItem;
    public void SetUp(ItemData data)
    {
        itemData = data;
        switch(itemData.itemType)
        {
            case ItemType.Card:
                ActivateCard((CardData)itemData);
                break;
            case ItemType.Relic:
                ActivateRelic((RelicData)itemData);
                break;
        }
    }

    public void ActivateCard(CardData data)
    {
        cardItem.gameObject.SetActive(true);
        relicItem.gameObject.SetActive(false);

        cardItem.SetUp(data);
    }
    public void ActivateRelic(RelicData data)
    {
        cardItem.gameObject.SetActive(false);
        relicItem.gameObject.SetActive(true);

        relicItem.SetUp(data);
    }
}
