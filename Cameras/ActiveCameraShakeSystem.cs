using DELTation.LeoEcsExtensions.Utilities;
using Leopotam.EcsLite;
using UnityEngine;

namespace Cameras
{
    public class ActiveCameraShakeSystem : IEcsRunSystem
    {
        private readonly EcsFilter _filter;

        public ActiveCameraShakeSystem(EcsWorld world) =>
            _filter = world.Filter<CameraShakeData>()
                .Inc<ActiveCameraShakeData>()
                .End();

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                var cameraShakeData = _filter.Read<CameraShakeData>(i);
                ref var activeCameraShakeData = ref _filter.Get<ActiveCameraShakeData>(i);

                activeCameraShakeData.ElapsedTime += Time.deltaTime;
                float amplitudeGain;
                var progress = activeCameraShakeData.ElapsedTime / cameraShakeData.Duration;
                if (progress >= 1f)
                {
                    amplitudeGain = 0f;
                    _filter.Del<ActiveCameraShakeData>(i);
                }
                else
                {
                    amplitudeGain = cameraShakeData.AmplitudeGainOverTime.Evaluate(progress) *
                                    activeCameraShakeData.BaseAmplitudeGain;
                }

                cameraShakeData.Noise.m_AmplitudeGain = amplitudeGain;
            }
        }
    }
}