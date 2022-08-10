using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Inventory.Model
{
    [CreateAssetMenu]
    public class ConsumableItemSO : ItemSO, IDestroyableItem, IItemAction
    {   
        [field: SerializeField]

        public string ActionName => "Consume";

        public bool PerformAction(GameObject character, bool increase)
        {
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


