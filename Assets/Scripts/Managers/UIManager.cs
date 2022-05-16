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
    

    [Header("설정")]
    public GameObject optionUI;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
    }
}
