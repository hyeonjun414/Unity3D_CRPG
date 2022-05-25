﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : Singleton<SpellManager>
{
    private void Awake()
    {
        if (_instance == null)
            _instance = this;
    }
    public void CastSpell(Monster monster, SpellData spell)
    {
        switch(spell.spellType)
        {
            case SpellType.Heal:
                Heal(monster);
                break;
            case SpellType.Meditation:
                Meditation(monster);
                break;
            case SpellType.Evolution:
                Evolution(monster);
                break;
        }
        BattleManager.Instance.player.UseMp(spell.cost);
    }

    public void Meditation(Monster monster)
    {

    }
    public void Heal(Monster monster)
    {

    }
    public void Evolution(Monster monster)
    {
        if(monster.monsterData.nextMonster != null)
        {
            MonsterData md = monster.monsterData.nextMonster;
            BattleTile bt = monster.curTile;
            switch(monster.owner)
            {
                case MonsterOwner.Player:
                    BattleManager.Instance.allyMonster.Remove(monster);
                    break;
                case MonsterOwner.Enemy:
                    BattleManager.Instance.enemyMonster.Remove(monster);
                    break;
            }
            monster.ReturnPool();
            CardManager.Instance.graveyard.Remove(monster.monsterData);
            CardManager.Instance.graveyard.Add(md);
            bt.monster = SummonManager.Instance.SummonMonster(md, bt, MonsterOwner.Player);
            
        }
    }
}
