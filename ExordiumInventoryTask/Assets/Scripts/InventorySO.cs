using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Inventory.UI;
using Equippement.Model;


namespace Inventory.Model
{
[CreateAssetMenu]
public class InventorySO : ScriptableObject
{
    [SerializeField]
    private List<SingleItem> _inventoryItems;

    public static bool AddNewEmptyRow = false;
    public static bool RemoveEmptyRow = false;

    private InventorySlots _slots;

    [SerializeField]
    private GameObject _itemPrefab;

    [field: SerializeField]
    public int Size {get;private set;} = 10;

    public event Action<Dictionary<int, SingleItem>> OnInventoryUpdated;

    public void Initialize()
    {
        _inventoryItems = new List<SingleItem>();
        for(int i=0; i<Size; i++)
        {
            _inventoryItems.Add(SingleItem.GetEmptyItem());
        }
    }

    public int AddItem(ItemSO item, int quantity)
    {
        CreateNewInventoryRow();
            if(item.IsStackable == false)
        {
            for(int i=0; i<_inventoryItems.Count; i++)
            {       
                while(quantity > 0)
                {
                    quantity -= AddItemToFirstFreeSlot(item,1);
                }
                InformAboutChange();
                return quantity;    
            }
        }
        quantity = AddStackableItem(item,quantity);
        InformAboutChange();
        return quantity;
    }

    public void RemoveItem(int index, int amount)
    {
        if(_inventoryItems.Count > index)
        {
            if(_inventoryItems[index].IsEmpty){
                return;
            }
            int reminder = _inventoryItems[index].Quantity - amount;
            _itemPrefab.GetComponent<Item>().SetSingleItem(_inventoryItems[index].Item);
            _itemPrefab.GetComponent<Item>().SetQuantitiy(amount);
            if(reminder <= 0)
            {
                _inventoryItems[index] = SingleItem.GetEmptyItem();
            }
            else
            {
                _inventoryItems[index] = _inventoryItems[index].ChangeQuantity(reminder);
            }
            GameObject obj = Instantiate(_itemPrefab, Vector3.zero, Quaternion.identity);
            obj.transform.SetParent(GameObject.Find("Items").gameObject.transform);
            obj.transform.position = GameObject.Find("Character").gameObject.transform.position;
            InformAboutChange();
        }
        CheckIfRowEmpty();
    }

    private bool CheckIfRowEmpty()
    {
       int emptyCount = 0;
       for(int i=0; i<_inventoryItems.Count; i++)
           {
            if(_inventoryItems[i].IsEmpty)
               {
                     emptyCount++;
                }
                else
                {
                     emptyCount = 0;
                }
            }
        if(emptyCount == 7)
        {
            Debug.Log("Empty row should be removed!");
        }
       return false;
    }

    private int AddItemToFirstFreeSlot(ItemSO item, int quantity)
    {   
         SingleItem newItem = new SingleItem
         {
            Item = item,
            Quantity = quantity 
         };
        
         for(int i=0; i<_inventoryItems.Count; i++)
         {
            if(_inventoryItems[i].IsEmpty)
            {
                _inventoryItems[i] = newItem;
                return quantity;
            }
         }
         return 0;
    }

    private void CreateNewInventoryRow()
    {
        if(CountInventoryItems()  == _inventoryItems.Count)
        {
            Size += 7;
            AddNewEmptyRow = true;
            for(int i =0; i<7 ; i++)
            {   
                _inventoryItems.Add(SingleItem.GetEmptyItem());
            }
        }
    }

    private int CountInventoryItems()
    {
        int count = 0;
        foreach(SingleItem item in _inventoryItems)
        {
            if(!item.IsEmpty)
            {
                count++;
            }
        }
        return count;
    }

    private bool IsInventoryFull()
        => _inventoryItems.Where(item => item.IsEmpty).Any() == false;

    private int AddStackableItem(ItemSO item, int quantity)
    {
        for(int i=0;i<_inventoryItems.Count; i++)
        {
            if(_inventoryItems[i].IsEmpty)
            {
                continue;
            }
            if(_inventoryItems[i].Item.ID == item.ID)
            {
                int amountPossible = _inventoryItems[i].Item.MaxStackSize- _inventoryItems[i].Quantity;
                if(quantity > amountPossible)
                {
                    _inventoryItems[i] = _inventoryItems[i].ChangeQuantity(_inventoryItems[i].Item.MaxStackSize);
                    quantity -= amountPossible;
                }
                else
                {
                    _inventoryItems[i] = _inventoryItems[i].ChangeQuantity(_inventoryItems[i].Quantity + quantity);
                    InformAboutChange();
                    return 0;
                }
            }
        }
        while(quantity > 0)
        {
            int newQuantity = Mathf.Clamp(quantity,0, item.MaxStackSize);
            quantity -= newQuantity;
            AddItemToFirstFreeSlot(item, newQuantity);
        }
        return quantity;
    }

    public void AddItem(SingleItem item)
    {
        AddItem(item.Item, item.Quantity);
    }

    public Dictionary<int, SingleItem> GetCurrentInventoryState()
    {
        Dictionary <int, SingleItem> returnValue = new Dictionary <int, SingleItem>();
        for(int i=0; i<_inventoryItems.Count; i++){
            if(_inventoryItems[i].IsEmpty){
                continue;
            }
            returnValue[i] = _inventoryItems[i];
        }
        return returnValue;
    }

    public SingleItem GetItemAt(int index){
        return _inventoryItems[index];
    }

    public void SwapItems(int index1, int index2){
        SingleItem item1 = _inventoryItems[index1];
        _inventoryItems[index1] = _inventoryItems[index2];
        _inventoryItems[index2] = item1;
        InformAboutChange();
        CheckIfRowEmpty();
    }
    private void InformAboutChange(){
        OnInventoryUpdated?.Invoke(GetCurrentInventoryState());
    }

}

    [Serializable]
    public struct SingleItem
    {
        public int Quantity;
        public ItemSO Item;

        public bool IsEmpty => Item == null;

        public SingleItem ChangeQuantity (int newQuant)
        {
            return new SingleItem{
                Item = this.Item,
                Quantity = newQuant,
            };
        }
        
        public static SingleItem GetEmptyItem()
            => new SingleItem{
                Item = null,
                Quantity = 0,
            };
    }

}
