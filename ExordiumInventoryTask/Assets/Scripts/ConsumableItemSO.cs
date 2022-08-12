using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Stats.Model;

namespace Inventory.Model
{
    [CreateAssetMenu]
    public class ConsumableItemSO : ItemSO, IDestroyableItem, IItemAction
    {   
        [field: SerializeField]
        private List<ModifierData> modifiersData = new List<ModifierData>();

        [SerializeField]
        private StatsSO _statData;

        public string ActionName => "Consume";

        public bool PerformAction(GameObject character, bool increase)
        {
            foreach(ModifierData data in modifiersData)
            {
                if(data.statModifier.AffectCharacter(_statData, data.value, increase) == false)
                    {
                        return false;
                    }

            }
            return true;
        }
    }

    public interface IDestroyableItem
    {

    }

    public interface IItemAction
    {
        string ActionName {get;}
        bool PerformAction (GameObject character, bool increase);
    }

}


