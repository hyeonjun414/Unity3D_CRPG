using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Card,
    Relic,
    Count,
}

public abstract class Item : MonoBehaviour
{
    public ItemType type;
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
        UIManager.Instance.keyUI.ItemExit();
    }
}
