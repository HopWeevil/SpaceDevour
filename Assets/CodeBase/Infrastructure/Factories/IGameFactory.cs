using System.Collections.Generic;
using System.Threading.Tasks;
using CodeBase.Enums;
using UnityEngine;

namespace CodeBase.Infrastructure.Factories
{
    public interface IGameFactory
    {
        Task CreatePlanet(Vector3 at, PlanetTypeId planetType);
        Task WarmUp();
        void CleanUp();
        Task<GameObject> CreateSpaceship(Vector3 at, ShipTypeId shipType);
    }
}