using System;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Maps
{
    public class MapData : MonoBehaviour
    {
        [SerializeField] [TableList] private GateData[] _gates = Array.Empty<GateData>();

        public GateData[] Gates => _gates;

        [Button]
        private void Gather()
        {
            var oldGatesData = _gates
                .Where(g => g.Gate != null)
                .ToDictionary(g => g.Gate, g => g);
            _gates = FindObjectsOfType<Gate>()
                .Select(g =>
                    {
                        if (oldGatesData.TryGetValue(g, out var oldData))
                            return oldData;
                        return new GateData
                        {
                            Gate = g,
                            Guid = Guid.NewGuid().ToString(),
                        };
                    }
                ).ToArray();
        }


        [Serializable]
        public struct GateData
        {
            [Required]
            public Gate Gate;

            [Required]
            public string Guid;
        }
    }
}