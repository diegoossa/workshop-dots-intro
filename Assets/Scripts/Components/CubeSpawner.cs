using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public struct CubeSpawner : IComponentData
{
    public float Separation;      // Distance between entities
    public float3 Bounds;      // Size of the spawn volume
    public Entity Prefab;  // Prefab to spawn
    public bool ShouldSpawn;     // Flag to trigger spawning
}
