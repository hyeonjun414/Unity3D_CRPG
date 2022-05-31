using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum EventType
{
    HitPlayer,
    DropGold,
    HealPlayer,
    RandomReward,


    End
}

public class EventManager : Singleton<EventManager>
{
    public UnityAction OnEvent;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
    }

    public void ActivateEvent()
    {
        OnEvent?.Invoke();
        OnEvent = null;
    }

    public void SetRandomEvent()
    {
        int rand = Random.Range(0, (int)EventType.End);
        switch((EventType)rand)
        {
            case EventType.HitPlayer:
                OnEvent = HitPlayer;
                break;
            case EventType.DropGold:
                OnEvent = DropGold;
                break;
            case EventType.HealPlayer:
                OnEvent = HealPlayer;
                break;
            case EventType.RandomReward:
                OnEvent = RandomReward;
                break;
        }
    }

    public void HitPlayer()
    {
        Time.timeScale = 0f;
        UIManager.Instance.messagePopUpUI.PopUp("파상풍 주의",
            "앞에 낡은 작은 상자가 보인다. \n" +
            "당신은 앞에 보이는 오래된 상자를 열려고 하다가 녹슨 못에 찔리고 말았다. \n" +
            "부상을 무릅쓰고 상자를 살펴봤지만 아무것도 없었다.",
            () => Time.timeScale = 1f);
        GameManager.Instance.player.Hit(5);

    }
    public void DropGold()
    {
        Time.timeScale = 0f;
        UIManager.Instance.messagePopUpUI.PopUp("황금 상자",
            "눈 앞에 반짝이는 상자가 보인다 \n" +
            "불안하지만 용기를 가지고 상자를 열어봤다. \n" +
            "용기를 낸 결과 당신은 많은 돈을 얻게 되었다.",
            () => Time.timeScale = 1f);
        RewardManager.Instance.GenerateGold(10, Vector3.zero);
    }
    public void HealPlayer()
    {
        Time.timeScale = 0f;
        UIManager.Instance.messagePopUpUI.PopUp("약초 꾸러미",
            "지친 몸을 끌고 어떤 방에 도착했다. \n" +
            "방 한가운데 방과 어울리지 않은 식물이 자라고 있다. \n" +
            "배고프고 지쳐있었던 당신은 마구잡이도 식물을 뜯어 먹었다. \n" +
            "몸에 다시 힘이 차오르는 것 같다...",
            () => Time.timeScale = 1f);
        GameManager.Instance.player.Heal(5);
    }
    public void RandomReward()
    {
        Time.timeScale = 0f;
        UIManager.Instance.messagePopUpUI.PopUp("뜻밖의 보상",
            "눈 앞에 반짝이는 상자가 보인다 \n" +
            "불안하지만 용기를 가지고 상자를 열어봤다. \n" +
            "상자에서 무언가 튀어나와 반짝거린다...",
            () => Time.timeScale = 1f);
        RewardManager.Instance.StageReward(Vector3.zero);
    }
}
