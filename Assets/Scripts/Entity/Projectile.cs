using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Projectile")]
    public GameObject hit;

    [Header("Monster")]
    public Monster owner;
    public Monster target;
    public float duration;

    public void SetUp(Monster owner, Monster target, float duration )
    {
        this.owner = owner;
        this.target = target;
        this.duration = duration;
        Destroy(gameObject, duration * 1.1f);
        StartCoroutine(ShotRoutine());
    }
    public IEnumerator ShotRoutine()
    {
        float curTime = 0f;
        float endTime = 0.5f;
        while (true)
        {
            if(owner == null || target == null)
            {
                Destroy(gameObject);
                yield break;
            }
                

            if (curTime >= endTime)
            {
                break;
            }

            curTime += Time.deltaTime;
            transform.rotation = Quaternion.LookRotation((target.transform.position + Vector3.up - transform.position).normalized);
            transform.position = Vector3.Lerp(owner.transform.position+Vector3.up, target.transform.position+Vector3.up, curTime / endTime);
            yield return null;
        }
        CreateHit();
        target.Hit(owner.Attack());
        UIManager.Instance.battleInfoUI.UpdateUI();
        Destroy(gameObject);

    }
    public void CreateHit()
    {
        if (hit != null)
        {
            var hitInstance = Instantiate(hit, transform.position, Quaternion.identity);
            hitInstance.transform.rotation = Quaternion.LookRotation(-(transform.position - target.transform.position + Vector3.up).normalized);

            var hitPs = hitInstance.GetComponent<ParticleSystem>();
            if (hitPs != null)
            {
                Destroy(hitInstance, hitPs.main.duration);
            }
            else
            {
                var hitPsParts = hitInstance.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(hitInstance, hitPsParts.main.duration);
            }
        }
    }
}
