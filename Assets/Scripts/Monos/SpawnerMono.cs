using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class SpawnerMono : MonoBehaviour
{
    public float3 fieldDimensions;
    public int numEnemiesToSpawn;
    public GameObject enemyPrefab;
    public uint randomSeed;
    public float enemySpawnRate;
}

public class SpawnerBaker : Baker<SpawnerMono>
{
    public override void Bake(SpawnerMono authoring)
    {
        var spawnerEntity = GetEntity(TransformUsageFlags.Dynamic);

        AddComponent(spawnerEntity, new SpawnerProperties { fieldDimenions = authoring.fieldDimensions, 
            numEnemiesToSpawn = authoring.numEnemiesToSpawn,
            enemyPrefab = GetEntity(authoring.enemyPrefab, TransformUsageFlags.Dynamic),
            enemySpawnRate = authoring.enemySpawnRate});

        AddComponent(spawnerEntity, new SpawnerRandom {
            value = Unity.Mathematics.Random.CreateFromIndex(authoring.randomSeed)
        });

        AddComponent<EnemySpawnTimer>(spawnerEntity);
    }
}
