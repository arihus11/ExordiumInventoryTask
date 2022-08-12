using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Inventory.Model;

public class CollisionDetection : MonoBehaviour
{
    [SerializeField] private float _range = 1f;
    public GameObject PickUpMessage;
    public static bool _pickUpEnabled = false;
    public static Item ItemInRange;
    // Start is called before the first frame update

    [SerializeField]
    private InventorySO _inventoryData;

    void Start()
    {
        _pickUpEnabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        #region PHYSICS OVERLAP CIRCLE
         Collider2D[] colliderArray = Physics2D.OverlapCircleAll(transform.position, _range);
        if(colliderArray.Length >= 2){
            foreach(Collider2D collider2D in colliderArray)
            {
                    try
                    {
                        if(collider2D.CompareTag("Item"))
                        {
                            ProcessCollisionEnter(collider2D.gameObject);
                            Array.Clear(colliderArray, 0, colliderArray.Length);
                        }
                    }
                    catch(System.NullReferenceException ex)
                    {
                        Debug.Log("Null reference ignored.");
                    }
            }
        }
        else
        {
            ProcessCollisionExit();
        }
        #endregion

        #region MOUSE BUTTON
        if (Input.GetMouseButtonDown(0)) 
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null && _pickUpEnabled && hit.collider.gameObject.CompareTag("Item")) 
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
        #endregion
    }

    void ProcessCollisionEnter(GameObject collider){
            _pickUpEnabled = true;
            ItemInRange = collider.GetComponent<Item>();
            PickUpMessage.SetActive(true);
    }

    void ProcessCollisionExit(){
            _pickUpEnabled = false;
            PickUpMessage.SetActive(false);
    }
}
