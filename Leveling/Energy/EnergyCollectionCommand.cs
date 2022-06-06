using Leopotam.EcsLite;
using UnityEngine;

namespace Leveling.Energy
{
    public struct EnergyCollectionCommand
    {
        public EcsPackedEntityWithWorld Collector;
        public int Energy;
        public GameObject Collectable;
    }
}