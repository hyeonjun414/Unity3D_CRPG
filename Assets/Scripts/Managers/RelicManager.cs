using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicManager : Singleton<RelicManager>
{
    [Header("Relic Database")]
    public RelicData[] relicDB;

    [Header("Player Relic")]
    public List<RelicData> playerRelics;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
    }

    public void AddRelic(RelicData relic)
    {
        SetRelicEffect(relic.relicType);
        playerRelics.Add(relic);
        UIManager.Instance.relicUI.UpdateUI();
    }
    public void DeleteRelic(RelicData relic)
    {
        UnsetRelicEffect(relic.relicType);
        playerRelics.Remove(relic);
        UIManager.Instance.relicUI.UpdateUI();
    }
    public void SetRelicEffect(RelicType type)
    {
        switch (type)
        {
            case RelicType.MpUp:
                MpUp(5);
                break;
            case RelicType.MpRegenUp:
                MpRegenUp(3);
                break;
            case RelicType.RandomEvolution:
                GameManager.Instance.AddAction(ActionType.OnStageEnd, RandomEvolution);
                break;
            case RelicType.AttackSpeedUp:
                GameManager.Instance.AddAction(ActionType.OnBattleStart, AttackSpeedUp);
                break;
            case RelicType.StartMpUp:
                GameManager.Instance.AddAction(ActionType.OnBattleStart, StartMpUp);
                break;
        }
    }
    public void UnsetRelicEffect(RelicType type)
    {
        switch (type)
        {
            case RelicType.MpUp:
                MpUp(-5);
                break;
            case RelicType.MpRegenUp:
                MpRegenUp(-3);
                break;
            case RelicType.RandomEvolution:
                GameManager.Instance.AddAction(ActionType.OnStageEnd, RandomEvolution);
                break;
            case RelicType.AttackSpeedUp:
                GameManager.Instance.AddAction(ActionType.OnBattleStart, AttackSpeedUp);
                break;
            case RelicType.StartMpUp:
                GameManager.Instance.AddAction(ActionType.OnBattleStart, StartMpUp);
                break;
        }
    }

    public void MpUp(int value)
    {
        GameManager.Instance.player.maxMp += value;
        GameManager.Instance.player.statusUI.UpdateUI();
    }
    public void MpRegenUp(int value)
    {
        GameManager.Instance.player.mpRegen += value;
        GameManager.Instance.player.statusUI.UpdateUI();

    }
    public void RandomEvolution()
    {
        print("랜덤 진화 유물 사용");

        List<CardData> deck = CardManager.Instance.deck;
        CardData cd = null;
        MonsterData md = null;
        while (cd == null)
        {
            cd = deck[Random.Range(0, deck.Count)];
            md = cd.cardType == CardType.Monster ? (MonsterData)cd : null;
            if (md != null && md.level != MonsterLevel.LV3)
                break;
        }
        md = ((MonsterData)cd).nextMonster;
        deck.Add(md);
        deck.Remove(cd);
        UIManager.Instance.deckUI.UpdateUI();
    }
    public void AttackSpeedUp()
    {
        print("아군 몬스터 공속 증가");
        List<Monster> monList = BattleManager.Instance.allyMonster;
        foreach (Monster mon in monList)
        {
            mon.attackSpeed *= 2f;
        }

    }
    public void StartMpUp()
    {
        print("아군 몬스터 시작 MP 40%");

        List<Monster> monList = BattleManager.Instance.allyMonster;
        foreach(Monster mon in monList)
        {
            mon.MP += (int)(mon.maxMp * 0.4f);
            mon.statusBar.UpdateUI();
        }
        
        UIManager.Instance.battleInfoUI.UpdateUI();
    }
}
