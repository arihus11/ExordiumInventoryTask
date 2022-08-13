using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stats.Model;

[CreateAssetMenu]
public class CharacterStatAgilityModifier : CharacterStatModifierSO
{
    [SerializeField]
    private StatsSO _statData;

    public override bool AffectCharacter(StatsSO statData, int val, bool increase)
    {
         bool success;
        if(increase)
        {
            success = _statData.SetStatValue(StatType.AGILITY, val);
        }
        else{
            success = _statData.ResetStatValue(StatType.AGILITY, val);
        }
        return success;
        
    }
}
