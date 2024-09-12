using CodeBase.Logic.Spawner;
using CodeBase.SO;
using UnityEngine;

namespace CodeBase.Logic
{
    public class Planet : MonoBehaviour
    {
        [SerializeField] private AsteroidSpawner _asteroidSpawner;

        private Asteroid[] _asteroids;
        private int _numberOfAsteroids;
        private float _minOrbitRadius;
        private float _maxOrbitRadius;
        private float _minOrbitSpeed;
        private float _maxOrbitSpeed;
        private float _minRotationSpeed;
        private float _maxRotationSpeed;
        private float _minAsteroidSize;
        private float _maxAsteroidSize;
        private string _name;

        public string Name => _name;


        private void Start()
        {
            Initialize();
        }

        public void SetStats(PlanetStaticData data)
        {
            _numberOfAsteroids = data.NumberOfAsteroids;
            _minOrbitRadius = data.MinOrbitRadius;
            _maxOrbitRadius = data.MaxOrbitRadius;
            _minOrbitSpeed = data.MinOrbitSpeed;
            _maxOrbitSpeed = data.MaxOrbitSpeed;
            _minRotationSpeed = data.MinRotationSpeed;
            _maxRotationSpeed = data.MaxRotationSpeed;
            _minAsteroidSize = data.MinAsteroidSize;
            _maxAsteroidSize = data.MaxAsteroidSize;
            _name = data.Name;
        }

        private async void Initialize()
        {
            _asteroids = await _asteroidSpawner.SpawnAsteroids(_numberOfAsteroids, _minOrbitRadius, _maxOrbitRadius);

            foreach (Asteroid asteroid in _asteroids)
            {
                ConfigureAsteroid(asteroid);
            }
        }

        private void ConfigureAsteroid(Asteroid asteroid)
        {
            float orbitSpeed = Random.Range(_minOrbitSpeed, _maxOrbitSpeed);
            float rotationSpeed = Random.Range(_minRotationSpeed, _maxRotationSpeed);
            float orbitRadius = Vector3.Distance(asteroid.transform.position, transform.position);
            float size = Random.Range(_minAsteroidSize, _maxAsteroidSize);

            asteroid.Initialize(transform.position, orbitRadius, orbitSpeed, rotationSpeed, size);
        }

        private void Update()
        {
            if (_asteroids == null)
                return;

            for (int i = 0; i < _asteroids.Length; i++)
            {
                _asteroids[i].UpdateMovement();
            }
        }
    }
}