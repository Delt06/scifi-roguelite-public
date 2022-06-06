using _Shared.Utils;
using DELTation.LeoEcsExtensions.Views.Components;
using UnityEngine;

namespace Movement
{
    public class LookAtClosestEnemyDataView : ComponentView<LookAtClosestEnemyData>
    {
        private void OnDrawGizmos()
        {
            Gizmos.color = ColorExt.Orange;
            Gizmos.DrawWireSphere(transform.TransformPoint(StoredComponentValue.CheckCenterOffset),
                StoredComponentValue.CheckRadius
            );
        }
    }
}