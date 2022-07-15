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
    private void Start()
    {
        AddRelic(RewardManager.Instance.RandomRelic());
        AddRelic(RewardManager.Instance.RandomRelic());
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
            case RelicType.RerollCostDown:
                RerollCostDown(-2);
                break;
            case RelicType.DamageUp:
                GameManager.Instance.AddAction(ActionType.OnBattleStart, DamageUp);
                break;
            case RelicType.RandomSummon:
                GameManager.Instance.AddAction(ActionType.OnBattlePrepare, RandomSummon);
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
            case RelicType.RerollCostDown:
                RerollCostDown(-2);
                break;
            case RelicType.DamageUp:
                GameManager.Instance.AddAction(ActionType.OnBattleStart, DamageUp);
                break;
            case RelicType.RandomSummon:
                GameManager.Instance.AddAction(ActionType.OnBattlePrepare, RandomSummon);
                break;
        }
    }

    public void MpUp(int value)
    {
        // 즉시 적용
        GameManager.Instance.player.maxMp += value;
        GameManager.Instance.player.statusUI.UpdateUI();
    }
    public void MpRegenUp(int value)
    {
        // 즉시 적용
        GameManager.Instance.player.mpRegen += value;
        GameManager.Instance.player.statusUI.UpdateUI();

    }
    public void RandomEvolution()
    {
        // 스테이지 종료 시
        print("랜덤 진화 유물 사용");

        List<CardData> deck = CardManager.Instance.deck;
        CardData cd = null;
        MonsterData md = null;
        while (true)
        {
            cd = deck[Random.Range(0, deck.Count)];
            md = cd.cardType == CardType.Monster ? (MonsterData)cd : null;
            if (md != null && md.level != MonsterLevel.LV3)
                break;
        }
        md = md.nextMonster;
        deck.Add(md);
        deck.Remove(cd);
        UIManager.Instance.deckUI.UpdateUI();
    }
    public void AttackSpeedUp()
    {
        // 전투시작 시
        print("아군 몬스터 공속 증가");
        List<Monster> monList = BattleManager.Instance.allyMonster;
        foreach (Monster mon in monList)
        {
            mon.attackSpeed *= 1.2f;
        }

    }
    public void StartMpUp()
    {
        // 전투시작 시
        print("아군 몬스터 시작 MP 50%");

        List<Monster> monList = BattleManager.Instance.allyMonster;
        foreach(Monster mon in monList)
        {
            mon.MP += (int)(mon.maxMp * 0.5f);
            mon.statusBar.UpdateUI();
        }
        
        UIManager.Instance.battleInfoUI.UpdateUI();
    }

    public void RerollCostDown(int value)
    {
        // 즉시 적용
        GameManager.Instance.player.rerollCost += value;

        if(GameManager.Instance.player.rerollCost < 0)
            GameManager.Instance.player.rerollCost = 0;
        GameManager.Instance.player.statusUI.UpdateUI();
    }
    public void DamageUp()
    {
        // 전투시작 시
        List<Monster> monList = BattleManager.Instance.allyMonster;
        foreach (Monster mon in monList)
        {
            mon.damage += 30;
        }
    }
    public void RandomSummon()
    {
        // 전투준비 시
        MonsterData md = RewardManager.Instance.RandomMonsterData();
        BattleTile bt = BattleManager.Instance.stage.RandomAllyTile();
        SummonManager.Instance.SummonMonster(md, bt, MonsterOwner.Player);
    }
}
