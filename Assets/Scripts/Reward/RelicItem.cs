using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RelicItem : Item
{
    public RelicData relicData;
    public Image frontImg;
    public Image backImg;

/*    protected override void Start()
    {
        base.Start();
        type = ItemType.Relic;
        frontImg.sprite = relicData.image;
        backImg.sprite = relicData.image;
    }*/

    public void SetUp(RelicData data)
    {
        relicData = data;
        type = ItemType.Relic;
        frontImg.sprite = relicData.image;
        backImg.sprite = relicData.image;
    }

    public override void RewardGet()
    {
        base.RewardGet();
        RelicManager.Instance.AddRelic(relicData);
        Destroy(gameObject, 1f);
    }

    private void OnMouseEnter()
    {
        print("유물 드랍 아이템 손댐");
        UIManager.Instance.relicInfoUI.InfoEnter(relicData);
    }
    private void OnMouseExit()
    {
        UIManager.Instance.relicInfoUI.InfoExit();
    }
}
