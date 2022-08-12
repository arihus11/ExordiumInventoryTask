using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Inventory.Model;

namespace Inventory.UI
{
    public class InventorySlots : MonoBehaviour
{
    [SerializeField]
    private InventoryItem _itemPrefab;

    [SerializeField]
    private MouseFollower _mouseFollower;

    [SerializeField]
    private RectTransform _contentPanel;

    List<InventoryItem> _listOfItems = new List<InventoryItem>();

    public static bool UpdateInventorySlots = false;

    private int _currentlyDraggedItemIndex = -1;

    public event Action<int> OnItemActionRequestedMiddle, OnItemActionRequestedRight, OnStartDragging, OnSelectRequested, OnRemoveButtonPressed;
    public event Action <int,int> OnSwapItems;


    private void Awake(){
        Hide();
     //   _mouseFollower.Toggle(false);
    }

    public void InitializeInventory(int inventorySize)
    {
        for(int i= 0; i<inventorySize; i++)
        {
            AddNewItemSlot();
        }
    }

    void Update(){
        if(InventorySO.AddNewEmptySlot)
        {
            AddNewItemSlot();
            InventorySO.AddNewEmptySlot = false;
        }
    }

    public void AddNewItemSlot()
    {
         InventoryItem item = Instantiate(_itemPrefab, Vector3.zero,Quaternion.identity);
            item.transform.SetParent(_contentPanel);
            _listOfItems.Add(item);
            item.OnItemClicked += HandleItemSelection;
            item.OnItemBeginDrag += HandleBeginDrag;
            item.OnItemDroppedOn += HandleSwap;
            item.OnItemEndDrag += HandleEndDrag;
            item.OnMiddleMouseButtonClicked += HandleShowItemActionsMiddle;
            item.OnRightMouseButtonClicked += HandleShowItemActionsRight;
            item.OnRemoveButtonPressed += HandleRemoveButtonActions;
    }    

    public void UpdateSelection(int index)
    {
        DeselectAllItems();
        _listOfItems[index].Select();
    }

    public void ResetAllItems()
    {
        foreach(var item in _listOfItems){
            item.ResetData();
            item.Deselect();
        }
    }

    private void HandleRemoveButtonActions(InventoryItem obj){
        int index = _listOfItems.IndexOf(obj);
         if(index == -1)
          {
             return;
          }
        OnRemoveButtonPressed?.Invoke(index);
    }

    private void HandleItemSelection(InventoryItem obj)
    {
        int index = _listOfItems.IndexOf(obj);
        if(index == -1)
        {
            return;
        }
         OnSelectRequested?.Invoke(index);
    }

    private void HandleBeginDrag(InventoryItem obj){
        int index = _listOfItems.IndexOf(obj);
        if(index == -1){
            return;
        }
        _currentlyDraggedItemIndex = index;
        HandleItemSelection(obj);
        OnStartDragging?.Invoke(index);
       
    }

    public void CreateDraggedItem(Sprite sprite, int quantity)
    {
        _mouseFollower.SetData(sprite,quantity);
    }
    private void HandleSwap(InventoryItem obj)
    {
        int index = _listOfItems.IndexOf(obj);
        if(index == -1)
        {
            return;
        }
        OnSwapItems?.Invoke(_currentlyDraggedItemIndex, index);
        HandleItemSelection(obj);
    }
    private void HandleEndDrag(InventoryItem obj){
      _mouseFollower.ResetData();
      _currentlyDraggedItemIndex = -1;
    }

    public void UpdateData(int itemIndex, Sprite itemImage, int itemQuantity){
        if(_listOfItems.Count > itemIndex)
        {
            _listOfItems[itemIndex].SetData(itemImage,itemQuantity);
        }
    }

    private void HandleShowItemActionsMiddle(InventoryItem obj){
         int index = _listOfItems.IndexOf(obj);
        if(index == -1)
        {
            return;
        }
        OnItemActionRequestedMiddle?.Invoke(index);
    }

    private void HandleShowItemActionsRight(InventoryItem obj){
         int index = _listOfItems.IndexOf(obj);
        if(index == -1)
        {
            return;
        }
        OnItemActionRequestedRight?.Invoke(index);
    }

    public void Show(){
        gameObject.SetActive(true);
        ResetSelection();
    }

    public void ResetSelection(){
        DeselectAllItems();
    }

    private void DeselectAllItems(){
        foreach(InventoryItem item in _listOfItems)
        {
            item.Deselect();
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        _mouseFollower.ResetData();
        _currentlyDraggedItemIndex = -1;
    }
}
}

