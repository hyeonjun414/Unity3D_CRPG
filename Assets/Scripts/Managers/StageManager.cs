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
        if(Input.GetKeyUp(KeyCode.P))
        {
            BattlePrepare();
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            BattleStageStart();
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

    public void BattleStageStart()
    {
        if (!isPrepared) return;
        MapSearch();
        foreach (Monster enemy in EnemyMonster)
        {
            enemy.BattleStart();
        }
        foreach (Monster ally in AllyMonster)
        {
            ally.BattleStart();
        }


    }
    public void BattleStageEnd()
    {
        foreach (Monster enemy in EnemyMonster)
        {
            enemy.BattleEnd();
        }
        foreach (Monster ally in AllyMonster)
        {
            ally.BattleEnd();
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
    public void EraseMonster(Monster monster)
    {
        if(monster.owner == MonsterOwner.Player)
        {
            AllyMonster.Remove(monster);
            CardManager.Instance.MoveCard(CardSpace.Field, CardSpace.Graveyard, monster.monsterData);
        }
        else
            EnemyMonster.Remove(monster);

        if (AllyMonster.Count == 0 || EnemyMonster.Count == 0)
            BattleStageEnd();
    }

    public void MapSearch()
    {
        AllyMonster.Clear();
        EnemyMonster.Clear();

        for (int i = 0; i < stage.mapSize; i++)
        {
            for (int j = 0; j < stage.mapSize; j++)
            {
                Monster monster = stage.battleMap[i, j].monster;
                if (monster == null) continue;
                switch (monster.owner)
                {
                    case MonsterOwner.Player:
                        AllyMonster.Add(monster);
                        break;
                    case MonsterOwner.Enemy:
                        EnemyMonster.Add(monster);
                        break;
                }
            }
        }
    }
}
