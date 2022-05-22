using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour, IPoolable
{
    public ParticleSystem ps;

    private void OnEnable()
    {
        ps = GetComponent<ParticleSystem>();
        Invoke("ReturnPool", ps.main.duration);
    }

    public void ReturnPool()
    {
        gameObject.SetActive(false);
        ObjectPoolManager.Instance.ReturnObj(gameObject);
    }
}
