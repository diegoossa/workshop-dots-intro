using System;
using Unity.Entities;
using UnityEngine;

public class FlowerMovementAuthoring : MonoBehaviour
{
    public float speed = 1f;
    public float angleOffset = 0f;
    public float scale = 1f;
    
    public class FlowerMovementBaker : Baker<FlowerMovementAuthoring>
    {
        public override void Bake(FlowerMovementAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new FlowerMovement
            {
                Speed = authoring.speed,
                AngleOffset = authoring.angleOffset,
                Scale = authoring.scale
            });
        }
    }
}
