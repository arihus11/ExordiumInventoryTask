﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory.Model;

public class Item : MonoBehaviour
{
    [field: SerializeField]
    public ItemSO SingleItem {get; private set;}

    [SerializeField]
    private GameObject _itemHighlight;

    [field: SerializeField]
    public int Quantity {get;set;} = 1;

    private void Start(){
        GetComponent<SpriteRenderer>().sprite = SingleItem.ItemImage;
        _itemHighlight.gameObject.GetComponent<SpriteRenderer>().sprite = SingleItem.ItemImage;
    }

    public void DestroyItem(){
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject);
    }
}
