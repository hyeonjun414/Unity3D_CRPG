using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [Header("플레이어")]
    public Text HpText;
    public Text MpText;
    public Slider HpBar;
    public Slider MpBar;

    [Header("카드")]
    public CardHolder cardHolder;
    public DeckUI deckUI;
    

    [Header("설정")]
    public GameObject optionUI;

    private void Awake()
    {
        _instance = this;
    }
}
