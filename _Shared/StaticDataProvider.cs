using JetBrains.Annotations;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace _Shared
{
    public static class StaticDataProvider
    {
        [CanBeNull]
        public static IStaticDataProvider FindOrDefault()
        {
            var ecsEntryPoint = Object.FindObjectOfType<GameEcsEntryPoint>();
            return ecsEntryPoint == null ? null : ecsEntryPoint.GetComponent<IStaticDataProvider>();
        }
    }
}