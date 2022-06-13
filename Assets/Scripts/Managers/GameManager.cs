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

    public float prevTimeScale = 1f;

    public bool _isPause = false;
    public bool IsPause{ get; set; }

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
        SceneManager.sceneLoaded += Player.Instance.ResetPlayer;
    }

    public void GiveUp()
    {
        UIManager.Instance.messagePopUpUI.YesOrNoPopUp("여정 포기",
            "정말로 지금까지의 여정을 포기하고 돌아가시겠습니까? \n" +
            "\"예\" 를 누르시면 타이틀 화면으로 돌아가고 진행사항은 저장되지 않습니다.",
            () => {
                Destroy(gameObject);
                Destroy(player.gameObject);
                Time.timeScale = 1f;
                LoadingManager.LoadScene("TitleScene");

            }, null);
    }
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= StageManager.Instance.GenerateScene;
        SceneManager.sceneLoaded -= BattleManager.Instance.FindingEnemyAndStage;
        SceneManager.sceneLoaded -= Player.Instance.ResetPlayer;
    }

}
