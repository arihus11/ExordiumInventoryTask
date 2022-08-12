using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Equippement.Model;
using Inventory.Model;
using Equippement.UI;

namespace Equippement
{
    public class EquipController : MonoBehaviour
    {
        [SerializeField]
        private EquippementSlots _equipementUI;

        [SerializeField]
        private EquippementSO _equippementData;

        [SerializeField]
         private GameObject InventoryButton,EquippementButton,StatsButton, InventoryPanel,StatsPanel;

        public List<SingleEquipementItem> InitialEquippement = new List<SingleEquipementItem>();

        private void Start()
        {
            PrepareEquippementUI();
            PrepareEquippementData();
        }

        private void PrepareEquippementUI()
        {
        _equipementUI.InitializeEquippement();
        this._equipementUI.OnRemoveEquippementRequested += HandleEquippementRemoveRequested;
        }

        private void HandleEquippementRemoveRequested(EquipType type, EquippementItem item)
        {
            _equippementData.UnEquipItem(type, item);

        }

        private void PrepareEquippementData()
        {
            _equippementData.Initialize();
            _equippementData.OnEquippementUpdated += UpdateEquippementUI;
            foreach(SingleEquipementItem item in InitialEquippement)
            {
                if(item.IsEmpty)
                {
                    continue;
                }
                switch(item.Type)
                {
                    case EquipType.HEAD:
                        _equippementData.EquipItem(item.Item, EquipType.HEAD);
                        break;
                    case EquipType.BODY: 
                        _equippementData.EquipItem(item.Item, EquipType.BODY);
                        break;
                    case EquipType.WEAPON: 
                        _equippementData.EquipItem(item.Item, EquipType.WEAPON);
                        break;
                    case EquipType.SHIELD: 
                        _equippementData.EquipItem(item.Item, EquipType.SHIELD);
                        break;
                    default:
                        break;
                }
            }
        }

        private void UpdateEquippementUI(Dictionary<EquipType, SingleEquipementItem> inventoryState)
        {
            _equipementUI.ResetAllEquippement();
            foreach(var item in inventoryState)
            {
                _equipementUI.UpdateData(item.Key, item.Value.Item.ItemImage);
            } 
        } 

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.X))
            {
                if(_equipementUI.isActiveAndEnabled == false)
                {
                    _equipementUI.Show();
                    InventoryButton.SetActive(true);
                    EquippementButton.SetActive(false);
                    StatsButton.SetActive(true);
                    InventoryPanel.SetActive(false);
                    StatsPanel.SetActive(false);
                    foreach(var item in _equippementData.GetCurrentEquippementState())
                    {
                        _equipementUI.UpdateData(item.Key, item.Value.Item.ItemImage);
                    } 
                }
                else
                {
                _equipementUI.Hide();
                InventoryButton.SetActive(true);
                EquippementButton.SetActive(true);
                StatsButton.SetActive(true);
                }
            }
        }
    }
    
}
