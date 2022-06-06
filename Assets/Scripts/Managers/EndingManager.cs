using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingManager : MonoBehaviour
{
    public GameObject[] maps;

    private void Update()
    {
        foreach (GameObject map in maps)
        {
            if (map.transform.localPosition.x >= 64f)
            {
                map.transform.localPosition -= Vector3.right * 32 * maps.Length;
            }
            map.transform.localPosition -= Vector3.left * 15 * Time.deltaTime;
        }
    }
}
