using System;
using Unity.Entities;
using UnityEngine;

public class RotationSpeedAuthoring : MonoBehaviour
{
    public float rotationSpeed;

    public class RotationSpeedBaker : Baker<RotationSpeedAuthoring>
    {
        public override void Bake(RotationSpeedAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new RotationSpeed { Value = authoring.rotationSpeed });
        }
    }
}
