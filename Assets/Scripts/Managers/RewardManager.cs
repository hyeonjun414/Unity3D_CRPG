using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardManager : Singleton<RewardManager>
{
    [Header("Reward")]
    public RewardItem rewardPrefab;
    public CardData[] cardDatas;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
    }

    public void StageReward(Vector3 pos)
    {
        RewardItem reward = ObjectPoolManager.Instance.UseObj(rewardPrefab.gameObject).GetComponent<RewardItem>();
        reward.transform.position = pos + rewardPrefab.transform.position;
        reward.transform.rotation = Quaternion.identity;
        reward.rewardData = cardDatas[Random.Range(0, cardDatas.Length)];
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
