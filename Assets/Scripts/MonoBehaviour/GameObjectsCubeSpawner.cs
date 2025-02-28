using System;
using UnityEngine;

public class GameObjectsCubeSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public float separation = 1f;
    public Vector3 cubeSize = new Vector3(10f, 10f, 10f);

    [Header("Prefab Settings")]
    public GameObject cubePrefab;

    void Start()
    {
        SpawnCubes();
    }

    void SpawnCubes()
    {
        // Calculate how many cubes can fit in each dimension
        int countX = Mathf.FloorToInt(cubeSize.x / separation);
        int countY = Mathf.FloorToInt(cubeSize.y / separation);
        int countZ = Mathf.FloorToInt(cubeSize.z / separation);

        // Calculate starting position (corner of the cube)
        Vector3 startPosition = transform.position + new Vector3(
            -cubeSize.x / 2 + separation / 2,
            -cubeSize.y / 2 + separation / 2,
            -cubeSize.z / 2 + separation / 2
        );
        
        // Fill the entire cube
        for (int y = 0; y < countY; y++)
        {
            for (int z = 0; z < countZ; z++)
            {
                for (int x = 0; x < countX; x++)
                {
                    Vector3 position = startPosition + new Vector3(
                        x * separation,
                        y * separation,
                        z * separation
                    );
                    
                    Instantiate(cubePrefab, position, Quaternion.identity);
                }
            }
        }

        Debug.Log($"Spawned {countX * countY * countZ} cubes");
        CubeCounter.Instance.SetCubeCount(countX * countY * countZ);
    }
}
