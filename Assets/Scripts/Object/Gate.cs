using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gate : MonoBehaviour
{
    public string destination;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            SceneManager.LoadScene(destination);
        }
    }

}
