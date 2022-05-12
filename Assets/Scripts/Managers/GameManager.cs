using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class GameManager : Singleton<GameManager>
{
    private Player player;
    private CinemachineVirtualCamera mainCam;

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

    public void FindingMainCam(Scene scene, LoadSceneMode mode)
    {
        print($"현재 씬 : {scene.name})");
        player = FindObjectOfType<Player>();
        mainCam = FindObjectOfType<CinemachineVirtualCamera>();

        if (mainCam != null && player != null)
            mainCam.m_Follow = player.transform;

    }

}
