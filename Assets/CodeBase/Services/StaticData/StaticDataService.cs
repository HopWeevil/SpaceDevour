using CodeBase.Enums;
using CodeBase.SO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeBase.Infrastructure.AssetManagement;

namespace CodeBase.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string ShipDataLabel = "ShipStaticData";
        private const string PlanetDataLabel = "PlanetStaticData";
        private const string LevelDataLabel = "LevelStaticData";

        private Dictionary<ShipTypeId, ShipStaticData> _ships;
        private Dictionary<PlanetTypeId, PlanetStaticData> _planets;
        private Dictionary<string, LevelStaticData> _levels;

        private readonly IAssetProvider _assetProvider;

        public StaticDataService(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public async Task Initialize()
        {
            _ships = (await _assetProvider.LoadAll<ShipStaticData>(ShipDataLabel)).ToDictionary(x => x.TypeId, x => x);
            _planets = (await _assetProvider.LoadAll<PlanetStaticData>(PlanetDataLabel)).ToDictionary(x => x.TypeId, x => x);
            _levels = (await _assetProvider.LoadAll<LevelStaticData>(LevelDataLabel)).ToDictionary(x => x.LevelKey, x => x);
        }

       
        public ShipStaticData ForShip(ShipTypeId id)
        {
            if (_ships.TryGetValue(id, out ShipStaticData staticData))
            {
                return staticData;
            }
            else
            {
                return null;
            }
        }

        public LevelStaticData ForLevel(string sceneKey)
        {
            if (_levels.TryGetValue(sceneKey, out LevelStaticData staticData))
            {
                return staticData;
            }
            else
            {
                return null;
            }
        }

        public PlanetStaticData ForPlanet(PlanetTypeId id)
        {
            if (_planets.TryGetValue(id, out PlanetStaticData staticData))
            {
                return staticData;
            }
            else
            {
                return null;
            }
        }

        public List<ShipStaticData> GetAllShips()
        {
            return _ships.Values.ToList();
        }
    }
}