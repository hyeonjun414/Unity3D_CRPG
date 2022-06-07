using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    public static string nextScene;

    [SerializeField]
    Image progressBar;
    [SerializeField]
    Animator anim;

    AsyncOperation op;

    private void Start()
    {
        //StartCoroutine(LoadScene());
    }

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");

    }
    IEnumerator LoadScene()
    {
        yield return null;

        op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;

        while(!op.isDone)
        {
            yield return null;

            if(op.progress >= 0.9f)
            {
                anim.SetTrigger("LoadComplete");
                yield break;
            }
        }
    }
    public void LoadStart()
    {
        StartCoroutine(LoadScene());
    }
    public void LoadComplate()
    {
        op.allowSceneActivation = true;

        op = null;
    }
}
