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

    public void Meditation(Monster monster, SpellData spell)
    {

    }
    public void Heal(Monster monster, SpellData spell)
    {

    }
    public void Evolution(Monster monster, SpellData spell)
    {

    }
}
