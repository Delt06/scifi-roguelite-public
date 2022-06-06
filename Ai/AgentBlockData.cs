using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Ai
{
    [Serializable]
    public struct AgentBlockData
    {
        [Min(0f)]
        public float MaxDistance;

        [Min(0f)]
        public float Duration;

        [HideInEditorMode]
        public float RemainingTime;
    }
}