using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Vfx
{
    public class VfxFactory : IVfxFactory
    {
        private readonly VfxStaticData _vfxStaticData;

        public VfxFactory(VfxStaticData vfxStaticData) => _vfxStaticData = vfxStaticData;

        public void Create(Func<VfxStaticData, ParticleSystem> prefabSelector, Vector3 position)
        {
            if (prefabSelector == null) throw new ArgumentNullException(nameof(prefabSelector));
            var prefab = prefabSelector(_vfxStaticData);
            position += prefab.transform.localPosition;
            var vfx = Object.Instantiate(prefab, position, Quaternion.identity);
            var main = vfx.main;
            main.stopAction = ParticleSystemStopAction.Destroy;
        }
    }
}