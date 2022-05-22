using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckUI : MonoBehaviour
{
    public Button deckButton;
    public Text deckCountText;
    

    public Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void UpdateUI()
    {
        anim.SetTrigger("Active");
        deckCountText.text = CardManager.Instance.deck.Count.ToString();

    }

}
