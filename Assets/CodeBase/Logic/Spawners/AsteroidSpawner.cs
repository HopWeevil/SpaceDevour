using CodeBase.Infrastructure.AssetManagement;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.Spawner
{
    public class AsteroidSpawner : MonoBehaviour
    {
        private const string AsteroidLabel = "Asteroid";
        private IAssetProvider _assetProvider;

        [Inject]
        private void Construct(IAssetProvider provider)
        {
            _assetProvider = provider;
        }

        public async Task<Asteroid[]> SpawnAsteroids(int numberOfAsteroids, float minOrbitRadius, float maxOrbitRadius)
        {
            List<GameObject> asteroidsAsstes = await _assetProvider.LoadAll<GameObject>(AsteroidLabel);

            Asteroid[] asteroids = new Asteroid[numberOfAsteroids];

            for (int i = 0; i < numberOfAsteroids; i++)
            {
                asteroids[i] = SpawnSingleAsteroid(asteroidsAsstes[Random.Range(0, asteroidsAsstes.Count)], minOrbitRadius, maxOrbitRadius);
            }

            return asteroids;
        }

        private Asteroid SpawnSingleAsteroid(GameObject asteroidAsset, float minOrbitRadius, float maxOrbitRadius)
        {
            Asteroid asteroid = Instantiate(asteroidAsset, transform.position, Quaternion.identity).GetComponent<Asteroid>();
            asteroid.transform.position = CalculateRandomOrbitPosition(minOrbitRadius, maxOrbitRadius);
            return asteroid;
        }

        private Vector3 CalculateRandomOrbitPosition(float minOrbitRadius, float maxOrbitRadius)
        {
            float orbitRadius = Random.Range(minOrbitRadius, maxOrbitRadius);
            float angleInRadians = Random.Range(0f, Mathf.PI * 2);

            float x = Mathf.Cos(angleInRadians) * orbitRadius;
            float y = Mathf.Sin(angleInRadians) * orbitRadius;

            return new Vector3(x, y, 0) + transform.position;
        }
    }
}