using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stats.Model;
using Inventory.UI;


    public class StatController : MonoBehaviour
    {

        [SerializeField]
        private GameObject _statPanel;

        [SerializeField]
        private StatsSO _statData;

        [SerializeField]
        private InventorySlots _inventorySlots;

        [SerializeField]
         private GameObject InventoryButton,EquippementButton,StatsButton, InventoryPanel, EquippementPanel;

        void Awake() 
        {
            _statPanel.SetActive(false);
            _statData.InitializeStats();
            Debug.Log("Stats initialized");
        }
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.Y))
            {
                if(!_statPanel.gameObject.activeInHierarchy)
                {
                    _inventorySlots.ResetMouseFollowerData();
                    _statPanel.SetActive(true);
                    InventoryButton.SetActive(true);
                    EquippementButton.SetActive(true);
                    StatsButton.SetActive(false);
                    InventoryPanel.SetActive(false);
                    EquippementPanel.SetActive(false);
                }
                else
                {
                _statPanel.SetActive(false);
                InventoryButton.SetActive(true);
                EquippementButton.SetActive(true);
                StatsButton.SetActive(true);
                }
            }
        }
    }

