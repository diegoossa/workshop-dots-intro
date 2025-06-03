using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

/// <summary>
/// System that moves cubes in a flower pattern.
/// </summary>
partial struct CubeMovementSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var time = (float)SystemAPI.Time.ElapsedTime;

        foreach (var (flowerMovement, localTransform)
                 in SystemAPI.Query<RefRO<FlowerMovement>, RefRW<LocalTransform>>())
        {
            // Calculate the polar angle θ with the cube's speed and angleOffset.
            float theta = time * flowerMovement.ValueRO.Speed + flowerMovement.ValueRO.AngleOffset;
            
            // Use the flower equation to compute the radius:
            float r = math.sin(4.0f * theta);

            // Convert polar coordinates into Cartesian. Then apply per-entity scale.
            float offsetX = r * math.cos(theta) * flowerMovement.ValueRO.Scale;
            float offsetZ = r * math.sin(theta) * flowerMovement.ValueRO.Scale;

            // Calculate the new position by adding the polar offset to the stored initial position.
            // This keeps the cube's initial Y value.
            float3 newPosition = flowerMovement.ValueRO.Position + new float3(offsetX, 0f, offsetZ);

            localTransform.ValueRW.Position = newPosition;
        }
    }
}

// /// <summary>
// /// System that moves cubes in a flower pattern using jobs.
// /// </summary>
// partial struct CubeMovementWithJobsSystem : ISystem
// {
//     [BurstCompile]
//     public void OnUpdate(ref SystemState state)
//     {
//         var time = (float)SystemAPI.Time.ElapsedTime;
//         new CubeMovementJob { ElapsedTime = time }.ScheduleParallel();
//     }
//
//     [BurstCompile]
//     public partial struct CubeMovementJob : IJobEntity
//     {
//         public float ElapsedTime;
//         
//         void Execute(FlowerMovement flowerMovement, ref LocalTransform localTransform)
//         {
//             // Calculate the polar angle θ with the cube's speed and angleOffset.
//             float theta = ElapsedTime * flowerMovement.Speed + flowerMovement.AngleOffset;
//             
//             // Use the flower equation to compute the radius:
//             float r = math.sin(4.0f * theta);
//             
//             // Convert polar coordinates into Cartesian. Then apply per-entity scale.
//             float offsetX = r * math.cos(theta) * flowerMovement.Scale;
//             float offsetZ = r * math.sin(theta) * flowerMovement.Scale;
//             
//             // Calculate the new position by adding the polar offset to the stored initial position.
//             // This keeps the cube's initial Y value.
//             float3 newPosition = flowerMovement.Position + new float3(offsetX, 0f, offsetZ);
//             
//             localTransform.Position = newPosition;
//         }
//     }
// }
