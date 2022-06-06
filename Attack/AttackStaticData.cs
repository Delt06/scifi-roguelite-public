using UnityEngine;

namespace Attack
{
    [CreateAssetMenu]
    public class AttackStaticData : ScriptableObject
    {
        [SerializeField] [Min(0)] private int _attackLayerIndex = 1;
        [SerializeField] private AnimationCurve _attackLayerWeightOverProgress;

        public int AttackLayerIndex => _attackLayerIndex;

        public AnimationCurve AttackLayerWeightOverProgress => _attackLayerWeightOverProgress;
    }
}