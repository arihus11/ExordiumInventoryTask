using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stats.Model;

public abstract class CharacterStatModifierSO : ScriptableObject
{
    public abstract bool AffectCharacter(StatsSO statData, int val, bool incerase);
}
