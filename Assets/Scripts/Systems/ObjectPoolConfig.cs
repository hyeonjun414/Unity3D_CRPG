using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolConfig : MonoBehaviour
{
    [HideInInspector]
    public string key;


    private void OnEnable()
    {
        //print("몬스터 생성");
        ObjectPooling.Instance.monstorCount++;
    }
    private void OnDisable()
    {
        //print("몬스터 제거");
        ObjectPooling.Instance.monstorCount--;
    }

    public void Dead()
    {

        ObjectPooling.Instance.ObjectReturn(key, gameObject);

    }

}
