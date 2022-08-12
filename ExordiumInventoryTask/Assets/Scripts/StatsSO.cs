using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Equippement.Model;


namespace Stats.Model
{
    [CreateAssetMenu]
    public class StatsSO : ScriptableObject
    {
        [SerializeField]
        private int _health,_agility,_attack,_defense;

        public void InitializeStats()
        {
                _health = 90;
                _agility = 100;
                _attack = 0;
                _defense = 0;
        }

        public bool SetStatValue(StatType statType, int val)
        {
            bool availability;
            switch(statType)
            {
                case StatType.HEALTH:
                    availability = CheckOverflow(_health,val);
                    if(availability)
                    {
                        _health += val;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case StatType.AGILITY:
                    availability = CheckOverflow(_agility,val);
                    if(availability)
                    {
                        _agility += val;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case StatType.ATTACK:
                    availability = CheckOverflow(_attack,val);
                    if(availability)
                    {
                        _attack += val;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case StatType.DEFENSE:
                    availability = CheckOverflow(_defense,val);
                    if(availability)
                    {
                        _defense += val;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                default:
                    return false;
            }
            return false;
        }

        public bool ResetStatValue(StatType statType, int val)
        {
            bool availability;
            switch(statType)
            {
                case StatType.HEALTH:
                     availability = CheckUnderflow(_health,val);
                    if(availability)
                    {
                        _health -= val;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case StatType.AGILITY:
                     availability = CheckUnderflow(_agility,val);
                    if(availability)
                    {
                        _agility -= val;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case StatType.ATTACK:
                     availability = CheckUnderflow(_attack,val);
                    if(availability)
                    {
                        _attack -= val;
                        return true;
                    }
                    else
                    {
                       return false;
                    }
                case StatType.DEFENSE:
                     availability = CheckUnderflow(_defense,val);
                    if(availability)
                    {
                        _defense -= val;
                        return true;
                    }
                    else
                    {
                       return false;
                    }
                default:
                    return false;
            }
            return false;
        }

        public int GetStatFor(StatType type)
        {
            switch(type)
            {
                case StatType.HEALTH:
                    return _health;
                case StatType.AGILITY:
                    return _agility;
                case StatType.ATTACK:
                    return _attack;
                case StatType.DEFENSE:
                     return _defense;
                default:
                    return 0;
            }
        }

        public bool CheckOverflow(int stat, int value)
        {
            if((stat + value) > 100)
            {
                Debug.Log("Unable to perform action: Overflow detected!");
                return false;
            }
            return true;
        }

        public bool CheckUnderflow(int stat, int value)
        {
            if((stat - value) < 0)
            {
                Debug.Log("Unable to perform action: Underflow detected!");
                return false;
            }
            return true;
        }
    }

    public enum StatType
    {
            HEALTH,
            AGILITY,
            ATTACK,
            DEFENSE
    }
}
