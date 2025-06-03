using System;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public partial struct RotationSystem : ISystem
{
    
    public void OnUpdate(ref SystemState state)
    {
        var deltaTime = SystemAPI.Time.DeltaTime;
        foreach (var (rotationSpeed, localTransform) 
                 in SystemAPI.Query<RefRO<RotationSpeed>, RefRW<LocalTransform>>()
                     .WithAll<CubeComponent>())
        {
            // Rotate the entity around the Y-axis based on the rotation speed and delta time.
            localTransform.ValueRW.Rotation = math.mul(
                localTransform.ValueRW.Rotation,
                quaternion.Euler(0f, rotationSpeed.ValueRO.Value * deltaTime, 0f)
            );
        }
    }
}
