using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory.UI;

public class MouseFollower : MonoBehaviour
{
    [SerializeField]
    private Canvas _canvas;

    [SerializeField]
    private InventoryItem _item;

    public void Awake(){
        _canvas = transform.root.GetComponent<Canvas>();
        _item = GetComponentInChildren<InventoryItem>();
    }

    public void SetData(Sprite sprite, int quantity){
        _item.SetData(sprite,quantity);
    }

    public void ResetData()
    {
        if(_item != null){
            _item.ResetData();
        }
    }

    void Update(){
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)_canvas.transform,
            Input.mousePosition,
            _canvas.worldCamera,
        out position);
        transform.position = _canvas.transform.TransformPoint(position);
    }

    public void Toggle(bool val){
        gameObject.SetActive(val);
    }
}
