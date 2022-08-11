using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Equippement;
using System;
using Inventory.Model;
using Equippement.UI;
using Equippement.Model;

public class EquippementSlots : MonoBehaviour
{
    public event Action<EquipType, EquippementItem> OnRemoveEquippementRequested;

    [SerializeField]
    private EquippementItem _headSlot,_bodySlot,_weaponSlot,_shieldSlot;

    [SerializeField]
    private EquippementSO _equippementData;

    public void InitializeEquippement()
    {
       Debug.Log("Equippement initialized!");
       _headSlot.OnRemoveEquippementRequested += HandleRemoveEquippementActions;
       _bodySlot.OnRemoveEquippementRequested += HandleRemoveEquippementActions;
       _weaponSlot.OnRemoveEquippementRequested += HandleRemoveEquippementActions;
       _shieldSlot.OnRemoveEquippementRequested += HandleRemoveEquippementActions;

    }

    public void ResetAllEquiped()
    {
        _headSlot.ResetEquippementData();
        _bodySlot.ResetEquippementData();
        _weaponSlot.ResetEquippementData();
        _shieldSlot.ResetEquippementData();
    }

    private void HandleRemoveEquippementActions(EquippementItem item)
    {  
        if(item == _headSlot)
        {
            OnRemoveEquippementRequested?.Invoke(EquipType.HEAD, item); 
        }
        else if(item == _bodySlot)
        {
            OnRemoveEquippementRequested?.Invoke(EquipType.BODY, item); 
        }
        else if(item == _weaponSlot)
        {
            OnRemoveEquippementRequested?.Invoke(EquipType.WEAPON, item); 
        }
        else if(item == _shieldSlot)
        {
            OnRemoveEquippementRequested?.Invoke(EquipType.SHIELD, item); 
        }
    }

    public void UpdateData(EquipType type, Sprite itemImage)
    {
        switch(type)
        {
            case EquipType.HEAD:
                _headSlot.SetEquippementData(itemImage);
                break;
            case EquipType.BODY:
                _bodySlot.SetEquippementData(itemImage);
                break; 
            case EquipType.WEAPON:
                _weaponSlot.SetEquippementData(itemImage);
                break;
            case EquipType.SHIELD:
                _shieldSlot.SetEquippementData(itemImage);
                break;
        } 
    }

    public void Update()
    {
        if(_equippementData.IsTypeEquipped(EquipType.HEAD))
        {
            _headSlot.SetEquippementData(_equippementData.GetEquippementAt(EquipType.HEAD).Item.ItemImage);
        }
        if(_equippementData.IsTypeEquipped(EquipType.WEAPON))
        {
             _weaponSlot.SetEquippementData(_equippementData.GetEquippementAt(EquipType.WEAPON).Item.ItemImage);
        }
        if(_equippementData.IsTypeEquipped(EquipType.BODY))
        {
             _bodySlot.SetEquippementData(_equippementData.GetEquippementAt(EquipType.BODY).Item.ItemImage);
        }
        if(_equippementData.IsTypeEquipped(EquipType.SHIELD))
        {
            _shieldSlot.SetEquippementData(_equippementData.GetEquippementAt(EquipType.SHIELD).Item.ItemImage);
        }
    }
      
    public void ResetAllEquippement()
    {
      _headSlot.ResetEquippementData();
      _bodySlot.ResetEquippementData();
      _weaponSlot.ResetEquippementData();
      _shieldSlot.ResetEquippementData();
    }

    private void Awake()
    {
        Hide();
    }

    public void Show(){
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
