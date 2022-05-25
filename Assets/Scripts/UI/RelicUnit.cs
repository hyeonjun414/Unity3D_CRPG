using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RelicUnit : MonoBehaviour
{
    public Image icon;

    public void UpdateUI(RelicData data)
    {
        gameObject.SetActive(true);

        icon.sprite = data.image;
    }
    public void ResetUI()
    {
        gameObject.SetActive(false);
    }
}
