using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardManager : Singleton<RewardManager>
{
    [Header("Reward")]
    public CardItem cardPrefab;
    public RelicItem relicPrefab;
    public RewardItem rewardPrefab;


    [Header("Gold")]
    public Coin coinPrefab;
    public int minGold;
    public int maxGold;


    [Header("Database")]
    public RewardDB rewardDB;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
    }

    public void StageReward(Vector3 pos)
    {
        int rand = Random.Range(0, (int)ItemType.Count);
        //Item reward = null;
        RewardItem reward = ObjectPoolManager.Instance.UseObj(rewardPrefab.gameObject).GetComponent<RewardItem>();
        switch ((ItemType)rand)
        {
            case ItemType.Card:
                reward.ActivateCard(RandomCard());
                break;
            case ItemType.Relic:
                reward.ActivateRelic(RandomRelic());
                break;
        }
        GenerateGold(Random.Range(0, 6), Vector3.zero);
        reward.transform.position += pos;
        reward.transform.rotation = Quaternion.identity;
    }

    public void GenerateGold(int count, Vector3 pos)
    {
        Coin coin = null;
        for(int i = 0; i < count; i++)
        {
            coin = ObjectPoolManager.Instance.UseObj(coinPrefab.gameObject).GetComponent<Coin>();
            
            coin.transform.position += pos;
            coin.transform.rotation = Quaternion.identity;
            coin.SetUp(Random.Range(minGold, maxGold));
        }
    }

    private void Update()
    {
    }
    public void StartText()
    {
        for (int i = 0; i < 5; i++)
            CardManager.Instance.MoveCard(CardSpace.Field, CardSpace.Deck, RandomCard());
        
    }
    public CardData RandomCard()
    {
        CardData cd = null;
        switch(Random.Range(0, (int)CardType.End))
        {
            case 0:
                cd = RandomMonsterData();
                break;
            case 1:
                cd = RandomSpell();
                break;

        }
        return cd;
    }
    public MonsterData RandomMonsterData()
    {
        MonsterData[] monList = null;

        int rand = Random.Range(0, 101);
        if(rand <= 80)
        {
            monList = rewardDB.monsters_lv1;
        }
        else if (rand <= 95)
        {
            monList = rewardDB.monsters_lv2;
        }
        else
        {
            monList = rewardDB.monsters_lv3;
        }

        return monList[Random.Range(0, monList.Length)];   
    }
    public SpellData RandomSpell()
    {
        return rewardDB.spellData[Random.Range(0, (int)SpellType.End)];
    }
    public RelicData RandomRelic()
    {
        return rewardDB.relicData[Random.Range(0, rewardDB.relicData.Length)];
    }
}
