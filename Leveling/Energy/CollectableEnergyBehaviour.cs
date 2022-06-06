using DELTation.LeoEcsExtensions.Components;
using DELTation.LeoEcsExtensions.Views;
using Leopotam.EcsLite;
using UnityEngine;

namespace Leveling.Energy
{
    public class CollectableEnergyBehaviour : MonoBehaviour
    {
        [SerializeField] [Min(1)] private int _energy = 10;

        public int Energy
        {
            get => _energy;
            set => _energy = value;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out IEntityView entityView)) return;
            if (!entityView.TryGetEntity(out var entity)) return;
            if (!entity.Unpack(out var world, out _)) return;

            ref var command = ref world.NewPackedEntityWithWorld().Add<EnergyCollectionCommand>();
            command.Collector = entity;
            command.Energy = _energy;
            command.Collectable = gameObject;
        }
    }
}