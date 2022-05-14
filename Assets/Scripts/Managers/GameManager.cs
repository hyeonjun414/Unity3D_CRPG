using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cinemachine;

public class GameManager : Singleton<GameManager>
{
    private Player player;
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
        if (Instance != null)
            Destroy(gameObject);
        else
        {
            DontDestroyOnLoad(gameObject);
            _instance = this;
        }

        SceneManager.sceneLoaded += FindingMainCam;
    }

    public GameObject mouseVfx;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            OptionManager.Instance.OptionBtn();
            IsPause = !IsPause;
        }



    }
    public void FindingMainCam(Scene scene, LoadSceneMode mode)
    {
        print($"현재 씬 : {scene.name})");
        player = FindObjectOfType<Player>();
        mainCam = FindObjectOfType<CinemachineVirtualCamera>();

        if (mainCam != null && player != null)
            mainCam.m_Follow = player.transform;

    }

    public void CreateDamage(int damage, Vector3 pos)
    {
        DamageText dt = Instantiate(damageText, Vector3.zero, Quaternion.identity);
        dt.transform.SetParent(worldCanvas.transform, false);
        dt.Enable(damage, pos);
    }

}
