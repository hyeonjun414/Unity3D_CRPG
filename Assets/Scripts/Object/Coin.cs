using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour, IPoolable
{
    public GameObject target;
    public Effect getEffect;

    public int value;
    

    public Collider coll;
    public Vector3 initPos;
    //public 
    private void Awake()
    {
        coll = GetComponent<Collider>();
    }
    private void Update()
    {
        if (target == null) return;

        transform.position = Vector3.Lerp(transform.position, target.transform.position, Time.deltaTime * 5f);
        if(Vector3.Distance(transform.position, target.transform.position) < 0.5f)
        {
            GetCoin();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(target == null && other.CompareTag("Player"))
        {
            target = other.gameObject;
        }
    }

    public void SetUp(int value)
    {
        this.value = value;
        coll.enabled = false;
        target = null;
        initPos = new Vector3(Random.Range(-3, 4), 0, Random.Range(-3, 4));

        StartCoroutine("MoveInitPosition");
    }

    IEnumerator MoveInitPosition()
    {
        float curTime = 0f;
        Vector3 dest = transform.position + initPos;
        while (true)
        {
            if (curTime > 1f)
                break;
            curTime += Time.deltaTime;

            transform.position = Vector3.Lerp(transform.position, dest, curTime / 1f);
            yield return null;
        }
        coll.enabled = true;
    }

    public void GetCoin()
    {
        ShopManager.Instance.GOLD += value;
        if(getEffect != null)
        {
            GameObject go = ObjectPoolManager.Instance.UseObj(getEffect.gameObject);
            go.transform.SetParent(null, false);
            go.transform.position = transform.position;
        }

        ReturnPool();
    }

    public void ReturnPool()
    {
        gameObject.SetActive(false);
        ObjectPoolManager.Instance.ReturnObj(gameObject);
    }
}
