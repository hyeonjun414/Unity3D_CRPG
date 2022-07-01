using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem
{
    public ItemData itemData;
    public int price;
    public bool isSold;
    public ShopItem(ItemData data, int value)
    {
        itemData = data;
        price = value;
        isSold = false;
    }
}
