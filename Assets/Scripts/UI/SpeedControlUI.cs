using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SpeedState
{
    Normal,
    Double,
    Quad
}

public class SpeedControlUI : MonoBehaviour
{
    [Header("Indicator")]
    public GameObject selectImg;
    public Text curSpeedText;
    public SpeedState speedState;

    [Header("Buttons")]
    public Button normalSpeedBtn;
    public Button doubleSpeedBtn;
    public Button quadSpeedBtn;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
            BtnNormal();
        }
        if(Input.GetKeyDown(KeyCode.F2))
        {
            BtnDouble();
        }
        if(Input.GetKeyDown(KeyCode.F3))
        {
            BtnQuad();
        }
    }

    public void ReturnSpeed()
    {
        switch(speedState)
        {
            case SpeedState.Normal:
                BtnNormal();
                break;
            case SpeedState.Double:
                BtnDouble();
                break;
            case SpeedState.Quad:
                BtnQuad();
                break;
        }
    }

    public void BtnNormal()
    {
        curSpeedText.text = "x 1";
        Time.timeScale = 1f;
        speedState = SpeedState.Normal;
        StopAllCoroutines();
        StartCoroutine(MoveIndicator(selectImg.transform.position, normalSpeedBtn.transform.position, 0.2f));
    }

    public void BtnDouble()
    {
        curSpeedText.text = "x 2";
        speedState = SpeedState.Double;
        Time.timeScale = 2f;
        StopAllCoroutines();
        StartCoroutine(MoveIndicator(selectImg.transform.position, doubleSpeedBtn.transform.position, 0.2f));
    }

    public void BtnQuad()
    {
        curSpeedText.text = "x 4";
        speedState = SpeedState.Quad;
        Time.timeScale = 4f;
        StopAllCoroutines();
        StartCoroutine(MoveIndicator(selectImg.transform.position, quadSpeedBtn.transform.position, 0.2f));
    }


    IEnumerator MoveIndicator(Vector3 p1, Vector3 p2, float time)
    {
        float curTime = 0f;
        while(true)
        {
            if (curTime > time)
                break;
            curTime += Time.unscaledDeltaTime;
            selectImg.transform.position = Vector3.Lerp(p1, p2, curTime/time);

            yield return null;
        }

    }
}
