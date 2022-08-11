using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stats.Model;


    public class StatController : MonoBehaviour
    {

        [SerializeField]
        private GameObject _statPanel;

        [SerializeField]
        private StatsSO _statData;

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
                    _statPanel.SetActive(true);
                }
                else
                {
                _statPanel.SetActive(false);
                }
            }
        }
    }

