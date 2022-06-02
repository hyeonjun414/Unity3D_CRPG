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
    public CardData[] cardDatas;
    public RelicData[] relicDatas;

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
                reward.ActivateCard(cardDatas[Random.Range(0, cardDatas.Length)]);
                break;
            case ItemType.Relic:
                reward.ActivateRelic(relicDatas[Random.Range(0, relicDatas.Length)]);
                break;
        }
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
        if(Input.GetKeyDown(KeyCode.G))
        {
            StartText();
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            GenerateGold(10, Vector3.zero);
        }
    }
    public void StartText()
    {
        int rand;
        for (int i = 0; i < 5; i++)
        {
            rand = Random.Range(0, cardDatas.Length);
            CardManager.Instance.MoveCard(CardSpace.Field, CardSpace.Deck, cardDatas[rand]);
        }
        
    }
    public MonsterData RandomMonsterData()
    {
        CardData md = null;
        int rand = 0;
        while(true)
        {
            rand = Random.Range(0, cardDatas.Length);
            if(cardDatas[rand] is MonsterData)
            {
                md = cardDatas[rand];
                break;
            }
        }
        return (MonsterData)md;
    }
    public RelicData RandomRelic()
    {
        return relicDatas[Random.Range(0, relicDatas.Length)];
    }
}
