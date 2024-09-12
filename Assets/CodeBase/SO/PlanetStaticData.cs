using CodeBase.Enums;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.SO
{
    [CreateAssetMenu(fileName = "PlanetData", menuName = "Static Data/Planet")]
    public class PlanetStaticData : ScriptableObject
    {
        [field: SerializeField] public PlanetTypeId TypeId { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public int NumberOfAsteroids { get; private set; }
        [field: SerializeField] public float MinOrbitRadius { get; private set; }
        [field: SerializeField] public float MaxOrbitRadius { get; private set; }
        [field: SerializeField] public float MinOrbitSpeed { get; private set; }
        [field: SerializeField] public float MaxOrbitSpeed { get; private set; }
        [field: SerializeField] public float MinRotationSpeed { get; private set; }
        [field: SerializeField] public float MaxRotationSpeed { get; private set; }
        [field: SerializeField] public float MinAsteroidSize { get; private set; }
        [field: SerializeField] public float MaxAsteroidSize { get; private set; }
        [field: SerializeField] public AssetReferenceGameObject PrefabReference { get; private set; }
    }
}