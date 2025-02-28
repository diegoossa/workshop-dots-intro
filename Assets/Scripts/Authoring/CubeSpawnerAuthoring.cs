using System;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class CubeSpawnerAuthoring : MonoBehaviour
{
    public float separation = 1f;
    public float3 bounds = new(10f, 10f, 10f);
    public GameObject prefab;
    
    public class CubeSpawnerBaker : Baker<CubeSpawnerAuthoring>
    {
        public override void Bake(CubeSpawnerAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new CubeSpawner { Prefab = GetEntity(authoring.prefab, TransformUsageFlags.Dynamic), Separation = authoring.separation, Bounds = authoring.bounds, ShouldSpawn = true});
        }
    }
}
