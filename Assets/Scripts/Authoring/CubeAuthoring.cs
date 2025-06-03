using System;
using Unity.Entities;
using UnityEngine;

public class CubeAuthoring : MonoBehaviour
{
    public class CubeBaker : Baker<CubeAuthoring>
    {
        public override void Bake(CubeAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent<CubeComponent>(entity);
        }
    }
}