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
    private InventorySO _inventorySO;

    [SerializeField]
    private MouseFollower _mouseFollower;

    [SerializeField]
    private GameObject _slotsParent;

    [SerializeField]
    private RectTransform _contentPanel;

    public static bool ItemDraggedOut = false;

    private bool _mouseOverDroppableArea = false;

    
    List<InventoryItem> _listOfItems = new List<InventoryItem>();

    public static bool UpdateInventorySlots = false;

    private int _currentlyDraggedItemIndex = -1;
    public static int _currentlySelectedItem = -1;

    public event Action<int> OnItemActionRequestedMiddle, OnItemActionRequestedRight, OnStartDragging, OnSelectRequested, OnRemoveButtonPressed;
    public event Action <int,int> OnSwapItems;


    private void Awake(){
        Hide();
        _mouseOverDroppableArea = false;
        ItemDraggedOut = false;
     //   _mouseFollower.Toggle(false);
    }

    public void InitializeInventory(int inventorySize)
    {
        for(int i= 0; i<inventorySize; i++)
        {
            AddNewItemSlot();
        }
    }

    void Update()
    {
        if(InventorySO.AddNewEmptyRow)
        {
            for(int i= 0; i<7; i++)
            {
                AddNewItemSlot();
            }
            foreach(var item in _inventorySO.GetCurrentInventoryState())
            {
                 UpdateData(item.Key, item.Value.Item.ItemImage, item.Value.Quantity);
            }
            InventorySO.AddNewEmptyRow = false;
        }
    }

    public void AddNewItemSlot()
    {
            InventoryItem item = Instantiate(_itemPrefab);
            item.transform.SetParent(_contentPanel);
            item.transform.localScale = _itemPrefab.transform.localScale;
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
        _currentlySelectedItem = index;
        _listOfItems[index].Select();
    }

    public int GetCurrentSelectedItem()
    {
        return _currentlySelectedItem;
    }

    public void ResetAllItems()
    {
        foreach(var item in _listOfItems){
            item.ResetData();
            item.Deselect();
        }
    }

    private void HandleRemoveButtonActions(InventoryItem obj)
    {
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

    public void MouseOverDroppingPoint()
    {
        _mouseOverDroppableArea = true;
    }

    public void MouseOutOfDroppingPoint()
    {
        _mouseOverDroppableArea = false;
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

    private void HandleEndDrag(InventoryItem obj)
    {
        if(_mouseOverDroppableArea)
            {
                Debug.Log("Item dropped to the ground");
                ItemDraggedOut = true;
                _mouseFollower.ResetData();
                _currentlyDraggedItemIndex = -1;
            }
        else
        {
        _mouseFollower.ResetData();
        _currentlyDraggedItemIndex = -1;
        }
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

    public void ResetMouseFollowerData()
    {
        _mouseFollower.ResetData();
        _currentlyDraggedItemIndex = -1;
    }

    public void ResetSelection(){
        DeselectAllItems();
    }

    private void DeselectAllItems(){
        foreach(InventoryItem item in _listOfItems)
        {
            item.Deselect();
            _currentlySelectedItem = -1;
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

