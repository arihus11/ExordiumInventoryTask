using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace Inventory.UI
{
    public class ItemButtonPanel : MonoBehaviour
    {
        [SerializeField]
        private GameObject button;

        public void AddButton(Action onClickAction)
        {
            button.GetComponent<Button>().onClick.AddListener(() => onClickAction());
        }

        internal void Toggle(bool val)
        {
            if(val == true)
            {
                RemoveAllButtons();
            }
            gameObject.SetActive(val);
        }

        public void RemoveAllButtons()
        {
            foreach (Transform transformChildObjects in transform)
            {
                Destroy(transformChildObjects.gameObject);
            }
        }
    }
}

