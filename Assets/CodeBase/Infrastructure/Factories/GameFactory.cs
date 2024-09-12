using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Services.StaticData;
using CodeBase.Services.Randomizer;
using CodeBase.Logic.Spaceship;
using System.Threading.Tasks;
using CodeBase.Enums;
using UnityEngine;
using CodeBase.SO;
using Zenject;
using CodeBase.Logic;

namespace CodeBase.Infrastructure.Factories
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assets;
        private readonly IStaticDataService _staticData;
        private readonly IRandomService _randomService;
        private readonly DiContainer _container;
   
        public GameFactory(DiContainer container, IAssetProvider assets, IStaticDataService staticDataService, IRandomService randomService)
        {
            _assets = assets;
            _staticData = staticDataService;
            _randomService = randomService;
            _container = container;
        }

        public async Task WarmUp()
        {
            foreach(ShipStaticData data in _staticData.GetAllShips())
            {
                await _assets.Load<GameObject>(data.PrefabReference);
            }
        }

        public void CleanUp()
        {
            _assets.CleanUp();
        }

        public async Task CreatePlanet(Vector3 at, PlanetTypeId planetType)
        {
            PlanetStaticData data = _staticData.ForPlanet(planetType);
            GameObject prefab = await _assets.Load<GameObject>(data.PrefabReference);

            Planet planet = Object.Instantiate(prefab, at, Quaternion.identity).GetComponent<Planet>();
            planet.SetStats(data);
            _container.InjectGameObject(planet.gameObject);
        }

        public async Task<GameObject> CreateSpaceship(Vector3 at, ShipTypeId shipType)
        {
            ShipStaticData data = _staticData.ForShip(shipType);
            GameObject prefab = await _assets.Load<GameObject>(data.PrefabReference);
            GameObject ship = Object.Instantiate(prefab, at, Quaternion.identity);
            _container.InjectGameObject(ship);

            ship.GetComponentInChildren<SpaceshipMover>().SetStats(data);
            ship.GetComponentInChildren<ShipResourceCollector>().SetStats(data);
            return ship;
        }

    }
}