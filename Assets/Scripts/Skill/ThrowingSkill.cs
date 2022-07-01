using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingSkill : Skill
{
    [Header("Throwing")]
    public Effect throwingEffect;
    public Effect throwingGrounfHit;
    public LineRenderer lr;
    private BattleTile targetBt;
    private bool isThrowing;
    public GameObject grabEffect;
    public override void Casting()
    {
        ThrowingTarget();
    }

    public void ThrowingTarget()
    {
        targetBt = BattleManager.Instance.stage.RandomNoneTile();
        
        StartCoroutine(ThrowRoutine(monster.target.curTile.transform.position, targetBt.transform.position, 2f));
        monster.target.curTile.state = TileState.NONE;
        monster.target.curTile.monster = null;
        monster.target.curTile = targetBt;
        targetBt.monster = monster.target;
        monster.target.curTile.state = TileState.STAY;
    }
    IEnumerator ThrowRoutine(Vector3 p1, Vector3 p3, float time)
    {
        monster.target.ApplyCC(time);
        grabEffect.SetActive(true);
        grabEffect.transform.rotation = Quaternion.identity;
        isThrowing = true;
        float curTime = 0f;
        lr.enabled = true;
        lr.positionCount = 14;
        Vector3 p2 = (p1 + p3) * 0.5f + Vector3.up * 5f;
        while(true)
        {
            if (curTime > time)
                break;
            curTime += Time.deltaTime;
            
            for (int i = 0; i < 14; i++)
            {
                lr.SetPosition(i, GetBezierPos(transform.position + Vector3.up,
                    (transform.position + monster.target.transform.position) * 0.5f + Vector3.up * 5f,
                    monster.target.transform.position + Vector3.up, (float)i / (14 - 1)));
            }
            Vector3 targetPos = monster.target.transform.position;
            targetPos.y = 0;
            monster.transform.LookAt(targetPos);
            grabEffect.transform.position = monster.target.transform.position;
            monster.target.transform.position = GetBezierPos(p1, p2, p3, curTime / time);
            yield return null;
        }
        lr.enabled = false;
        monster.target = null;
        isThrowing = false;
        grabEffect.SetActive(false);
    }

    protected override void OnDisable()
    {
        if(isThrowing)
        {
            monster.target.transform.position = targetBt.transform.position;
            isThrowing=false;
        }
        grabEffect.SetActive(false);
    }

    private Vector3 GetBezierPos(Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        Vector3 q1 = Vector3.Lerp(p1, p2, t);
        Vector3 q2 = Vector3.Lerp(p2, p3, t);

        return Vector3.Lerp(q1, q2, t);
    }

}
