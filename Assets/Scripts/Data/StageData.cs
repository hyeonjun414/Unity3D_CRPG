﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StageType
{
    Enemy,
    Rest,
    Event,
    Shop,
    Boss,
}

[CreateAssetMenu(fileName = "Stage Data", menuName ="Data/Stage Data")]
public class StageData : ScriptableObject
{
    [Header("Stage Data")]
    public StageType type;
    public Sprite icon;

    [Header("Stage Prefabs")]
    public GameObject optionalMap;
}