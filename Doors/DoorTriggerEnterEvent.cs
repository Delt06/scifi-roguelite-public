using Leopotam.EcsLite;
using Maps;

namespace Doors
{
    public struct DoorTriggerEnterEvent
    {
        public Gate Gate;
        public EcsPackedEntityWithWorld EnteredEntity;
    }
}