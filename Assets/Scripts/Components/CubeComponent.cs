using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public struct CubeComponent : IComponentData
{
    public float Speed;       // How fast the entity moves (radians per second)
    public float AngleOffset;   // Optional angular offset (radians)
    public float3 Position;   // Current position of the entity
    public float Scale;       // Scale of the movement
}