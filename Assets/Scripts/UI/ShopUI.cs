using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    public List<ShopItem> shopItems;

    public ShopUnit[] shopUnits;

    public void UpdateUI()
    {
        List<ShopItem> tempList = ShopManager.Instance.shopitems;
        for(int i = 0; i < shopUnits.Length; i++)
        {
            if(i<tempList.Count)
            {
                shopUnits[i].SetItem(tempList[i]);
            }
            else
            {
                shopUnits[i].DeleteItem();
            }
        }
    }
    public void OpenShop()
    {
        gameObject.SetActive(true);
        UpdateUI();
    }
    public void CloseShop()
    {
        gameObject.SetActive(false);
    }
}
