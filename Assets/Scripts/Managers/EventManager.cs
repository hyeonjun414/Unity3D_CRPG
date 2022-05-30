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
        UIManager.Instance.messagePopUpUI.PopUp("가시에 찔림",
            () => Time.timeScale = 1f);
        GameManager.Instance.player.Hit(5);

    }
    public void DropGold()
    {
        Time.timeScale = 0f;
        UIManager.Instance.messagePopUpUI.PopUp("돈이 생김",
            () => Time.timeScale = 1f);
        RewardManager.Instance.GenerateGold(10, Vector3.zero);
    }
    public void HealPlayer()
    {
        Time.timeScale = 0f;
        UIManager.Instance.messagePopUpUI.PopUp("운좋게 회복함",
            () => Time.timeScale = 1f);
        GameManager.Instance.player.Heal(5);
    }
    public void RandomReward()
    {
        Time.timeScale = 0f;
        UIManager.Instance.messagePopUpUI.PopUp("랜덤 아이템 드랍됨",
            () => Time.timeScale = 1f);
        RewardManager.Instance.StageReward(Vector3.zero);
    }
}
