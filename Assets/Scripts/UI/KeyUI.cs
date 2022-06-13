using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyUI : MonoBehaviour
{
    Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.Instance.player;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + Vector3.up * 2.5f;
    }

    public void ItemEnter()
    {
        gameObject.SetActive(true);
    }
    public void ItemExit()
    {
        gameObject.SetActive(false);
    }
}
