using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum RelicType
{
    MpUp,
    MpRegenUp,
    RandomEvolution,
    AttackSpeedUp,
    StartMpUp,
}
[CreateAssetMenu(fileName = "Relic Data", menuName = "Data/Relic Data")]
public class RelicData : ItemData
{
    [Header("Relic Data")]
    public Sprite image;
    public RelicType relicType;
    public string relicName;
    [TextArea]
    public string relicDesc;

    [Header("Relic Prefab")]
    public GameObject relicPrefab;
}
