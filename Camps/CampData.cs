using System;
using Sirenix.OdinInspector;

namespace Camps
{
    [Serializable]
    public struct CampData
    {
        [Required] [ReadOnly] public CampSpawnPoint[] SpawnPoints;
    }
}