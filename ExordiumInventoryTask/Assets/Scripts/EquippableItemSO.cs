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

