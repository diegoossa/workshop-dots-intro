using System;
using Unity.Entities;
using Unity.Transforms;

public partial struct RotationSystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        foreach (var (rotationSpeed, localTransform) 
                 in SystemAPI.Query<RefRO<RotationSpeed>, RefRW<LocalTransform>>()
                     .WithAll<CubeComponent>())
        {
            // Rotate the entity around the Y-axis based on the rotation speed and delta time.
            localTransform.ValueRW = localTransform.ValueRW.RotateY(rotationSpeed.ValueRO.Value * SystemAPI.Time.DeltaTime);
        }
    }
}
