using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterInfoUI : MonoBehaviour
{
    [Header("Info UI")]
    public Text infoName;
    public Text costText;
    public Text descText;

    [Header("Offset")]
    public float offsetX;
    public float offsetY;

    [Header("Card Data")]
    public CardData cardData;
    [Header("Card UI")]
    public CardUI cardUI;

    private Vector3 mousePos;
    private void FixedUpdate()
    {
        mousePos = Input.mousePosition;

        Vector3 offset = Vector3.zero;
        if (mousePos.x < Screen.width / 2)
            offset += Vector3.right * offsetX;
        else
            offset += Vector3.left * offsetX;

        if (mousePos.y < Screen.height / 2)
            offset += Vector3.up * offsetY;
        else
            offset += Vector3.down * offsetY;

        transform.position = mousePos + offset;
    }

    public void InfoEnter(CardData data)
    {
        gameObject.SetActive(true);
        cardUI.AddCard(data);
        infoName.text = data.name;
        costText.text = data.cost.ToString();
        descText.text = data.desc;
    }
    public void InfoExit()
    {
        gameObject.SetActive(false);
        cardData = null;
        infoName.text = "";
        costText.text = "";
    }

}
