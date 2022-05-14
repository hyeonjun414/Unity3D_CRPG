using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodgeCommand : MonoBehaviour
{
    private Player onwer;
    private Animator anim;

    public void Setup(Player player)
    {
        onwer = player;
        anim = GetComponentInChildren<Animator>();
    }

    public void Excute()
    {
        Vector3 dir = gameObject.transform.forward;

        

        
    }
}
