namespace Leveling
{
    public static class LevelingDataExt
    {
        public static float ComputeForLevelIndex(this in DamageLevelingData damageLevelingData, int levelIndex) =>
            damageLevelingData.BaseDamage + damageLevelingData.DamagePerLevel * levelIndex;

        public static float ComputeForLevelIndex(this in HealthLevelingData healthLevelingData, int levelIndex) =>
            healthLevelingData.BaseHealth + healthLevelingData.HealthPerLevel * levelIndex;
    }
}