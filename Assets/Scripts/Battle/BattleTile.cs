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
    public MeshRenderer mr;
    public TileState state = TileState.NONE;

    [SerializeField]
    public Monster monster;
    public Vector2 tilePos;
    
    Color originColor;
    Color overColor;

    private void Start()
    {
        originColor = mr.material.color;
        overColor = new Color(0.8f, 0.45f, 1); //pupple
    }

    private void OnMouseEnter()
    {
        mr.material.color = overColor;
    }
    private void OnMouseExit()
    {
        mr.material.color = originColor;
    }
    private void OnMouseUp()
    {
        if(CardManager.Instance.holder.selectedCardIndex != -1 && monster == null)
        {
            CardManager cm = CardManager.Instance;
            MonsterData card = (MonsterData)cm.hands[cm.holder.selectedCardIndex];
            monster = Instantiate(card.monster, transform.position, Quaternion.LookRotation(Vector3.right));
            monster.returnTile = this;

            cm.MoveCard(CardSpace.Hands, CardSpace.Graveyard, card);

            cm.holder.selectedCardIndex = -1;
            cm.guideLine.enabled = false;
        }
        
    }
}
