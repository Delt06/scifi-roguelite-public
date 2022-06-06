using DELTation.LeoEcsExtensions.Views.Components;
using UnityEngine;

namespace Health.Damage
{
    public class MeleeDamageDataView : ComponentView<MeleeDamageData>
    {
        private void OnDrawGizmosSelected()
        {
            if (StoredComponentValue.Weapon == null) return;

            var color = Color.red;
            color.a = 0.1f;
            Gizmos.color = color;
            var (center, size, orientation) = StoredComponentValue.Weapon.GetHitBoxInfo();
            var oldMatrix = Gizmos.matrix;
            Gizmos.matrix = Matrix4x4.TRS(center, orientation, Vector3.one);
            Gizmos.DrawCube(Vector3.zero, size);
            Gizmos.matrix = oldMatrix;
        }
    }
}