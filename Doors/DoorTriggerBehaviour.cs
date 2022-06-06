using DELTation.LeoEcsExtensions.Components;
using DELTation.LeoEcsExtensions.Views;
using Leopotam.EcsLite;
using Maps;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Doors
{
    public class DoorTriggerBehaviour : MonoBehaviour
    {
        [SerializeField] [Required] private Gate _gate;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out IEntityView entityView)) return;
            if (!entityView.TryGetEntity(out var entity)) return;
            if (!entity.Unpack(out var world, out _)) return;

            ref var enterEvent = ref world.NewPackedEntityWithWorld().Add<DoorTriggerEnterEvent>();
            enterEvent.Gate = _gate;
            enterEvent.EnteredEntity = entity;
        }
    }
}