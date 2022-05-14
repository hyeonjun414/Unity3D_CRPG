using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveCommand : MonoBehaviour
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
        Vector3 dir = Vector3.zero;

        if (!onwer.isAttack)
        {
            if (Input.GetKey(KeyCode.W))
            {
                dir += Vector3.forward;
            }
            if (Input.GetKey(KeyCode.S))
            {
                dir += Vector3.back;
            }
            if (Input.GetKey(KeyCode.A))
            {
                dir += Vector3.left;
            }
            if (Input.GetKey(KeyCode.D))
            {
                dir += Vector3.right;
            }

            dir = Quaternion.Euler(0, 30, 0) * dir.normalized;

            transform.Translate(dir * onwer.moveSpeed * Time.deltaTime, Space.World);

        }

        if (dir.sqrMagnitude > 0.2f)
        {
            anim.SetBool("Run", true);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), onwer.roteSpeed * Time.deltaTime);
        }
        else
            anim.SetBool("Run", false);
    }
}
