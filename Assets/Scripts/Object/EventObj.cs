using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventObj : MonoBehaviour
{
    private void Start()
    {
        EventManager.Instance.SetRandomEvent();
    }

    public void PlayEvent()
    {
        EventManager.Instance.ActivateEvent();
        Destroy(gameObject);
    }

}
