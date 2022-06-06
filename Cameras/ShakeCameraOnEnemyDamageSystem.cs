using DELTation.LeoEcsExtensions.Utilities;
using Health.Damage;
using Health.Teams;
using Leopotam.EcsLite;
using UnityEngine;

namespace Cameras
{
    public class ShakeCameraOnEnemyDamageSystem : IEcsRunSystem
    {
        private readonly EcsFilter _commandsFilter;
        private readonly EcsFilter _shakeFilter;

        public ShakeCameraOnEnemyDamageSystem(EcsWorld world)
        {
            _commandsFilter = world.Filter<DealDamageCommand>()
                .End();
            _shakeFilter = world.Filter<CameraShakeData>()
                .Inc<CameraShakeOnHitTag>()
                .End();
        }

        public void Run(EcsSystems systems)
        {
            foreach (var iCommand in _commandsFilter)
            {
                var dealDamageCommand = _commandsFilter.Read<DealDamageCommand>(iCommand);
                if (dealDamageCommand.AttackerTeam != Team.Player) continue;

                foreach (var iShake in _shakeFilter)
                {
                    var baseAmplitudeGainRange = _shakeFilter.Read<CameraShakeData>(iShake).BaseAmplitudeGainRange;
                    ref var activeCameraShakeData = ref _shakeFilter.GetOrAdd<ActiveCameraShakeData>(iShake);
                    activeCameraShakeData.BaseAmplitudeGain =
                        Random.Range(baseAmplitudeGainRange.x, baseAmplitudeGainRange.y);
                    activeCameraShakeData.ElapsedTime = 0f;
                }

                break;
            }
        }
    }
}