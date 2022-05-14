using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackCommand : MonoBehaviour
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
        Collider[] detects = Physics.OverlapSphere(transform.position, 10f, LayerMask.GetMask("Enemy"));
        if (detects.Length > 0)
        {
            float minDist = 10f;
            Vector3 targetPos = onwer.rayPos;
            for (int i = 0; i < detects.Length; i++)
            {
                float dist = Vector3.Distance(transform.position, detects[i].transform.position);
                if (dist < minDist)
                {
                    minDist = dist;
                    targetPos = detects[i].transform.position;
                }
            }
            targetPos = (targetPos - transform.position).normalized;
            targetPos.y = 0;
            transform.rotation = Quaternion.LookRotation(targetPos);
        }
        else
        {
            transform.LookAt(onwer.rayPos);
        }

        anim.SetTrigger("Attack");
        Instantiate(GameManager.Instance.mouseVfx, onwer.rayPos + Vector3.up * 0.1f, Quaternion.identity);
        if (onwer.projectile != null)
            Instantiate(onwer.projectile, onwer.projPos.position + transform.forward, transform.rotation);
    }
}
