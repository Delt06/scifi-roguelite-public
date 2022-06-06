using Camps;
using DELTation.LeoEcsExtensions.Utilities;
using Leopotam.EcsLite;
using UnityEngine;
using UnityEngine.AI;

namespace Ai.Camp
{
    public class AgentReturnToCampWhenDestinationIsTooFarSystem : IEcsRunSystem
    {
        private readonly EnemyStaticData _enemyStaticData;
        private readonly EcsFilter _filter;

        public AgentReturnToCampWhenDestinationIsTooFarSystem(EcsWorld world, EnemyStaticData enemyStaticData)
        {
            _enemyStaticData = enemyStaticData;
            _filter = world.Filter<CampRef>()
                .Inc<NavMeshAgentData>()
                .Inc<FollowData>()
                .Exc<ReturnToCampCommand>()
                .End();
        }

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                var agent = _filter.Read<NavMeshAgentData>(i).Agent;
                if (!agent.hasPath) continue;

                var target = _filter.Read<FollowData>(i).Target;
                if (target == null || !target.Value.Unpack(out _, out _)) continue;

                var pathLength = agent.path.GetLength();
                if (pathLength >= _enemyStaticData.CampReturnDistance)
                    _filter.Add<ReturnToCampCommand>(i);
            }
        }
    }

    public static class AiExtensions
    {
        private static readonly Vector3[] CornersBuffer = new Vector3[100];

        public static float GetLength(this NavMeshPath path)
        {
            var cornersCount = path.GetCornersNonAlloc(CornersBuffer);

            var length = 0f;

            for (var i = 0; i < cornersCount - 1; i++)
            {
                var corner1 = CornersBuffer[i];
                var corner2 = CornersBuffer[i + 1];
                length += Vector3.Distance(corner1, corner2);
            }

            return length;
        }
    }
}