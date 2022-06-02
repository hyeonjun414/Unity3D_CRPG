using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProjectileMoveType
{
    Direct,
    Indirect,
}
public class Projectile : MonoBehaviour, IPoolable
{
    [Header("Projectile")]
    public GameObject hit;
    public ProjectileMoveType moveType;
    public int damage;

    [Header("Monster")]
    public Monster owner;
    public Monster target;
    public float duration;

    [Header("Indirect Option")]
    public float projHeight;
    public void SetUp(Monster owner, Monster target, int damage,float duration, ProjectileMoveType pmt = ProjectileMoveType.Direct)
    {
        this.owner = owner;
        this.damage = damage;
        this.target = target;
        this.duration = duration;
        Invoke("ReturnPool", duration + 0.5f);
        //Destroy(gameObject, duration + 0.5f);

        switch (pmt)
        {
            case ProjectileMoveType.Direct:
                moveType = ProjectileMoveType.Direct;
                break;
            case ProjectileMoveType.Indirect:
                moveType = ProjectileMoveType.Indirect;
                projHeight = 2;
                break;
        }

        StartCoroutine(ShotRoutine());
    }
    public void SetUp(Monster owner, Monster target, float duration, ProjectileMoveType pmt = ProjectileMoveType.Direct)
    {
        this.owner = owner;
        damage = owner.damage;
        this.target = target;
        this.duration = duration;
        Invoke("ReturnPool", duration + 0.5f);
        //Destroy(gameObject, duration + 0.5f);

        switch(pmt)
        {
            case ProjectileMoveType.Direct:
                moveType = ProjectileMoveType.Direct;
                break;
            case ProjectileMoveType.Indirect:
                moveType = ProjectileMoveType.Indirect;
                projHeight = 2;
                break;
        }

        StartCoroutine(ShotRoutine());
    }
    public IEnumerator ShotRoutine()
    {
        float curTime = 0f;
        while (true)
        {
            if(!owner.gameObject.activeSelf || target == null || !target.gameObject.activeSelf || target.isDead)
            {
                ReturnPool();
                yield break;
            }
                

            if (curTime >= duration)
            {
                break;
            }

            curTime += Time.deltaTime;
            switch(moveType)
            {
                case ProjectileMoveType.Direct:
                    DirectMove(curTime/ duration);
                    break;
                case ProjectileMoveType.Indirect:
                    IndirectMove(curTime/ duration);
                    break;
            }
            yield return null;
        }
        CreateHit();
        target.Hit(owner.Attack());
        UIManager.Instance.battleInfoUI.UpdateUI();
        ReturnPool();

    }

    public void DirectMove(float time)
    {
        transform.rotation = Quaternion.LookRotation((target.transform.position + Vector3.up - transform.position).normalized);
        transform.position = Vector3.Lerp(owner.transform.position + Vector3.up, target.transform.position + Vector3.up, time);
    }
    public void IndirectMove(float time)
    {
        Vector3 p2 = (owner.transform.position + target.transform.position) * 0.5f + Vector3.up * projHeight;
        Vector3 movePos = GetBezierPos(owner.transform.position + Vector3.up, p2, target.transform.position + Vector3.up, time);
        transform.LookAt(movePos);
        transform.position = movePos;
    }

    private Vector3 GetBezierPos(Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        Vector3 q1 = Vector3.Lerp(p1, p2, t);
        Vector3 q2 = Vector3.Lerp(p2, p3, t);

        return Vector3.Lerp(q1, q2, t);
    }

    public void CreateHit()
    {
        if (hit != null)
        {
            GameObject hitInstance = ObjectPoolManager.Instance.UseObj(hit);
            hitInstance.transform.position = transform.position;
            hitInstance.transform.rotation = Quaternion.LookRotation(-(transform.position - target.transform.position + Vector3.up).normalized);

        }
    }

    public void ReturnPool()
    {
        CancelInvoke("ReturnPool");
        StopAllCoroutines();
        gameObject.SetActive(false);
        ObjectPoolManager.Instance.ReturnObj(gameObject);
    }
}
