using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStage : MonoBehaviour
{
    /// <summary>
    /// 스테이지 마다 존재하는 배틀스테이지로 
    /// 자식 계층에 존재하는 배틀 타일들에 대한 정보를 가지고 있다.
    /// </summary>

    
    public Material[] tileColors;
    public int mapSize = 8;

    
    public BattleTile[,] battleMap;
    public BattleTile[] battleTiles;

    private Animator anim;
    void Awake()
    {
        // 배틀 타일 배열을 맵 사이즈 만큼 할당해준다. -> 8 X 8
        battleMap = new BattleTile[mapSize, mapSize];
        // 자식에서 배틀 타일들을 받아온다.
        battleTiles = GetComponentsInChildren<BattleTile>();

        // 경로 탐색을 위한 타일 포지션을 잡아준다.
        int count = 0;
        for(int i = 0; i < mapSize; i++)
        {
            for(int j = 0; j < mapSize; j++)
            {
                battleTiles[count].tileIndex = count;
                battleTiles[count].maxTileIndex = battleTiles.Length;
                battleMap[i,j] = battleTiles[count++];
                battleMap[i, j].tilePos = new Vector2(i, j);
            }
        }
    }
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void StageOut()
    {
        anim.SetTrigger("StageOut");
    }
    public void SelfDestroy()
    {
        Destroy(gameObject);
    }


}
