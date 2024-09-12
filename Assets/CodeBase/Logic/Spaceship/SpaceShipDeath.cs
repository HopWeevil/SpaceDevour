using UnityEngine;


namespace CodeBase.Logic.Spaceship
{
    [RequireComponent(typeof(IHealth))]
    public class SpaceShipDeath : MonoBehaviour
    {
        [SerializeField] private GameObject _deathFx;

        private IHealth _health;

        private void Awake()
        {
            _health = GetComponent<IHealth>();
        }

        private void OnEnable()
        {
            _health.HealthChanged += HealthChanged;
        }

        private void OnDisable()
        {
            _health.HealthChanged -= HealthChanged;
        }

        private void HealthChanged()
        {
            if (_health.GetCurrent() <= 0)
            {
                Die();
            }
        }

        private void Die()
        {         
            Instantiate(_deathFx, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}