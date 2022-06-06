using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Vfx
{
    public interface IVfxFactory
    {
        void Create([NotNull] Func<VfxStaticData, ParticleSystem> prefabSelector, Vector3 position);
    }
}