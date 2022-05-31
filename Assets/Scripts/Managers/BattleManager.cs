using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleManager : Singleton<BattleManager>
{

    public BattleStage stage;
    public Enemy enemy;
    public Player player;

    public List<Monster> enemyMonster;
    public List<Monster> allyMonster;

    private PathFinder pf;

    public bool isStage = false;
    public bool isPrepared = false;
    private void Awake()
    {
        if (_instance == null)
            _instance = this;

        pf = gameObject.AddComponent<PathFinder>();
    }

    public List<Vector2> PathFinding(Monster start, Monster end)
    {
        return pf.ExcutePathFind(start.curTile.tilePos, end.curTile.tilePos, stage);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            CardManager.Instance.TurnEndReroll();
        }
    }
    public void FindingEnemyAndStage(Scene scene, LoadSceneMode mode)
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
        CardManager.Instance.TurnEndReroll();
        player.RegenMp();
    }
    IEnumerator BattleLogic()
    {
        while(true)
        {

            FindTurn(enemyMonster);
            FindTurn(allyMonster);

            MoveTurn(enemyMonster);
            MoveTurn(allyMonster);

            AttackTurn(enemyMonster);
            AttackTurn(allyMonster);
            
            ResultTurn(enemyMonster);
            ResultTurn(allyMonster);

            EndTurn();

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
                monList[i].ReturnPool();
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
        if(allyMonster.Count == 0 || enemyMonster.Count == 0)
        {
            BattleStageEnd();
        }
    }


    public void BattleStageStart()
    {
        if (!isPrepared) return;
        //MapSearch();
        if (allyMonster.Count == 0) return;
        GameManager.Instance.UseAction(ActionType.OnBattleStart);
        StartCoroutine("BattleLogic");

    }
    public void BattleStageEnd()
    {
        StopCoroutine("BattleLogic");

        foreach(Monster monster in allyMonster)
        {
            //CardManager.Instance.MoveCard(CardSpace.Field, CardSpace.Graveyard, monster.monsterData);
            monster.ReturnPool();
            //Destroy(monster.gameObject);
        }
        foreach(Monster monster in enemyMonster)
        {
            monster.ReturnPool();
            //Destroy(monster.gameObject);
        }

        // 적 몬스터가 아군 몬스터보다 적을 때 -> 플레이어가 승리했을때
        if(enemyMonster.Count < allyMonster.Count)
        {
            enemy.Hit(player);
            enemy.waveCount++;
        }
        // 적 몬스터가 아군 몬스터보다 많을 때 -> 적이 승리했을때
        else
        {
            player.Hit(enemy);
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
        CardManager.Instance.MoveAlltoDeck();
        CameraManager.Instance.SwitchCam(0);
        RewardManager.Instance.StageReward(Vector3.zero);
        UIManager.Instance.battleInfoUI.StageEnd();
        stage.StageOut();
        
        isStage = false;
        StageManager.Instance.GenerateGate();
        GameManager.Instance.UseAction(ActionType.OnStageEnd);

    }
    public void ResetStage()
    {
        allyMonster.Clear();
        enemyMonster.Clear();

        foreach(BattleTile bt in stage.battleTiles)
        {
            bt.state = TileState.NONE;
            bt.monster = null;
        }
        isPrepared = false;
    }

}
