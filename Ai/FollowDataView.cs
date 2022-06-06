using DELTation.LeoEcsExtensions.Views.Components;
using UnityEngine;

namespace Ai
{
    public class FollowDataView : ComponentView<FollowData>
    {
        private void OnDrawGizmosSelected()
        {
            if (StoredComponentValue.VisibilityCheckRayStart == null) return;
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(
                StoredComponentValue.VisibilityCheckRayStart.position,
                StoredComponentValue.VisibilityCheckRayRadius
            );
        }
    }
}