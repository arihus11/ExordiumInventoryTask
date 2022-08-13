using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stats.Model;

namespace Inventory.Model 
{
    [CreateAssetMenu]
    public class EquippableItemSO : ItemSO, IDestroyableItem, IItemAction
    {
        [field: SerializeField]
        private List<ModifierData> modifiersData = new List<ModifierData>();

        [SerializeField]
        private StatsSO _statData;
        
        public string ActionName => "Equip";
        
        public bool PerformAction (GameObject character, bool increase)
        {
            foreach(ModifierData data in modifiersData)
            {
                // Bug: Provjeri prvo za agility i tu nema overflowa i izvede ga, a za npr. attack postoji overflow i njega ne izvede
                // Rezultat: Agility se smanji, a item se ne equippa niti se promijeni drugi modifier
                if(data.statModifier.AffectCharacter(_statData, data.value, increase) == false)
                {
                    return false;
                }
                
            }
            if(increase)
            {
                AgentWeapon weaponSystem = character.GetComponent<AgentWeapon>();
                if(weaponSystem != null)
                {
                    weaponSystem.SetWeapon(this, this.EquipType);
                        Debug.Log("New item equipped");
                        return true;

                }
            }
            return true;
        }
    }

    public enum EquipType
    {
        NONE,
        BODY,
        WEAPON,
        SHIELD, 
        HEAD
    }

}

