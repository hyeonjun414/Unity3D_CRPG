using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Monster Data", menuName = "Card/Monster Data")]
public class MonsterData : CardData
{
    [Header("Monster Data")]
    public int hp;
    public int mp;
    public int damage;
    public int armor;
    public int range;
    public float attackSpeed;
    public float moveSpeed;


    [Header("Monster Evolution")]
    public int evolutionCost;
    public MonsterData prevMonster;
    public MonsterData nextMonster;

    [Header("Monster Prefab")]
    public GameObject monster;
}
