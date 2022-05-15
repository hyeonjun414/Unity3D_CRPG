using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cinemachine;

public class GameManager : Singleton<GameManager>
{
    public Player player;
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

    private void Awake()
    {
        // 게임 매니저에서 모든 싱글톤 객체가 담겨있는 오브젝트의 파괴 처리를 담당한다.
        if (Instance != null)
            Destroy(gameObject);
        else
        {
            DontDestroyOnLoad(gameObject);
            _instance = this;
        }

    }

    public GameObject mouseVfx;
    private void Update()
    {
        // 옵션 창을 열고 게임을 일시정지 시킨다.
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            OptionManager.Instance.OptionBtn();
            IsPause = !IsPause;
        }
    }

    // 데미지 텍스트를 띄우는 함수
    public void CreateDamage(int damage, Vector3 pos)
    {
        DamageText dt = Instantiate(damageText, Vector3.zero, Quaternion.identity);
        dt.transform.SetParent(worldCanvas.transform, false); // 월드캔버스의 자식으로 만든다.
        dt.Enable(damage, pos);
    }

}
