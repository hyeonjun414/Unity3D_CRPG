using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StreamSceneTrigger : MonoBehaviour
{
    [SerializeField]
    private string streamTargetScene;
    [SerializeField]
    private string triggerOwnScene;

    private IEnumerator StreamingTargetScene()
    {
        Scene targetScene = SceneManager.GetSceneByName(streamTargetScene);
        if(!targetScene.isLoaded)
        {
            AsyncOperation op = SceneManager.LoadSceneAsync(streamTargetScene, LoadSceneMode.Additive);
            
            while(!op.isDone)
            {
                yield return null;
            }
        }
    }

    private IEnumerator UnloadStreamScene()
    {
        Scene targetScene = SceneManager.GetSceneByName(streamTargetScene);
        if (targetScene.isLoaded)
        {
            Scene currentScene = SceneManager.GetSceneByName(triggerOwnScene);

            AsyncOperation op = SceneManager.UnloadSceneAsync(streamTargetScene);

            while (!op.isDone)
            {
                yield return null;
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            float dir = Vector3.Angle(transform.forward, other.transform.position - transform.position);
            if(dir < 90f)
            {
                StartCoroutine(StreamingTargetScene());
            }
            else
            {
                StartCoroutine(UnloadStreamScene());
            }
        }
    }
}
