using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckUI : MonoBehaviour
{
    public Text deckCountText;

    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void AddDeckCard(List<CardData> deckList, CardData data)
    {
        anim.SetTrigger("Active");
        deckList.Add(data);
        deckCountText.text = deckList.Count.ToString();
    }
}
