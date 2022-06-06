using DELTation.LeoEcsExtensions.Components;
using DELTation.LeoEcsExtensions.Utilities;
using Leopotam.EcsLite;

namespace _Shared.Utils
{
    public static class ReplaceComponentExtensions
    {
        public static ref TNew Replace<TOld, TNew>(this EcsPackedEntityWithWorld entity)
            where TOld : struct where TNew : struct
        {
            entity.Del<TOld>();
            return ref entity.Add<TNew>();
        }

        public static ref TNew Replace<TOld, TNew>(this EcsFilter filter, int entity)
            where TOld : struct where TNew : struct
        {
            filter.Del<TOld>(entity);
            return ref filter.Add<TNew>(entity);
        }
    }
}