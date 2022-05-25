using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Relic : MonoBehaviour
{
    public RelicData data;
    public Image frontImg;
    public Image backImg;

    private void Start()
    {
        frontImg.sprite = data.image;
        backImg.sprite = data.image;
    }

    public void SetRelic()
    {
        switch(data.relicType)
        {
            case RelicType.MpUp:
                break;
            case RelicType.MpRegenUp:
                break;
            case RelicType.RandomEvolution:
                break;
            case RelicType.AttackSpeedUp:
                break;
            case RelicType.StartMpUp:
                break;
        }
    }
    public void UnsetRelic()
    {
        switch (data.relicType)
        {
            case RelicType.MpUp:
                break;
            case RelicType.MpRegenUp:
                break;
            case RelicType.RandomEvolution:
                break;
            case RelicType.AttackSpeedUp:
                break;
            case RelicType.StartMpUp:
                break;
        }
    }
}
