using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageText : MonoBehaviour
{
    [SerializeField] private Text text;

    public float duration;
    public Color textColor;

    public void Enable(int damage, Vector3 position)
    {
        // 오브젝트 활성화
        gameObject.SetActive(true);
        // 위치 설정
        transform.position = position;
        transform.LookAt(-Camera.main.transform.position);
        transform.Translate(Vector3.up);
        // 데미지 텍스트 설정
        text.text = damage.ToString();
        textColor = text.color;

        StartCoroutine(FloatingRoutine());
    }

    IEnumerator FloatingRoutine()
    {
        float curTime = 0;
        float alpha = 0;

        while(curTime < duration)
        {
            curTime += Time.deltaTime;
            alpha = 1 - curTime / duration;
            textColor.a = alpha;
            transform.Translate(Vector3.up * Time.deltaTime);
            text.color = textColor;
            yield return null;
        }

        Destroy(gameObject);
        


    }
}
