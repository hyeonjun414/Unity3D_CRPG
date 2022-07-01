using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraveyardUI : MonoBehaviour
{
    public Button gyButton;
    public Text gyCountText;


    public Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void UpdateUI()
    {
        anim.SetTrigger("Active");
        gyCountText.text = CardManager.Instance.graveyard.Count.ToString();
        
    }
}
