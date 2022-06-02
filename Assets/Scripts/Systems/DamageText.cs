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
    Skill
}

public class DamageText : MonoBehaviour, IPoolable
{
    [SerializeField] private Text floatingText;

    public float duration;

    public Color textColor;
    public Color healColor;
    public Color damageColor;
    public Color manaColor;
    public Color counterColor;
    public Color skillColor;

    public void Enable(int damage, Vector3 position, TextType tt)
    {
        // 위치 설정
        transform.position = position;
        transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
        float randX = Random.Range(-0.25f, 0.25f);
        float randY = Random.Range(-0.25f, 0.25f);
        transform.Translate(Vector3.up*1.5f + new Vector3(randX, randY, -10));
        // 데미지 텍스트 설정
        floatingText.text = damage.ToString();

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
            case TextType.Skill:
                textColor = skillColor;
                break;
        }

        StartCoroutine(FloatingRoutine());
    }

    public void Enable(string text, Vector3 position, TextType tt)
    {
        // 위치 설정
        transform.position = position;
        transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
        transform.Translate(Vector3.up * 1.25f + new Vector3(0, 0, -20));
        
        // 데미지 텍스트 설정
        floatingText.text = text;

        switch (tt)
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
            case TextType.Skill:
                textColor = skillColor;
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
            floatingText.color = textColor;
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
