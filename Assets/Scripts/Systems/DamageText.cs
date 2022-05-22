using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TextType
{
    Damage,
    Heal,
    Mana,
    Counter,
}

public class DamageText : MonoBehaviour, IPoolable
{
    [SerializeField] private Text text;

    public float duration;

    public Color textColor;
    public Color healColor;
    public Color damageColor;
    public Color manaColor;
    public Color counterColor;

    public void Enable(int damage, Vector3 position, TextType tt)
    {
        // 위치 설정
        transform.position = position;
        transform.LookAt(-Camera.main.transform.position);
        transform.Translate(Vector3.up*1.5f);
        // 데미지 텍스트 설정
        text.text = damage.ToString();

        switch(tt)
        {
            case TextType.Damage:
                textColor = damageColor;
                break;
            case TextType.Heal:
                textColor = healColor;
                break;
            case TextType.Mana:
                textColor = manaColor;
                break;
            case TextType.Counter:
                textColor = counterColor;
                break;
        }

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

        ReturnPool();




    }

    public void ReturnPool()
    {
        gameObject.SetActive(false);
        ObjectPoolManager.Instance.ReturnObj(gameObject);
    }
}
