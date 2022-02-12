using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantGenerator : MonoBehaviour
{
    int bounds = 1000;
    public GameObject prefab;
    public int spawnCount = 1000;
    public Vector3[] spawnPos;

    void Start()
    {
        Vector3[] spawnPositions = new Vector3[spawnCount];
        for(int i = 0; i < spawnCount; i++)
        {
            spawnPositions[i] = new Vector3(Random.Range(0f, bounds), 0f, Random.Range(0f, bounds));
            spawnPositions[i] = new Vector3(spawnPositions[i].x, Terrain.activeTerrain.SampleHeight(spawnPositions[i]), spawnPositions[i].z);
            Instantiate(prefab, spawnPositions[i], new Quaternion());
        }
        spawnPos = spawnPositions;
    }
}
