using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

namespace Ai
{
    [Serializable]
    public struct NavMeshAgentData
    {
        [Required]
        public NavMeshAgent Agent;
        [Min(0f)]
        public float StoppingThreshold;
    }
}