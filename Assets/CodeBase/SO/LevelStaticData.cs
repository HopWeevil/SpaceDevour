using CodeBase.Data;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.SO
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "Static Data/Level")]
    public class LevelStaticData : ScriptableObject
    {
        public string LevelKey;

        public List<PlanetData> Planets;

        public Vector3 InitialPlayerShipPosition;
    }
}
