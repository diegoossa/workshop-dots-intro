using Unity.Burst;
using Unity.Entities;

namespace Systems
{
    public partial struct CalculateCubeCountSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<CubeComponent>();
        }

        public void OnUpdate(ref SystemState state)
        {
            EntityQuery cubeQuery = SystemAPI.QueryBuilder().WithAll<CubeComponent>().Build();
            int cubeCount = cubeQuery.CalculateEntityCount();
            CubeCounter.Instance.SetCubeCount(cubeCount);
            
            // Disable the system after the first update
            state.Enabled = false;
        }
    }
}
