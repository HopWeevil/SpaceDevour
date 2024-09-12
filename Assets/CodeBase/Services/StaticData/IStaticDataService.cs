using CodeBase.Enums;
using CodeBase.SO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeBase.Services.StaticData
{
    public interface IStaticDataService
    {
        Task Initialize();
        ShipStaticData ForShip(ShipTypeId id);
        LevelStaticData ForLevel(string sceneKey);
        PlanetStaticData ForPlanet(PlanetTypeId id);
        List<ShipStaticData> GetAllShips();
    }
}