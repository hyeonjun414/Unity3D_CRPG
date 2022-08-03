using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleStage : MonoBehaviour
{
    /// <summary>
    /// 스테이지 마다 존재하는 배틀스테이지로 
    /// 자식 계층에 존재하는 배틀 타일들에 대한 정보를 가지고 있다.
    /// </summary>

    
    public Material[] tileColors;
    public int mapSize;

    
    public BattleTile[,] battleMap; // astar용
    public List<BattleTile> battleTiles; // 탐색용

    private Animator animator;
    void Awake()
    {
        animator = GetComponent<Animator>();

        // 배틀 타일 배열을 맵 사이즈 만큼 할당해준다. -> 8 X 8
        battleMap = new BattleTile[mapSize, mapSize];
        // 자식에서 배틀 타일들을 받아온다.
        battleTiles = GetComponentsInChildren<BattleTile>().ToList();

        // 경로 탐색을 위한 타일 포지션을 잡아준다.
        int tileCount = 0;
        for(int yIndex = 0; yIndex < mapSize; yIndex++)
        {
            for(int xIndex = 0; xIndex < mapSize; xIndex++)
            {
                battleTiles[tileCount].tileIndex = tileCount;
                battleTiles[tileCount].maxTileCount = battleTiles.Count;
                battleMap[yIndex, xIndex] = battleTiles[tileCount++];
                battleMap[yIndex, xIndex].tilePos = new Vector2(yIndex, xIndex);
            }
        }
    }
    public void StageOut()
    {
        animator.SetTrigger("StageOut");
    }
    public void SelfDestroy()
    {
        Destroy(gameObject);
    }

    public BattleTile RandomNoneTile()
    {
        BattleTile randomNoneTile = null;

        while (true)
        {
            randomNoneTile = battleTiles[Random.Range(0, battleTiles.Count)];
            if (randomNoneTile.state == TileState.NONE)
                break;
        }
        return randomNoneTile;
    }
    public BattleTile RandomAllyTile()
    {
        BattleTile randomAllyTile = null;

        while (true)
        {
            randomAllyTile = battleTiles[Random.Range(battleTiles.Count / 2, battleTiles.Count)];
            if (randomAllyTile.state == TileState.NONE)
                break;
        }
        return randomAllyTile;
    }

    public BattleTile FindTileFromPoint(Vector2 tilePos)
    {
        return battleTiles.Find((targetTile) => targetTile.tilePos == tilePos);
    }

    public List<BattleTile> FindAroundTile(BattleTile centerTile, int findRange)
    {
        float posX = centerTile.tilePos.x;
        float posY = centerTile.tilePos.y;
        return battleTiles.FindAll((targetTile)=>
            (posX - findRange <= targetTile.tilePos.x && targetTile.tilePos.x <= posX + findRange) &&
            (posY - findRange <= targetTile.tilePos.y && targetTile.tilePos.y <= posY + findRange) &&
            targetTile != centerTile);
    }

}
