using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

namespace Inventory.UI{
    public class InventoryItem : MonoBehaviour, IPointerClickHandler, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
{
    [SerializeField]
    private Image _itemImage;
    [SerializeField]
    private Text _quantity;

    [SerializeField]
    private Image _borderImage;

    [SerializeField]
    private GameObject _removeButton;

    public event Action<InventoryItem> OnItemClicked, OnItemDroppedOn, OnItemBeginDrag, OnItemEndDrag, OnMiddleMouseButtonClicked, OnRightMouseButtonClicked, OnRemoveButtonPressed;

    private bool _empty = true;

    public void Awake()
    {
        ResetData();
        Deselect();
    }
    public void ResetData()
    {
        this._itemImage.gameObject.SetActive(false);
        _removeButton.SetActive(false);
        _empty = true;
    }

    public void Deselect()
    {
        _borderImage.enabled = false;
    }

    public void SetData(Sprite sprite, int quantity)
    {
        this._itemImage.gameObject.SetActive(true);
        this._itemImage.sprite = sprite;
        this._quantity.text = quantity + "";
        _removeButton.SetActive(true);
        _empty = false;

    }


    public void Select(){
        _borderImage.enabled = true;
    }

    public void RemoveButtonPress(){
        OnRemoveButtonPressed?.Invoke(this);
    }

    public void OnPointerClick(PointerEventData pointerData)
    {
        if(pointerData.button == PointerEventData.InputButton.Middle)
        {
            OnMiddleMouseButtonClicked?.Invoke(this);
        }
        else if(pointerData.button == PointerEventData.InputButton.Right)
        {
            OnRightMouseButtonClicked?.Invoke(this);
        }
        else
        {
            OnItemClicked?.Invoke(this);
        }
    }

    public void OnEndDrag(PointerEventData eventData){
        OnItemEndDrag?.Invoke(this);
    }

    public void OnBeginDrag(PointerEventData eventData){
        if(_empty)
        {
            return;
        }
        OnItemBeginDrag?.Invoke(this);
    }

    public void OnDrop(PointerEventData eventData){
        OnItemDroppedOn?.Invoke(this);
    }

    public void OnDrag(PointerEventData eventData){
        //Leave empty
    }
}
}

