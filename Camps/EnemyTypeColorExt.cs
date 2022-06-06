using System;
using UnityEngine;

namespace Camps
{
    public static class EnemyTypeColorExt
    {
        private static readonly Color Orange = Color.Lerp(Color.red, Color.yellow, 0.5f);

        public static Color GetColor(this EnemyType enemyType) =>
            enemyType switch
            {
                EnemyType.Melee => Orange,
                EnemyType.Ranged => Color.yellow,
                EnemyType.MeleeShield => Color.red,
                _ => throw new ArgumentOutOfRangeException(nameof(enemyType), enemyType, null),
            };
    }
}