using Cinemachine;
using DELTation.LeoEcsExtensions.Components;
using DELTation.LeoEcsExtensions.Views.Components;
using Leopotam.EcsLite;

namespace Cameras
{
    public class CameraShakeDataView : ComponentView<CameraShakeData>
    {
        protected override void PostInitializeEntity(EcsPackedEntityWithWorld entity)
        {
            base.PostInitializeEntity(entity);
            ref var cameraShakeData = ref entity.Get<CameraShakeData>();
            cameraShakeData.Noise =
                cameraShakeData.VirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }
    }
}