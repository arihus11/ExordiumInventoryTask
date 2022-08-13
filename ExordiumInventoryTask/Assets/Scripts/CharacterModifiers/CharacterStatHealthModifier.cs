using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stats.Model;

[CreateAssetMenu]
public class CharacterStatHealthModifier : CharacterStatModifierSO
{
    [SerializeField]
    private StatsSO _statData;
    
    public override bool AffectCharacter(StatsSO statData, int val, bool increase)
    {
        bool success;
        if(increase)
        {
           success = _statData.SetStatValue(StatType.HEALTH, val);
        }
        else{
           success = _statData.ResetStatValue(StatType.HEALTH, val);
        }
        return success;
    }
}
