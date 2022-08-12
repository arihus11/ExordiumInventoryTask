using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory.Model;

public class Item : MonoBehaviour
{
    [field: SerializeField]
    public ItemSO SingleItem {get; private set;}

    private GameObject _itemHighlight;

    [field: SerializeField]
    public int Quantity {get;set;} = 1;

    private void Start(){
        _itemHighlight = this.gameObject.transform.GetChild(0).gameObject;
        GetComponent<SpriteRenderer>().sprite = SingleItem.ItemImage;
        _itemHighlight.GetComponent<SpriteRenderer>().sprite = SingleItem.ItemImage;
    }

    public void DestroyItem(){
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject);
    }

    public void SetSingleItem(ItemSO item)
    {
        this.SingleItem = item;
    }

    public void SetQuantitiy(int quant)
    {
        this.Quantity = quant;
    }
}
