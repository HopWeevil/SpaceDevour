using CodeBase.Enums;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.SO
{
    [CreateAssetMenu(fileName = "ShipData", menuName = "Static Data/Ship")]
    public class ShipStaticData : ScriptableObject
    {
        [field: SerializeField] public ShipTypeId TypeId { get; private set; }
        [field: SerializeField] public float RotationSpeed { get; private set; }
        [field: SerializeField] public float Acceleration { get; private set; }
        [field: SerializeField] public float BackwardAcceleration { get; private set; }
        [field: SerializeField] public float MaxSpeed { get; private set; }
        [field: SerializeField] public float DriftFactor { get; private set; }
        [field: SerializeField] public float Drag { get; private set; }
        [field: SerializeField] public float AngularDrag { get; private set; }
        [field: SerializeField] public float ResourceGrabRadius { get; private set; }
        [field: SerializeField] public float HookSpeed { get; private set; }
        [field: SerializeField] public AssetReferenceGameObject PrefabReference { get; private set; }
    }
}