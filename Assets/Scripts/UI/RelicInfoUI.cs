using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RelicInfoUI : MonoBehaviour
{
    public Text relicName;
    public Text relicDesc;

    
    

    [Header("Position")]
    public RectTransform canvas;
    public Camera cam;
    private Vector2 mousePos;
    public float offsetX;
    public float offsetY;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        SetPosition();
    }
    public void SetPosition()
    {
        Vector3 originMouse = Input.mousePosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas, originMouse, cam, out mousePos);
        Vector3 offset = Vector2.zero;
        if (originMouse.x < Screen.width / 2)
            offset += Vector3.right * offsetX;
        else
            offset += Vector3.left * offsetX;

        if (originMouse.y < Screen.height / 2)
            offset += Vector3.up * offsetY;
        else
            offset += Vector3.down * offsetY;

        transform.localPosition = (Vector3)mousePos + offset + new Vector3(0, 0, -50);
    }

    public void InfoEnter(RelicData data)
    {
        SetPosition();
        gameObject.SetActive(true);
        relicName.text = data.relicName;
        relicDesc.text = data.relicDesc;
    }
    public void InfoExit()
    {
        gameObject.SetActive(false);
    }
}
