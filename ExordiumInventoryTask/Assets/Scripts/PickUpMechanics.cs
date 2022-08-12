using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory.Model;

public class PickUpMechanics : MonoBehaviour
{
    [SerializeField]
    private InventorySO _inventoryData;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(CollisionDetection._pickUpEnabled)
            {
                Item item = CollisionDetection.ItemInRange;
                int reminder = _inventoryData.AddItem(item.SingleItem, item.Quantity);
                if(reminder == 0)
                {
                    item.DestroyItem();
                }
                else
                {
                    item.Quantity = reminder;
                }
            }
        }
    }
}
