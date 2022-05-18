using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMoveCommand : MoveCommand
{
    private Monster monster;
    private Animator anim;
    public override void Setup(LivingEntity entity)
    {
        monster = (Monster)entity;
        anim = GetComponentInChildren<Animator>();
    }
    public override void Excute()
    {
        if(monster.target == null || monster.isAttacking)
        {
            return;
        }
        StartCoroutine("MoveRoutine");
    }

    public IEnumerator MoveRoutine()
    {
        monster.isMoving = true;
        BattleStage bs = StageManager.Instance.stage;
        monster.state = MonsterState.Move;

        List<Vector2> moveList = StageManager.Instance.PathFinding(monster, monster.target);
        if (moveList.Count == 0)
        {
            monster.isMoving = false;
            yield break;
        }
        else if (moveList.Count <= monster.range+1)
        {
            monster.isMoving = false;
            yield break;
        }
        else
        {
            monster.curTile.state = TileState.NONE;
            monster.curTile.monster = null;
            monster.curTile = bs.battleMap[(int)moveList[1].x, (int)moveList[1].y];
            monster.curTile.state = TileState.STAY;
            monster.curTile.monster = monster;

            StartCoroutine(moveTile(moveList));
            yield return new WaitForSeconds(monster.moveSpeed);
        }

        monster.isMoving = false;
    }

    IEnumerator moveTile(List<Vector2> tiles)
    {

        BattleTile nextTile = StageManager.Instance.stage.battleMap[(int)tiles[1].x, (int)tiles[1].y];


        Vector3 curPos = transform.position;
        Vector3 distPos = nextTile.transform.position;
        float curTime = 0f;
        float endTime = monster.moveSpeed;
        anim.SetBool("IsMove", true);
        transform.rotation = Quaternion.LookRotation((distPos - curPos).normalized);
        while (true)
        {
            if (curTime >= endTime)
            {
                break;
            }
            curTime += Time.deltaTime;

            transform.position = Vector3.Lerp(curPos, distPos, curTime / endTime);
            yield return null;
        }
        anim.SetBool("IsMove", false);
    }

}
