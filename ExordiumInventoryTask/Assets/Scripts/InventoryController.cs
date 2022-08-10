using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory.UI;
using Inventory.Model;

namespace Inventory
{
    public class InventoryController : MonoBehaviour
{
    [SerializeField]
    private InventorySlots _inventoryUI;

    [SerializeField]
    private InventorySO _inventoryData;

    public List<SingleItem> InitialItems = new List<SingleItem>();

    private void Start()
    {
        PrepareUI();
        PrepareInventoryData();
    }

    private void PrepareInventoryData()
    {
        _inventoryData.Initialize();
        _inventoryData.OnInventoryUpdated += UpdateInventoryUI;
        foreach(SingleItem item in InitialItems)
        {
            if(item.IsEmpty)
            {
                continue;
            }
            _inventoryData.AddItem(item);
        }
    }

    private void UpdateInventoryUI(Dictionary<int, SingleItem> inventoryState){
        _inventoryUI.ResetAllItems();
        foreach(var item in inventoryState){
            _inventoryUI.UpdateData(item.Key, item.Value.Item.ItemImage, item.Value.Quantity);
        }
    }

    private void PrepareUI()
    {
        _inventoryUI.InitializeInventory(_inventoryData.Size);
        this._inventoryUI.OnSelectRequested += HandleSelectRequested;
        this._inventoryUI.OnSwapItems += HandleSwapItems;
        this._inventoryUI.OnStartDragging += HandleDragging;
        this._inventoryUI.OnItemActionRequestedMiddle += HandleItemActionRequestedMiddle;
        this._inventoryUI.OnItemActionRequestedRight += HandleItemActionRequestedRight;
        this._inventoryUI.OnRemoveButtonPressed += HandleRemoveButtonRequest;
    }

    private void HandleRemoveButtonRequest(int index)
    {
        SingleItem inventoryItem = _inventoryData.GetItemAt(index);
        if(inventoryItem.IsEmpty)
        {
            return;
        }
         _inventoryData.RemoveItem(index,1);
    }

    private void HandleItemActionRequestedRight(int index)
    {
        SingleItem inventoryItem = _inventoryData.GetItemAt(index);
        if(inventoryItem.IsEmpty)
        {
            return;
        }

        IItemAction itemAction = inventoryItem.Item as IItemAction;
        if(itemAction.ActionName == "Equip")
        {
            bool successEquippement = false;
            if(itemAction != null)
            {
                successEquippement=itemAction.PerformAction(gameObject, true);
            }

            if(successEquippement == true)
            {
                IDestroyableItem destroyableItem = inventoryItem.Item as IDestroyableItem;
                if(destroyableItem != null)
                {
                    _inventoryData.RemoveItem(index,1);
                }
            }
        }

    }


    private void HandleItemActionRequestedMiddle(int index)
    {
        SingleItem inventoryItem = _inventoryData.GetItemAt(index);

        if(inventoryItem.IsEmpty)
        {
            return;
        }

        IItemAction itemAction = inventoryItem.Item as IItemAction;
        if(itemAction.ActionName == "Consume")
        {
            IDestroyableItem destroyableItem = inventoryItem.Item as IDestroyableItem;

            bool success = false;
            if(itemAction != null)
            {
                success = itemAction.PerformAction(gameObject, true);
            } 
            if(success)
            {
                if(destroyableItem != null)
                {
                    _inventoryData.RemoveItem(index,1);
                }
            }
        }

    }

    private void DropItem(int index, int quantity)
    {
        _inventoryData.RemoveItem(index,quantity);
        _inventoryUI.ResetSelection();
    }

    public void PerformAction(int index, bool increase)
    {
        SingleItem inventoryItem = _inventoryData.GetItemAt(index);
        if(inventoryItem.IsEmpty){
            return;
        }

        IDestroyableItem destroyableItem = inventoryItem.Item as IDestroyableItem;
        if(destroyableItem != null)
        {
            _inventoryData.RemoveItem(index,1);
        }

        IItemAction itemAction = inventoryItem.Item as IItemAction;
        if(itemAction != null)
        {
            itemAction.PerformAction(gameObject, increase);
        }

    }

    private void HandleSelectRequested(int index){
        SingleItem inventoryItem = _inventoryData.GetItemAt(index);
        if(inventoryItem.IsEmpty){
            _inventoryUI.ResetSelection();
            return;
        }
        ItemSO item = inventoryItem.Item;
        _inventoryUI.UpdateSelection(index);
        
    }


    private void HandleDragging(int index){
        SingleItem inventoryItem = _inventoryData.GetItemAt(index);
        if(inventoryItem.IsEmpty){
            return;
        }
        _inventoryUI.CreateDraggedItem(inventoryItem.Item.ItemImage, inventoryItem.Quantity);
    }
    private void HandleSwapItems(int index1, int index2)
    {
        _inventoryData.SwapItems(index1,index2);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            if(_inventoryUI.isActiveAndEnabled == false)
            {
                _inventoryUI.Show();
                foreach(var item in _inventoryData.GetCurrentInventoryState())
                {
                    _inventoryUI.UpdateData(item.Key, item.Value.Item.ItemImage, item.Value.Quantity);
                }
            }
            else
            {
            _inventoryUI.Hide();
            }
        }
    }
}
}
