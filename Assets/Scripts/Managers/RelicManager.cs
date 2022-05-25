using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicManager : Singleton<RelicManager>
{
    [Header("Relic Database")]
    public RelicData[] relicDB;

    [Header("Player Relic")]
    public List<Relic> playerRelics;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
    }

    public void SetRelicEffect()
    {
        foreach(Relic relic in playerRelics)
        {
            relic.SetRelic();
        }
    }
    public void UnsetRelicEffect()
    {
        foreach (Relic relic in playerRelics)
        {
            relic.UnsetRelic();
        }
    }
}
