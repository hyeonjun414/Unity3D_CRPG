using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [Header("플레이어")]
    public StatusUI statusUI;

    [Header("카드")]
    public CardHolder cardHolder;
    public DeckUI deckUI;
    public GraveyardUI graveyardUI;
    
    [Header("Info UI")]
    public MonsterInfoUI monsterInfoUI;
    public BattleInfoUI battleInfoUI;
    public RelicInfoUI relicInfoUI;
    public CardInfoUI cardInfoUI;
    public DeckInfoUI deckInfoUI;

    [Header("Stage UI")]
    public StageUI stageUI;

    [Header("Relic UI")]
    public RelicUI relicUI;

    [Header("Shop UI")]
    public ShopUI shopUI;
    public GoldUI goldUI;
    

    [Header("Message Box")]
    public MessagePopUpUI messagePopUpUI;

    [Header("설정")]
    public GameObject optionUI;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
    }
}
