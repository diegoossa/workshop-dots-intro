using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

partial struct SpawnerSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var random = Random.CreateFromIndex(42);

        foreach (var spawner in SystemAPI.Query<RefRW<CubeSpawner>>())
        {
            if (!spawner.ValueRO.ShouldSpawn || spawner.ValueRO.Prefab == Entity.Null)
                continue;

            float separation = spawner.ValueRO.Separation;
            float3 bounds = spawner.ValueRO.Bounds;

            // Calculate how many entities can fit in each dimension
            int countX = (int)(bounds.x / separation);
            int countY = (int)(bounds.y / separation);
            int countZ = (int)(bounds.z / separation);

            // Calculate starting position (corner of the cube)
            float3 startPosition = new float3(
                -bounds.x / 2 + separation / 2,
                -bounds.y / 2 + separation / 2,
                -bounds.z / 2 + separation / 2
            );

            // Fill the entire cube
            for (int y = 0; y < countY; y++)
            {
                for (int z = 0; z < countZ; z++)
                {
                    for (int x = 0; x < countX; x++)
                    {
                        float3 position = startPosition + new float3(
                            x * separation,
                            y * separation,
                            z * separation
                        );

                        // Spawn entity at calculated position
                        var instance = state.EntityManager.Instantiate(spawner.ValueRO.Prefab);
                        SystemAPI.SetComponent(instance, new LocalTransform { Position = position, Rotation = quaternion.identity, Scale = 1f });
                        SystemAPI.SetComponent(instance, new FlowerMovement { Speed = 1.0f, Position = position, AngleOffset = random.NextFloat(0f, math.PI * 2f), Scale = 0.2f });
                        SystemAPI.SetComponent(instance, new CubeComponent());
                    }
                }
            }

            // Prevent continuous spawning
            spawner.ValueRW.ShouldSpawn = false;
        }
    }
}
