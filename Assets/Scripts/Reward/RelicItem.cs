using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RelicItem : RewardItem
{
    public RelicData relicdata;
    public Image frontImg;
    public Image backImg;

    protected override void Start()
    {
        base.Start();
        type = RewardType.Relic;
        frontImg.sprite = relicdata.image;
        backImg.sprite = relicdata.image;
    }

    public override void RewardGet()
    {
        base.RewardGet();
        RelicManager.Instance.AddRelic(relicdata);
        Destroy(gameObject, 1f);
    }
}
