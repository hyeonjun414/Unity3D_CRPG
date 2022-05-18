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

    [Header("Evolution Info")]
    public Text nextEvo;
    public Text curEvo;
    public Text prevEvo;

    [Header("Offset")]
    public float offsetX;
    public float offsetY;

    [Header("Position")]
    public RectTransform canvas;
    public Camera cam;



    [Header("Card Data")]
    public CardData cardData;
    [Header("Card UI")]
    public CardUI cardUI;

    private Vector2 mousePos;
    private void Start()
    {
        cam = Camera.main;
    }
    private void Update()
    {
        Vector2 originMouse = Input.mousePosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas, originMouse, cam, out mousePos);
        Vector2 offset = Vector2.zero;
        if (originMouse.x < Screen.width / 2)
            offset += Vector2.right * offsetX;
        else
            offset += Vector2.left * offsetX;

        if (originMouse.y < Screen.height / 2)
            offset += Vector2.up * offsetY;
        else
            offset += Vector2.down * offsetY;

        transform.localPosition = mousePos + offset;
    }

    public void InfoEnter(CardData data)
    {
        MonsterData md = (MonsterData)data;
        gameObject.SetActive(true);
        cardUI.AddCard(data);
        infoName.text = data.name;
        costText.text = data.cost.ToString();
        descText.text = data.desc;

        curEvo.text = md.name;
        nextEvo.text = md.nextMonster != null ? md.nextMonster.name : "X";
        prevEvo.text = md.prevMonster != null ? md.prevMonster.name : "X";
    }
    public void InfoExit()
    {
        gameObject.SetActive(false);
        cardData = null;
        infoName.text = "";
        costText.text = "";
    }

}
