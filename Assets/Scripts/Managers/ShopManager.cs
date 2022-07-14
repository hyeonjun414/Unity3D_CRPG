using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : Singleton<ShopManager>
{
    public AudioClip buySfx;

    public int curGold = 1000;
    public int GOLD
    {
        get
        {
            return curGold;
        }
        set
        {
            curGold = value;
            if(curGold < 0)
                curGold = 0;
            UIManager.Instance.goldUI.UpdateUI();
        }
    }

    public List<ShopItem> shopitems = new List<ShopItem>();

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
    }

    public void GenerateShop(int minPrice, int maxPrice)
    {
        shopitems.Clear();
        ItemData tempItem = null;
        for(int i = 0; i < 10; i++)
        {
            switch(Random.Range(0,2))
            {
                case 0:
                    tempItem = RewardManager.Instance.RandomCard();
                    break;
                case 1:
                    tempItem = RewardManager.Instance.RandomRelic();
                    break;
            }
            shopitems.Add(new ShopItem(tempItem, Random.Range(minPrice, maxPrice + 1)));
        }
    }

    public bool BuyResult(ShopItem shopItem)
    {
        if (curGold < shopItem.price)
            return false;
        GOLD -= shopItem.price;

        switch(shopItem.itemData.itemType)
        {
            case ItemType.Card:
                CardManager.Instance.AddCard((CardData)shopItem.itemData);
                break;
            case ItemType.Relic:
                RelicManager.Instance.AddRelic((RelicData)shopItem.itemData);
                break;
        }
        SoundManager.Instance.PlayEffectSound(buySfx);
        return true;
    }
}
