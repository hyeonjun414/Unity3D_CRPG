using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUnit : MonoBehaviour
{
    [Header("Item Base")]
    public ShopItem shopItem;
    public RewardItem unitItem;

    [Header("Buy Button")]
    public Button buyButton;
    public Text buyPriceText;

    [Header("Sold Image")]
    public GameObject soldImage;

    public void SetItem(ShopItem item)
    {
        gameObject.SetActive(true);
        shopItem = item;
        unitItem.SetUp(item.itemData);

        if(item.isSold)
        {
            soldImage.SetActive(true);
            buyButton.interactable = false;
        }
        else
        {
            soldImage.SetActive(false);
            buyButton.interactable = true;
        }

        buyPriceText.text = item.price.ToString();
    }
    public void DeleteItem()
    {
        gameObject.SetActive(false);
    }

    public void Buy()
    {
        if(ShopManager.Instance.BuyResult(shopItem))
        {
            shopItem.isSold = true;
            UIManager.Instance.shopUI.UpdateUI();
        }
        else
        {
            UIManager.Instance.messagePopUpUI.PopUp("가지고 있는 돈이 부족합니다.");
        }
    }
}
