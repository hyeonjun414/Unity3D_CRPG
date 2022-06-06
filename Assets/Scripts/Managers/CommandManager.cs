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
    }

    public void ActiveMap()
    {
        StageUI go = UIManager.Instance.stageUI;
        go.UpdateUI();
        go.gameObject.SetActive(!go.gameObject.activeSelf);
        GameManager.Instance.IsPause = !GameManager.Instance.IsPause;
    }
    public void ActiveMenu()
    {
        OptionManager.Instance.OptionBtn();
        GameManager.Instance.IsPause = !GameManager.Instance.IsPause;
    }
}
