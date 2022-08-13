using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory.Model;
using Equippement.Model;

public class PickUpMechanics : MonoBehaviour
{
    [SerializeField]
    private InventorySO _inventoryData;

    [SerializeField]
    private EquippementSO _equippementData;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(CollisionDetection._pickUpEnabled)
            {
                Item item = CollisionDetection.ItemInRange;
                if(item.SingleItem.PermanentUsage)
                {
                    SingleItem newItem = new SingleItem
                        {
                            Item = item.SingleItem,
                            Quantity = item.Quantity 
                        };
                        IItemAction itemAction = newItem.Item as IItemAction;
                        bool successEquippement = false;
                        successEquippement = itemAction.PerformAction(gameObject, true);
                    Debug.Log("Permanent usage item " + item.SingleItem.Name + " has been used!");
                    item.DestroyItem();
                }
                else
                {
                    if(CheckIsCanBeEquipped(item.SingleItem))
                    {
                        
                        SingleItem newItem = new SingleItem
                        {
                            Item = item.SingleItem,
                            Quantity = item.Quantity 
                        };
                        IItemAction itemAction = newItem.Item as IItemAction;
                        bool successEquippement = false;
                        successEquippement = itemAction.PerformAction(gameObject, true);
                        _equippementData.EquipItem(item.SingleItem, item.SingleItem.EquipType);
                        item.DestroyItem();
                            
                    }
                    else
                    {
                        int reminder = _inventoryData.AddItem(item.SingleItem, item.Quantity);
                        if(reminder == 0)
                        {
                            item.DestroyItem();
                        }
                        else
                        {
                            item.Quantity = reminder;
                        }
                    }
                }
            }
        }
    }

    public bool CheckIsCanBeEquipped(ItemSO item)
    {
        if(item.EquipType != EquipType.NONE)
        {
            switch(item.EquipType)
            {
                case EquipType.HEAD:
                    if(_equippementData.GetEquippementAt(EquipType.HEAD).IsEmpty)
                    {
                         return true;
                    }
                    return false;
                case EquipType.BODY:
                    if(_equippementData.GetEquippementAt(EquipType.BODY).IsEmpty)
                    {
                         return true;
                    }
                    return false;
                case EquipType.WEAPON:
                    if(_equippementData.GetEquippementAt(EquipType.WEAPON).IsEmpty)
                    {
                         return true;
                    }
                    return false;
                case EquipType.SHIELD:
                     if(_equippementData.GetEquippementAt(EquipType.SHIELD).IsEmpty)
                    {
                         return true;
                    }
                    return false;
                default:
                    return false;
            }
        }
        return false;
    }
}
