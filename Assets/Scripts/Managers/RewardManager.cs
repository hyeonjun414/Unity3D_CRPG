using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardManager : Singleton<RewardManager>
{
    [Header("Reward")]
    public CardItem cardPrefab;
    public RelicItem relicPrefab;

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
        int rand = Random.Range(0, (int)RewardType.Count);
        RewardItem reward = null;
        switch ((RewardType)rand)
        {
            case RewardType.Card:
                reward = ObjectPoolManager.Instance.UseObj(cardPrefab.gameObject).GetComponent<RewardItem>();
                ((CardItem)reward).cardData = cardDatas[Random.Range(0, cardDatas.Length)];
                reward.transform.position = cardPrefab.transform.position;
                break;
            case RewardType.Relic:
                reward = ObjectPoolManager.Instance.UseObj(relicPrefab.gameObject).GetComponent<RewardItem>();
                ((RelicItem)reward).relicdata = relicDatas[Random.Range(0, relicDatas.Length)];
                reward.transform.position = relicPrefab.transform.position;
                break;
        }
        reward.transform.position += pos;
        reward.transform.rotation = Quaternion.identity;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            StartText();
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
}
