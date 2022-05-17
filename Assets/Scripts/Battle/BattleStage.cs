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
    public int mapSize = 7;
    int mapSizeX = 7;
    int mapSizeY = 7;

    
    public BattleTile[,] battleMap;
    public BattleTile[] battleTiles;

    private Animator anim;
    void Awake()
    {
        // 배틀 타일 배열을 맵 사이즈 만큼 할당해준다. -> 7 X 7
        battleMap = new BattleTile[mapSizeX, mapSizeY];
        // 자식에서 배틀 타일들을 받아온다.
        battleTiles = GetComponentsInChildren<BattleTile>();

        // 홀수는 검은색, 짝수는 흰색으로 타일의 색상을 변경
        for(int i = 0; i < battleTiles.Length; i++)
        {
            if(i % 2==0)
            {
                battleTiles[i].mr.material = tileColors[0];
            }
            else
            {
                battleTiles[i].mr.material = tileColors[1];
            }
        }

        // 경로 탐색을 위한 타일 포지션을 잡아준다.
        int count = 0;
        for(int i = 0; i < mapSizeY; i++)
        {
            for(int j = 0; j < mapSizeX; j++)
            {
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
