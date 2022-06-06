namespace Attack
{
    public static class AttackStateExtensions
    {
        public static float GetNormalizedTime(this in AttackState activeAttackData) =>
            activeAttackData.ElapsedTime / activeAttackData.Duration;
    }
}