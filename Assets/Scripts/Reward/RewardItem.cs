using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardItem : MonoBehaviour
{

    public CardData rewardData;

    public ParticleSystem getEffect;

    private Animator anim;
    private Collider coll;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        coll = GetComponent<Collider>();
    }

    public void RewardGet()
    {
        coll.enabled = false;
        anim.SetTrigger("Get");
        CardManager.Instance.AddCard(rewardData);
        Destroy(gameObject, 1f);
    }

}
