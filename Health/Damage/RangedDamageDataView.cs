using DELTation.LeoEcsExtensions.Views.Components;
using UnityEngine;

namespace Health.Damage
{
    public class RangedDamageDataView : ComponentView<RangedDamageData>
    {
        private void OnDrawGizmosSelected()
        {
            var shootFrom = StoredComponentValue.ShootFrom;
            if (shootFrom == null) return;

            Gizmos.color = Color.red;
            var origin = shootFrom.position;
            Gizmos.DrawRay(origin,
                shootFrom.forward * StoredComponentValue.MaxDistance
            );
            Gizmos.DrawWireSphere(origin, StoredComponentValue.Radius);
        }
    }
}