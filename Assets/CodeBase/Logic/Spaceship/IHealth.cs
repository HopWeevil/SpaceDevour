using System;

namespace CodeBase.Logic.Spaceship
{
    public interface IHealth
    {
        event Action HealthChanged;

        void TakeDamage(float damage);

        float GetCurrent();
        float GetMax();
    }
}