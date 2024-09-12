using CodeBase.Enums;
using System;
using UnityEngine;

namespace CodeBase.Data
{
    [Serializable]
    public class PlanetData
    {
        public PlanetTypeId TypeId;
        public Vector3 Position;

        public PlanetData(PlanetTypeId typeId, Vector3 position)
        {
            TypeId = typeId;
            Position = position;
        }
    }
}