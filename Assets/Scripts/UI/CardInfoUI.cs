using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardInfoUI : MonoBehaviour
{
    [Header("Position")]
    public RectTransform canvas;
    public Camera cam;
    private Vector2 mousePos;
    public float offsetX;
    public float offsetY;


    [Header("Common")]
    public Text nameText;
    public Text costText;
    public Text typeText;

    [Header("Monster Card")]
    public GameObject monsterObj;
    public Text monDescText;
    public Text monSkillNameText;
    public Text monSkillDescText;
    public Text monPreEvoText;
    public Text monCurEvoText;
    public Text monNextEvoText;
    public Text monHpText;
    public Text monMpText;
    public Text monDamageText;
    public Text monArmorText;
    public Text monRangeText;
    public Text monAttackSpeedText;

    [Header("Spell Card")]
    public GameObject spellObj;
    public Image spellIcon;
    public Text spellDescText;

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

    public void InfoEnter(CardData data)
    {
        SetPosition();

        nameText.text = data.name;
        costText.text = data.cost.ToString();

        switch(data.cardType)
        {
            case CardType.Monster:
                typeText.text = "몬스터";
                SetMonsterUI((MonsterData)data);
                break;
            case CardType.Spell:
                typeText.text = "주문";
                SetSpellUI((SpellData)data);
                break;
        }
        gameObject.SetActive(true);
    }
    public void InfoEnter(Monster monster)
    {
        SetPosition();
        monsterObj.SetActive(true);
        spellObj.SetActive(false);

        MonsterData data = monster.monsterData;
        nameText.text = data.name;
        costText.text = data.cost.ToString();
        typeText.text = "몬스터";

        monDescText.text = data.desc;
        monSkillNameText.text = data.skillData != null ? data.skillData.skillName : "X";
        monSkillDescText.text = data.skillData != null ? data.skillData.skillDesc : "해당 몬스터는 스킬이 없습니다";
        monPreEvoText.text = data.prevMonster != null ? data.prevMonster.name : "X";
        monCurEvoText.text = data.name;
        monNextEvoText.text = data.nextMonster != null ? data.nextMonster.name : "X";
        monHpText.text = monster.maxHp.ToString();
        monMpText.text = monster.maxMp.ToString();
        monDamageText.text = monster.damage.ToString();
        monArmorText.text = monster.armor.ToString();
        monRangeText.text = monster.range.ToString();
        monAttackSpeedText.text = monster.attackSpeed.ToString();

        gameObject.SetActive(true);
    }
    public void InfoExit()
    {
        gameObject.SetActive(false);
    }

    public void SetMonsterUI(MonsterData data)
    {
        monsterObj.SetActive(true);
        spellObj.SetActive(false);

        monDescText.text = data.desc;
        monSkillNameText.text = data.skillData != null ? data.skillData.skillName : "X";
        monSkillDescText.text = data.skillData != null ? data.skillData.skillDesc : "해당 몬스터는 스킬이 없습니다";
        monPreEvoText.text = data.prevMonster != null ? data.prevMonster.name : "X";
        monCurEvoText.text = data.name;
        monNextEvoText.text = data.nextMonster != null ? data.nextMonster.name : "X";
        monHpText.text = data.hp.ToString();
        monMpText.text = data.mp.ToString();
        monDamageText.text = data.damage.ToString();
        monArmorText.text = data.armor.ToString();
        monRangeText.text = data.range.ToString();
        monAttackSpeedText.text = data.attackSpeed.ToString();


    }
    public void SetSpellUI(SpellData data)
    {
        monsterObj.SetActive(false);
        spellObj.SetActive(true);

        spellIcon.sprite = data.icon;
        spellDescText.text = data.desc;
    }
}
