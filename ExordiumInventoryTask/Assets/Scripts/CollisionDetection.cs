using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Inventory.Model;
using Equippement.Model;

public class CollisionDetection : MonoBehaviour
{
    [SerializeField] private float _range = 1f;
    public GameObject PickUpMessage;
    public static bool _pickUpEnabled = false;
    public static Item ItemInRange;
    public static String ObjectInRange;
    // Start is called before the first frame update

    [SerializeField]
    private InventorySO _inventoryData;

    [SerializeField]
    private EquippementSO _equippementData;
    [SerializeField]
    private PickUpMechanics _pickupController;

    void Start()
    {
        ObjectInRange = "";
        _pickUpEnabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        #region PHYSICS OVERLAP CIRCLE
         Collider2D[] colliderArray = Physics2D.OverlapCircleAll(transform.position, _range);
         if(colliderArray.Length >= 2)
         {
            foreach(Collider2D col in colliderArray)
            {
                        if(col.CompareTag("InventoryItem"))
                        {
                            ObjectInRange = col.gameObject.name;
                            ProcessCollisionEnter(col.gameObject);
                        }
                    
            }
        }
        else
        {
            if(ObjectInRange != "")
                GameObject.Find(ObjectInRange).gameObject.transform.GetChild(0).gameObject.SetActive(false);
            Array.Clear(colliderArray, 0, colliderArray.Length);
            ProcessCollisionExit();
        }
        #endregion

        #region MOUSE BUTTON
        if (Input.GetMouseButtonDown(0)) 
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null && _pickUpEnabled && hit.collider.gameObject.CompareTag("InventoryItem")) 
            {
                Item item = ItemInRange;
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
                    if(_pickupController.CheckIsCanBeEquipped(item.SingleItem))
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
                        int reminder = _inventoryData.AddItem(ItemInRange.SingleItem, ItemInRange.Quantity);
                        if(reminder == 0)
                        {
                            ItemInRange.DestroyItem();
                        }
                        else{
                            ItemInRange.Quantity = reminder;
                        }
                    }
                }
            }
        }
        #endregion
    }

    void ProcessCollisionEnter(GameObject col){
            _pickUpEnabled = true;
            ItemInRange = col.GetComponent<Item>();
            col.transform.GetChild(0).gameObject.SetActive(true);
            PickUpMessage.SetActive(true);
    }

    void ProcessCollisionExit()
    {
            _pickUpEnabled = false;
            PickUpMessage.SetActive(false);
            ObjectInRange = "";
    }
}
