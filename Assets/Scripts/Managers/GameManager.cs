using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using Cinemachine;

public enum ActionType
{

    OnStageEnd,
    OnDraw,
    OnBattlePrepare,
    OnBattleStart,
    OnBattleEnd,
    Count,

}

public class GameManager : Singleton<GameManager>
{
    public Player player;
    public Transform objectSpace;
    private CinemachineVirtualCamera mainCam;

    

    public bool _isPause = false;
    public bool IsPause
    {
        get { return _isPause; }
        set 
        { 
            _isPause = value; 
            if(_isPause)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }
    }

    public Canvas worldCanvas;
    public DamageText damageText;

    [Header("Action")]
    public UnityAction[] Actions;

    public void UseAction(ActionType type)
    {
        Actions[(int)type]?.Invoke();
    }
    public void AddAction(ActionType type, UnityAction func)
    {
        Actions[(int)type] += func;
    }
    public void DeleteAction(ActionType type, UnityAction func)
    {
        Actions[(int)type] -= func;
    }

    private void Awake()
    {
        // 게임 매니저에서 모든 싱글톤 객체가 담겨있는 오브젝트의 파괴 처리를 담당한다.
        if (Instance != null)
            Destroy(gameObject);
        else
        {
            DontDestroyOnLoad(gameObject);
            _instance = this;
            Actions = new UnityAction[(int)ActionType.Count];

        }

    }

    private void Start()
    {
        ApplySceneLoadedFunc();
    }

    // 데미지 텍스트를 띄우는 함수
    public void CreateText(int damage, Vector3 pos, TextType tt)
    {
        DamageText dt = ObjectPoolManager.Instance.UseObj(damageText.gameObject).GetComponent<DamageText>();
        dt.transform.SetParent(worldCanvas.transform, false); // 월드캔버스의 자식으로 만든다.
        dt.Enable(damage, pos, tt);
    }
    public void CreateText(string text, Vector3 pos, TextType tt)
    {
        DamageText dt = ObjectPoolManager.Instance.UseObj(damageText.gameObject).GetComponent<DamageText>();
        dt.transform.SetParent(worldCanvas.transform, false); // 월드캔버스의 자식으로 만든다.
        dt.Enable(text, pos, tt);
    }

    public int ApplyRandomValue(int value)
    {
        int rand = (int)Random.Range(-value*0.2f, value * 0.2f);
        return value + rand;
    }

    public void ApplySceneLoadedFunc()
    {
        SceneManager.sceneLoaded += StageManager.Instance.GenerateScene;
        SceneManager.sceneLoaded += BattleManager.Instance.FindingEnemyAndStage;
    }

}
