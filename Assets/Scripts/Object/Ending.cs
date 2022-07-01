using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 0.4f;
    }
    public void ReturnToTitle()
    {
        Time.timeScale = 1f;
        LoadingManager.LoadScene("TitleScene");
        //SceneManager.LoadSceneAsync("TitleScene");
    }
}
