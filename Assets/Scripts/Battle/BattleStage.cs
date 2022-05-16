using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStage : MonoBehaviour
{
    public Material[] tileColors;
    public int mapSize = 7;
    int mapSizeX = 7;
    int mapSizeY = 7;

    
    public BattleTile[,] battleMap;
    public BattleTile[] battleTiles;

    private Animator anim;
    void Awake()
    {
        battleMap = new BattleTile[mapSizeX, mapSizeY];
        battleTiles = GetComponentsInChildren<BattleTile>();
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
