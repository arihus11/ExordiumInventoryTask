using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model 
{
    [CreateAssetMenu]
    public class EquippableItemSO : ItemSO, IDestroyableItem, IItemAction
    {
        
        public string ActionName => "Equip";
        
        public bool PerformAction (GameObject character, bool increase)
        {
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
        HEAD,
        BODY,
        WEAPON,
        SHIELD, 
        NONE
    }

}

