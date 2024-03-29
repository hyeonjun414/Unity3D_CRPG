﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public GameObject[] maps;

    public GameObject optionUI;
    private void Start()
    {
        SetResolution();
    }
    private void Update()
    {
        foreach (GameObject map in maps)
        {
            if(map.transform.localPosition.x <= -64f)
            {
                map.transform.localPosition += Vector3.right * 32 * maps.Length;
            }
            map.transform.localPosition += Vector3.left * 15 * Time.deltaTime;
        }
    }

    public void SetResolution()
    {
        int w = 1600;
        int h = 900;

        Screen.SetResolution(w, h, false);
    }

    public void BtnPlayGame()
    {
        LoadingManager.LoadScene("StageStartScene");
        //SceneManager.LoadSceneAsync("StageStartScene");
    }
    public void BtnOpenOption()
    {
        optionUI.SetActive(true);
    }
    public void BtnExitOption()
    {
        optionUI.SetActive(false);
    }
    public void BtnExitGame()
    {
        Application.Quit();
    }
}
