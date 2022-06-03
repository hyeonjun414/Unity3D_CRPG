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
            case SpellType.HealthUp:
                HealthUp(monster);
                break;
            case SpellType.Meditation:
                Meditation(monster);
                break;
            case SpellType.Evolution:
                Evolution(monster);
                break;
            case SpellType.Acceleration:
                Acceleration(monster);
                break;
            case SpellType.Honesty:
                Honesty(monster);
                break;
            case SpellType.Sacrifice:
                Sacrifice(monster);
                break;
        }
        BattleManager.Instance.player.UseMp(spell.cost);
    }
    public void HealthUp(Monster monster)
    {
        GameManager.Instance.CreateText("체력 증진", monster.transform.position, TextType.Spell);
        monster.maxHp = (int)(monster.maxHp * 1.2f);
        monster.HP = monster.maxHp;
        monster.statusBar.UpdateUI();
        UIManager.Instance.battleInfoUI.UpdateUI();
    }
    public void Meditation(Monster monster)
    {
        GameManager.Instance.CreateText("명상", monster.transform.position, TextType.Spell);
        monster.curMp = monster.maxMp;
        monster.statusBar.UpdateUI();
        UIManager.Instance.battleInfoUI.UpdateUI();
    }

    public void Evolution(Monster monster)
    {
        GameManager.Instance.CreateText("진화", monster.transform.position, TextType.Spell);
        if (monster.monsterData.nextMonster != null)
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
    public void Acceleration(Monster monster)
    {
        GameManager.Instance.CreateText("가속", monster.transform.position, TextType.Spell);
        monster.attackSpeed = monster.attackSpeed * 1.5f;
    }
    public void Honesty(Monster monster)
    {
        GameManager.Instance.CreateText("정직함", monster.transform.position, TextType.Spell);
        monster.maxMp *= 3;
        monster.damage *= 2;
        monster.statusBar.UpdateUI();
        UIManager.Instance.battleInfoUI.UpdateUI();
    }
    public void Sacrifice(Monster monster)
    {
        GameManager.Instance.CreateText("제물", monster.transform.position, TextType.Spell);

        switch (monster.owner)
        {
            case MonsterOwner.Player:
                BattleManager.Instance.allyMonster.Remove(monster);
                break;
            case MonsterOwner.Enemy:
                BattleManager.Instance.enemyMonster.Remove(monster);
                break;
        }
        monster.curTile.monster = null;
        monster.curTile.state = TileState.NONE;
        monster.ReturnPool();

        GameManager.Instance.player.MP = GameManager.Instance.player.maxMp;
        UIManager.Instance.battleInfoUI.UpdateUI();
    }
}
