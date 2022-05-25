using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileState
{
    NONE,
    STAY
}

public class BattleTile : MonoBehaviour
{
    /// <summary>
    /// 몬스터 소환, 주문 적용 등의 기능을 하는 타일이다.
    /// </summary>
    public int tileIndex;
    public int maxTileIndex;

    public MeshRenderer mr;
    public TileState state = TileState.NONE;

    [SerializeField]
    public Monster monster;
    public Vector2 tilePos;
    
    Color originColor;
    Color overColor;
    public Color allyStayColor;
    public Color enemyStayColor;

    private void Start()
    {
        originColor = mr.material.color;
        overColor = new Color(0.8f, 0.45f, 1); //pupple
    }
    private void Update()
    {
        if(state == TileState.STAY)
        {
            if(monster != null)
            {
                switch (monster.owner)
                {
                    case MonsterOwner.Player:
                        mr.material.color = allyStayColor;
                        break;
                    case MonsterOwner.Enemy:
                        mr.material.color = enemyStayColor;
                        break;
                }
            }
        }
        else
        {
            mr.material.color = originColor;
        }
    }
    private void OnMouseEnter()
    {
        if (monster != null)
            UIManager.Instance.monsterInfoUI.InfoEnter(monster.monsterData);
        mr.material.color = overColor;
    }
    private void OnMouseExit()
    {
        if (monster != null)
            UIManager.Instance.monsterInfoUI.InfoExit();
        mr.material.color = originColor;
    }
    private void OnMouseUp()
    {
        if (tileIndex < maxTileIndex / 2) return;
        // 만약 선택된 카드가 없다면 패스.
        if (CardManager.Instance.holder.selectedCardIndex == -1) return;

        // 선택된 카드가 있다면 선택된 카드 인덱스에 해당하는 카드 데이터를 가져온다.
        CardManager cm = CardManager.Instance;
        CardData cd = cm.hands[cm.holder.selectedCardIndex];

        // 가져온 카드 데이터의 타입에 따라 기능을 수행한다.
        switch(cd.cardType)
        {
            case CardType.Monster:
                SummonMonster(cd);
                break;
            case CardType.Spell:
                CastSpell(cd);
                break;
        }
    }
    public void SummonMonster(CardData data)
    {
        if (monster != null)
        {
            // 조건에 안맞는 경우 카드 선택 해제.
            CardManager.Instance.ResetSelectedCard();
            return;
        }

        // 몬스터 데이터로 만들어준다.
        MonsterData md = (MonsterData)data;
        // 몬스터를 생성.
        monster = SummonManager.Instance.SummonMonster(md, this, MonsterOwner.Player);
        if (monster == null)
            return;
        // 사용된 카드를 무덤으로 이동시키고, 카드 선택을 초기화한다.
        CardManager.Instance.MoveCard(CardSpace.Hands, CardSpace.Field, md);
        CardManager.Instance.ResetSelectedCard();

    }
    public void CastSpell(CardData data)
    {
        if (monster == null)
        {
            // 조건에 안맞는 경우 카드 선택 해제.
            CardManager.Instance.ResetSelectedCard();
            return;
        }

        // 주문 데이터로 변경한다.
        SpellData bd = (SpellData)data;
        SpellManager.Instance.CastSpell(monster, bd);
        // 사용된 카드를 무덤으로 이동시키고, 카드 선택을 초기화한다.
        CardManager.Instance.MoveCard(CardSpace.Hands, CardSpace.Graveyard, bd);
        CardManager.Instance.ResetSelectedCard();

    }
}
