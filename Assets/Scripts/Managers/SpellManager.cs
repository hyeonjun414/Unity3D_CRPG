using System.Collections;
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
            BattleTile bt = monster.returnTile;
            Destroy(monster.gameObject);
            bt.monster = SummonManager.Instance.SummonMonster(md, bt, MonsterOwner.Player);
        }
    }
}
