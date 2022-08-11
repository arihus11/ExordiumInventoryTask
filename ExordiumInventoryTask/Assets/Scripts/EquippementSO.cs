using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory.Model;
using System;
using Equippement.UI;

namespace Equippement.Model
{
    [CreateAssetMenu]
    public class EquippementSO : ScriptableObject
    {

        [SerializeField]
        private SingleEquipementItem _headItem,_bodyItem,_weaponItem,_shieldItem;

        private List<SingleEquipementItem> _equippedItems;

        [SerializeField]
        private InventorySO _inventoryData;

        private GameObject _character;

        public event Action<Dictionary<EquipType,SingleEquipementItem>> OnEquippementUpdated;

        public void Initialize()
        {
            _character = GameObject.Find("Character").gameObject;
            _headItem = SingleEquipementItem.GetEmptyItem();
            _bodyItem = SingleEquipementItem.GetEmptyItem();
            _weaponItem = SingleEquipementItem.GetEmptyItem();
            _shieldItem = SingleEquipementItem.GetEmptyItem();
            for(int i=0; i<4; i++)
            {
                _equippedItems.Add(SingleEquipementItem.GetEmptyItem());
            }
        }

        public bool EquipItem(ItemSO item, EquipType type)
        {
            SingleEquipementItem newItem = new SingleEquipementItem
            {
            Item = item,
            Type = type
            };
            
            switch(type)
            {
                case EquipType.HEAD:
                    _headItem = newItem;
                    break;
                case EquipType.BODY:
                    _bodyItem = newItem;
                    break;
                case EquipType.WEAPON:
                    _weaponItem = newItem;
                    break;
                case EquipType.SHIELD:
                    _shieldItem = newItem;
                    break;
                default:
                    Debug.Log("EquipType doesn't exist");
                    break;
            }
            
            InformAboutEquippementChange();
            return true;
        }

        public void UnEquipItem(EquipType type, EquippementItem item)
        {
            SingleEquipementItem itemForAction = SingleEquipementItem.GetEmptyItem();

            switch(type)
            {
                case EquipType.HEAD:
                    itemForAction = _headItem;
                    _headItem = SingleEquipementItem.GetEmptyItem();
                    break;
                case EquipType.BODY:
                    itemForAction = _bodyItem;
                    _bodyItem = SingleEquipementItem.GetEmptyItem();
                    break;
                case EquipType.WEAPON:
                    itemForAction = _weaponItem;
                    _weaponItem = SingleEquipementItem.GetEmptyItem();
                    break;
                case EquipType.SHIELD:
                     itemForAction = _shieldItem;
                     _shieldItem = SingleEquipementItem.GetEmptyItem();
                    break;
            }
            _inventoryData.AddItem(itemForAction.Item, 1);
             IItemAction itemActionShield = itemForAction.Item as IItemAction;
             if(itemActionShield.ActionName == "Equip")
             {
                  itemActionShield.PerformAction(_character,false);
             }
             InformAboutEquippementChange();
        } 

        public SingleEquipementItem GetEquippementAt(EquipType type)
        {
            switch(type)
            {
                case EquipType.HEAD:
                    return _headItem;
                case EquipType.BODY:
                    return _bodyItem;
                case EquipType.WEAPON:
                    return _weaponItem;
                case EquipType.SHIELD:
                     return _shieldItem;
                default:
                    SingleEquipementItem emptyItem = SingleEquipementItem.GetEmptyItem();
                    return emptyItem;
            }
        }

        private void InformAboutEquippementChange()
        {
            OnEquippementUpdated?.Invoke(GetCurrentEquippementState());
        }

        public Dictionary<EquipType, SingleEquipementItem> GetCurrentEquippementState()
        {
            Dictionary <EquipType, SingleEquipementItem> returnValue = new Dictionary <EquipType, SingleEquipementItem>();
            for(int i=0; i<_equippedItems.Count; i++)
            {
                if(_equippedItems[i].IsEmpty)
                {
                    continue;
                }
                returnValue.Add(_equippedItems[i].Type, _equippedItems[i]);
            }
            return returnValue; 
        } 

        public bool IsTypeEquipped(EquipType type)
        {   
            switch(type)
            {
                case EquipType.HEAD:
                    if(_headItem.Item == null)
                        return false;
                return true;
                case EquipType.BODY:
                    if(_bodyItem.Item == null)
                        return false;
                    return true;
                case EquipType.SHIELD:
                    if(_shieldItem.Item == null)
                        return false;
                    return true;
                case EquipType.WEAPON:
                    if(_weaponItem.Item == null)
                         return false;
                     return true;
                deafult:
                    break;
            }
            return false;
        }
    }
    [Serializable]
        public struct SingleEquipementItem
        {
            public ItemSO Item;
            public EquipType Type;

            public bool IsEmpty => Item == null;
            
            public static SingleEquipementItem GetEmptyItem()
                => new SingleEquipementItem
                {
                    Item = null,
                    Type = EquipType.NONE  
                };
        }
}
