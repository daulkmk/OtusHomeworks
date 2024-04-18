namespace ShootEmUp
{
    public interface IDamagable
    {
        void ApplyDamage(int damage, bool isPlayer);
    }
}