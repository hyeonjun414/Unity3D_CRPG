using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMoveCommand : MoveCommand
{
    private Monster monster;
    private Animator anim;
    private LineRenderer lr;
    public override void Setup(LivingEntity entity)
    {
        monster = (Monster)entity;
        anim = GetComponentInChildren<Animator>();
        lr = gameObject.AddComponent<LineRenderer>();
        lr.startWidth = 0.1f;
        lr.endWidth = 0.1f;
        

    }
    public override void Excute()
    {
        if(monster.target == null || monster.isMoving || monster.isAttacking)
        {
            return;
        }
        StartCoroutine("MoveRoutine");
    }

    public IEnumerator MoveRoutine()
    {
        
        BattleStage bs = StageManager.Instance.stage;
        monster.state = MonsterState.Move;

        List<Vector2> moveList = StageManager.Instance.PathFinding(monster, monster.target);
        if (moveList.Count == 0)
        {
            lr.positionCount = 0;
            yield break;
        }
        else if (moveList.Count <= monster.range+1)
        {
            lr.positionCount = 0;
            monster.transform.LookAt(monster.target.transform.position);
            yield break;
        }
        else
        {
            monster.isMoving = true;
/*            lr.positionCount = moveList.Count;
            for (int i = 0; i < moveList.Count; i++)
            {
                BattleTile bt = bs.battleMap[(int)moveList[i].x, (int)moveList[i].y];
                lr.SetPosition(i, bt.transform.position + Vector3.up * 0.2f);
            }*/
            monster.curTile.state = TileState.NONE;
            monster.nextTile = bs.battleMap[(int)moveList[1].x, (int)moveList[1].y];
            monster.nextTile.state = TileState.STAY;
            monster.curTile.monster = null;
            monster.curTile = monster.nextTile;
            monster.curTile.monster = monster;
            

            yield return StartCoroutine(moveTile(moveList));
            yield return new WaitForSeconds(0.2f);
            monster.isMoving = false;
        }

        
    }

    IEnumerator moveTile(List<Vector2> tiles)
    {

        BattleTile nextTile = StageManager.Instance.stage.battleMap[(int)tiles[1].x, (int)tiles[1].y];


        Vector3 curPos = monster.transform.position;
        Vector3 distPos = nextTile.transform.position;
        float curTime = 0f;
        float endTime = 1f / monster.moveSpeed;
        anim.SetBool("IsMove", true);
        anim.speed = monster.moveSpeed;
        monster.transform.rotation = Quaternion.LookRotation((distPos - curPos).normalized);
        while (true)
        {
            if (curTime >= endTime)
            {
                break;
            }
            curTime += Time.deltaTime;

            monster.transform.position = Vector3.Lerp(curPos, distPos, curTime / endTime);
            yield return null;
        }

        monster.nextTile = null;
        anim.SetBool("IsMove", false);
        anim.speed = 1f;
    }

}
