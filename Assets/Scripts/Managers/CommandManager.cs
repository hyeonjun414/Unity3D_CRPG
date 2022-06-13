using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandManager : Singleton<CommandManager>
{
    private int activeUICount = 0;
    private KeyCode curSelectedKey;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
    }

    private void Update()
    {
        if (!Input.anyKey) return;


        if(Input.GetKeyDown(KeyCode.M))
        {
            ActiveMap();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ActiveMenu();
        }
        if (Input.GetKeyDown(KeyCode.F5))
        {
            StageManager.Instance.GenerateGate();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            CardManager.Instance.UseRerollBtn();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            BattleManager.Instance.BattleStageStart();
        }
    }

    public void ActiveMap()
    {
        
        StageUI go = UIManager.Instance.stageUI;
        go.UpdateUI();
        go.gameObject.SetActive(!go.gameObject.activeSelf);
        GameManager.Instance.IsPause = go.gameObject.activeSelf;
    }
    public void ActiveMenu()
    {
        OptionManager.Instance.OptionBtn();
        
    }
}
