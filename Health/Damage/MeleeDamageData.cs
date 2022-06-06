using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Health.Damage
{
    [Serializable]
    public struct MeleeDamageData
    {
        [Required]
        public MeleeWeaponBehavior Weapon;
        [Min(0f)]
        public float Impulse;
    }
}