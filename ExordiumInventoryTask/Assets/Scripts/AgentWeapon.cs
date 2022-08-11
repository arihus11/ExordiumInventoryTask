using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory.Model;
using Equippement.Model;

public class AgentWeapon : MonoBehaviour
{

    [SerializeField]
    private InventorySO _inventoryData;

    [SerializeField]
    private EquippementSO _equippementData;

    public void SetWeapon(EquippableItemSO weaponItemSO, EquipType type)
    {   
        SingleEquipementItem itemForUnequip = SingleEquipementItem.GetEmptyItem();
        bool performStatsUnequip = false;
        switch(type)
        {
            case EquipType.HEAD:
                itemForUnequip = _equippementData.GetEquippementAt(EquipType.HEAD);
                if(!itemForUnequip.IsEmpty)
                {
                    performStatsUnequip = true;
                    _inventoryData.AddItem(itemForUnequip.Item, 1);
                }
                break;
            case EquipType.BODY:
                itemForUnequip = _equippementData.GetEquippementAt(EquipType.BODY);
                if(!itemForUnequip.IsEmpty)
                {
                     performStatsUnequip = true;
                    _inventoryData.AddItem(itemForUnequip.Item, 1);
                }
                break;
            case EquipType.SHIELD:
                itemForUnequip = _equippementData.GetEquippementAt(EquipType.SHIELD);
                if(!itemForUnequip.IsEmpty)
                {
                     performStatsUnequip = true;
                    _inventoryData.AddItem(itemForUnequip.Item, 1);
                }
                break;
            case EquipType.WEAPON:
                itemForUnequip = _equippementData.GetEquippementAt(EquipType.WEAPON);
                if(!itemForUnequip.IsEmpty)
                {
                     performStatsUnequip = true;
                    _inventoryData.AddItem(itemForUnequip.Item, 1);
                }
                break;
        }
        if(performStatsUnequip)
        {
             IItemAction itemActionShield = itemForUnequip.Item as IItemAction;
            if(itemActionShield.ActionName == "Equip")
            {
                itemActionShield.PerformAction(gameObject,false);
            }
        }
    }
}
