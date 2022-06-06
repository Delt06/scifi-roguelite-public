using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Health
{
    [Serializable]
    public struct HealthBarData
    {
        [Required]
        public TMP_Text Text;
        [Required]
        public GameObject Root;
    }
}