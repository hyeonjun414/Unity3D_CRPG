using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card Data", menuName = "Card/Card Data")]
public class CardData : ScriptableObject
{
    [Header("Card")]
    public Sprite icon;
    public new string name;
    public string desc;

    public GameObject vfx;
    
}
