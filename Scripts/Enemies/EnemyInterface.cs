namespace EnemySide
{
    public interface EnemyInterface
    {
        void OnSpwan();

        void OnMove();

        void OnTakenDamage(int damageAmount);

        void OnShotDown();
    }

}

