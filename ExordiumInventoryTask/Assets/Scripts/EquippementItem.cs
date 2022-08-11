using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;
using Inventory.Model;

namespace Equippement.UI
{
    public class EquippementItem : MonoBehaviour
    {
        [SerializeField]
        private Image _itemImage;
        [SerializeField]
        private Image _borderImage;
        [SerializeField]
        private GameObject _removeButton;

        private bool _empty = true;

        public event Action<EquippementItem> OnRemoveEquippementRequested;


        public void Awake()
        {
            ResetEquippementData();
        }

        public void ResetEquippementData()
        {
        this._itemImage.gameObject.SetActive(false);
        _removeButton.SetActive(false);
        _borderImage.enabled = false;
        _empty = true;
        }


        public void SetEquippementData(Sprite sprite)
        {
            this._itemImage.gameObject.SetActive(true);
            this._itemImage.sprite = sprite;
            _removeButton.SetActive(true);
            _empty = false;
            _borderImage.enabled = true;
        }

        public void RemoveEquippementButtonPress()
        {
            OnRemoveEquippementRequested?.Invoke(this);
        }
    }
}

