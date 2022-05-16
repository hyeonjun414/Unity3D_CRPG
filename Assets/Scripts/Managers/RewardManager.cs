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
        RewardItem reward = Instantiate(rewardPrefab, pos+rewardPrefab.transform.position, Quaternion.identity);
        reward.rewardData = cardDatas[Random.Range(0, cardDatas.Length)];
    }
}
