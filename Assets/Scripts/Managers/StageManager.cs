using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{

    public BattleStage stage;
    public Enemy enemy;
    public Player player;

    public List<Monster> EnemyMonster;
    public List<Monster> AllyMonster;

    private PathFinder pf;

    private bool isStage = false;
    private bool isPrepared = false;
    private void Awake()
    {
        if (_instance == null)
            _instance = this;

        pf = gameObject.AddComponent<PathFinder>();
    }

    private void Start()
    {
        FindingEnemyAndStage();
    }
    public List<Vector2> PathFinding(Monster start, Monster end)
    {
        return pf.ExcutePathFind(start.curTile.tilePos, end.curTile.tilePos, stage);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            CardManager.Instance.ActiveReroll();
        }
    }
    public void FindingEnemyAndStage()
    {
        stage = FindObjectOfType<BattleStage>();
        enemy = FindObjectOfType<Enemy>();
        player = FindObjectOfType<Player>();
        isStage = true;
    }
    public void BattlePrepare()
    {
        if (isPrepared || !isStage) return;

        isPrepared = true;
        
        enemy.SummonMonster();
        CardManager.Instance.ActiveReroll();
    }
    IEnumerator BattleLogic()
    {
        while(true)
        {
            FindTurn(EnemyMonster);
            FindTurn(AllyMonster);
            //yield return new WaitForSeconds(0.1f);
            MoveTurn(EnemyMonster);
            MoveTurn(AllyMonster);
            AttackTurn(EnemyMonster);
            AttackTurn(AllyMonster);
            //yield return new WaitForSeconds(0.1f);
            
            //yield return new WaitForSeconds(0.1f);
            ResultTurn(EnemyMonster);
            ResultTurn(AllyMonster);
            EndTurn();
            //yield return new WaitForSeconds(0.1f);
            yield return null;
        }


        
    }

    private void FindTurn(List<Monster> monList)
    {
        foreach (Monster monster in monList)
        {
            monster.FindTurn();
        }
    }
    private void AttackTurn(List<Monster> monList)
    {
        foreach (Monster monster in monList)
        {
            monster.AttackTurn();
        }
    }
    private void MoveTurn(List<Monster> monList)
    {
        foreach (Monster monster in monList)
        {
            monster.MoveTurn();
        }
    }
    private void ResultTurn(List<Monster> monList)
    {
        for(int i = 0; i< monList.Count;)
        {
            if(monList[i].isDead)
            {
                monList[i].curTile.state = TileState.NONE;
                monList[i].curTile.monster = null;
                Destroy(monList[i].gameObject);
                monList.RemoveAt(i);
            }
            else
            {
                i++;
            }
        }
    }
    private void EndTurn()
    {
        if(AllyMonster.Count == 0 || EnemyMonster.Count == 0)
        {
            BattleStageEnd();
        }
    }


    public void BattleStageStart()
    {
        if (!isPrepared) return;
        //MapSearch();
        if (AllyMonster.Count == 0) return;
        StartCoroutine("BattleLogic");

    }
    public void BattleStageEnd()
    {
        StopCoroutine("BattleLogic");

        foreach(Monster monster in AllyMonster)
        {
            CardManager.Instance.MoveCard(CardSpace.Field, CardSpace.Graveyard, monster.monsterData);
            Destroy(monster.gameObject);
        }
        foreach(Monster monster in EnemyMonster)
        {
            Destroy(monster.gameObject);
        }

        // 적 몬스터가 아군 몬스터보다 적을 때 -> 플레이어가 승리했을때
        if(EnemyMonster.Count < AllyMonster.Count)
        {
            enemy.Hit(player.Attack());
            enemy.waveCount++;
        }
        // 적 몬스터가 아군 몬스터보다 많을 때 -> 적이 승리했을때
        else
        {
            player.Hit(enemy.Attack());
        }

        ResetStage();

        if(enemy.HP == 0)
        {
           StageClear();
           Destroy(enemy.gameObject);
        }
        else if(player.HP == 0)
        {
           // GameOver();
        }
        else
        {
            Invoke("BattlePrepare",1f);
        }
        
    }
    public void StageClear()
    {
        CameraManager.Instance.SwitchCam(0);
        RewardManager.Instance.StageReward(enemy.gameObject.transform.position);
        UIManager.Instance.battleInfoUI.StageEnd();
        stage.StageOut();
        
        isStage = false;

    }
    public void ResetStage()
    {
        AllyMonster.Clear();
        EnemyMonster.Clear();

        foreach(BattleTile bt in stage.battleTiles)
        {
            bt.state = TileState.NONE;
            bt.monster = null;
        }
        isPrepared = false;
    }

}
