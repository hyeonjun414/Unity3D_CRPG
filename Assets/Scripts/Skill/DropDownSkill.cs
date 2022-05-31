using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropDownSkill : Skill
{
    [Header("DropDown")]
    public Effect downHitEffect;
    public override void SetUp(Monster monster, SkillData sd)
    {
        base.SetUp(monster, sd);
    }
    public override void Casting()
    {
        DropDown();
    }

    public void DropDown()
    {
        monster.isVanished = true;
        monster.curTile.state = TileState.NONE;
        monster.curTile.monster = null;
        monster.curTile = null;
        StartCoroutine(DropDownRoutine(2f, 0.35f));
    }
    IEnumerator  DropDownRoutine(float flyTime, float Droptime)
    {
        float curTime = 0;
        while(true)
        {
            if (curTime > flyTime)
                break;
            curTime += Time.deltaTime;
            monster.transform.position += Vector3.up * 100 * Time.deltaTime;
            yield return null;
        }

        curTime = 0;
        Vector3 curPos = monster.transform.position;
        BattleTile targetTile = null;
        if(monster.target.curTile == null)
        {
            targetTile = BattleManager.Instance.stage.RandomNoneTile();
        }
        else
        {
            targetTile = monster.target.curTile;
        }
        List<BattleTile> aroundTiles =  BattleManager.Instance.stage.FindAroundTile(targetTile);
        BattleTile targetBt = aroundTiles.Find((x)=> x.state == TileState.NONE);
        GameObject dropDownEft = ObjectPoolManager.Instance.UseObj(downHitEffect.gameObject);
        dropDownEft.transform.SetParent(null, false);
        dropDownEft.transform.position = targetBt.transform.position;

        monster.curTile = targetBt;
        targetBt.monster = monster;
        targetBt.state = TileState.STAY;
        while (true)
        {
            if (curTime > Droptime)
                break;
            curTime += Time.deltaTime;
            monster.transform.position = Vector3.Lerp(curPos, targetBt.transform.position, curTime/Droptime);
            yield return null;
        }
        aroundTiles = BattleManager.Instance.stage.FindAroundTile(monster.curTile);
        if(monster.owner == MonsterOwner.Player)
        {
            aroundTiles = aroundTiles.FindAll((x)=> x.monster != null && x.monster.owner == MonsterOwner.Enemy);
            foreach(BattleTile tile in aroundTiles)
            {
                tile.monster.Hit(50);
            }
        }
        else
        {
            aroundTiles = aroundTiles.FindAll((x) => x.monster != null && x.monster.owner == MonsterOwner.Player);
            foreach (BattleTile tile in aroundTiles)
            {
                tile.monster.Hit(50);
            }
        }
        monster.isVanished = false;

    }

}
