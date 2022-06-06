using System;
using Sirenix.OdinInspector;

namespace Movement.Ragdoll
{
    [Serializable]
    public struct RagdollDataView
    {
        [Required] public RagdollBehavior Ragdoll;
    }
}