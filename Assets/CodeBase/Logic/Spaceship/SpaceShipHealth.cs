using System;
using UnityEngine;

namespace CodeBase.Logic.Spaceship
{
    public class SpaceShipHealth : MonoBehaviour, IHealth
    {
        private float _current = 100;

        private float _max = 100;

        public event Action HealthChanged;

        public float Current
        {
            get => _current;
            set => _current = value;
        }

        public float Max
        {
            get => _max;
            set => _max = value;
        }

        public void TakeDamage(float damage)
        {
            Current -= damage;
            HealthChanged?.Invoke();
        }

        public float GetCurrent()
        {
            return _current;
        }

        public float GetMax()
        {
            return _max;
        }
    }
}