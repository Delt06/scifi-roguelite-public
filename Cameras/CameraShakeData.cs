using System;
using Cinemachine;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Cameras
{
    [Serializable]
    public struct CameraShakeData
    {
        [Required]
        public CinemachineVirtualCamera VirtualCamera;

        [Min(0f)]
        public float Duration;

        [MinMaxSlider(0f, 10f, true)]
        public Vector2 BaseAmplitudeGainRange;
        [FormerlySerializedAs("AmplitudeGain")]
        public AnimationCurve AmplitudeGainOverTime;

        [HideInEditorMode]
        public CinemachineBasicMultiChannelPerlin Noise;
    }
}