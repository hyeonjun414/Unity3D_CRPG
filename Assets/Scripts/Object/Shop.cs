using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [Header("Price")]
    public int minPrice;
    public int maxPrice;

    private void Start()
    {
        ShopManager.Instance.GenerateShop(minPrice, maxPrice);
    }

    public void OpenShop()
    {
        UIManager.Instance.shopUI.OpenShop();
    }
}
