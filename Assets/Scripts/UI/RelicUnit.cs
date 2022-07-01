using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RelicUnit : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image icon;

    RelicData data;
    public void UpdateUI(RelicData data)
    {
        gameObject.SetActive(true);
        this.data = data;
        icon.sprite = data.image;
    }
    public void ResetUI()
    {
        gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        UIManager.Instance.relicInfoUI.InfoEnter(data);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIManager.Instance.relicInfoUI.InfoExit();
    }
}
