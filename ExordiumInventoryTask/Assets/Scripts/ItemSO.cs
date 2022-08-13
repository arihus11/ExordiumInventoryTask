﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Inventory.Model
{
    public abstract class ItemSO : ScriptableObject
    {
        [field: SerializeField]
        public bool PermanentUsage { get; set; }

        [field: SerializeField]
        public bool IsStackable { get; set; }

        public int ID => GetInstanceID();

        [field: SerializeField]
        public int MaxStackSize { get; set; } = 1;
        
        [field: SerializeField]
        public string Name { get; set; }

        [field: SerializeField]
        public EquipType EquipType { get; set; }

        [field: SerializeField]
        public Sprite ItemImage { get; set; }

        
    }

    [Serializable]
    public class ModifierData
    {
        public CharacterStatModifierSO statModifier;
        public int value;
    }
}

