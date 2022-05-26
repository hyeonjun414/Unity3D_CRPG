using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RewardType
{
    Card,
    Relic,
    Count,
}

public abstract class RewardItem : MonoBehaviour
{
    public RewardType type;
    private Animator anim;
    private Collider coll;

    protected virtual void Start()
    {
        anim = GetComponentInChildren<Animator>();
        coll = GetComponent<Collider>();
    }

    public virtual void RewardGet()
    {
        coll.enabled = false;
        anim.SetTrigger("Get");
    }

}
