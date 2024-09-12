using CodeBase.Enums;
using UnityEngine;

namespace CodeBase.Logic.Spawner 
{ 
    public class PlanetSpawnMarker : MonoBehaviour
    {
        [SerializeField] private PlanetTypeId _typeId;

        public PlanetTypeId TypeId => _typeId;
    }
}