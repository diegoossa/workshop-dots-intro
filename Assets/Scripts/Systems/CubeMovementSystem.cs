using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

// /// <summary>
// /// System that moves cubes in a flower pattern.
// /// </summary>
// partial struct CubeMovementSystem : ISystem
// {
//     [BurstCompile]
//     public void OnUpdate(ref SystemState state)
//     {
//         var time = (float)SystemAPI.Time.ElapsedTime;
//
//         foreach (var (cube, localTransform) in SystemAPI.Query<RefRO<CubeComponent>, RefRW<LocalTransform>>())
//         {
//             // Calculate the polar angle θ with the cube's speed and angleOffset.
//             float theta = time * cube.ValueRO.Speed + cube.ValueRO.AngleOffset;
//             
//             // Use the flower equation to compute the radius:
//             float r = math.sin(4.0f * theta);
//
//             // Convert polar coordinates into Cartesian. Then apply per-entity scale.
//             float offsetX = r * math.cos(theta) * cube.ValueRO.Scale;
//             float offsetZ = r * math.sin(theta) * cube.ValueRO.Scale;
//
//             // Calculate the new position by adding the polar offset to the stored initial position.
//             // This keeps the cube's initial Y value.
//             float3 newPosition = cube.ValueRO.Position + new float3(offsetX, 0f, offsetZ);
//
//             localTransform.ValueRW.Position = newPosition;
//         }
//     }
// }

/// <summary>
/// System that moves cubes in a flower pattern using jobs.
/// </summary>
partial struct CubeMovementWithJobsSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var time = (float)SystemAPI.Time.ElapsedTime;
        new CubeMovementJob { ElapsedTime = time }.ScheduleParallel();
    }

    [BurstCompile]
    public partial struct CubeMovementJob : IJobEntity
    {
        public float ElapsedTime;
        
        void Execute(CubeComponent cube, ref LocalTransform localTransform)
        {
            // Calculate the polar angle θ with the cube's speed and angleOffset.
            float theta = ElapsedTime * cube.Speed + cube.AngleOffset;
            
            // Use the flower equation to compute the radius:
            float r = math.sin(4.0f * theta);
            
            // Convert polar coordinates into Cartesian. Then apply per-entity scale.
            float offsetX = r * math.cos(theta) * cube.Scale;
            float offsetZ = r * math.sin(theta) * cube.Scale;
            
            // Calculate the new position by adding the polar offset to the stored initial position.
            // This keeps the cube's initial Y value.
            float3 newPosition = cube.Position + new float3(offsetX, 0f, offsetZ);
            
            localTransform.Position = newPosition;
        }
    }
}
