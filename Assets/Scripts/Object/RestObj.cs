using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestObj : MonoBehaviour
{
    public GameObject restEffect;
    public AudioSource sfx;
    public bool isUsed = false;

    public void Resting(Player player)
    {
        if (isUsed) return;

        if (player.HP == player.maxHp)
            GameManager.Instance.CreateText("체력이 가득참!", player.transform.position, TextType.Heal);
        else
        {
            player.Heal(10);
        }
        sfx.Stop();
        restEffect.SetActive(false);
        isUsed = true;
    }
}
