using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Leveling.Energy
{
    [Serializable]
    public struct EnergyDropData
    {
        [MinMaxSlider(0, 1000, true)]
        public Vector2Int ValueRange;
    }
}