using Unity.Entities;
using Unity.Mathematics;

public struct SpawnerProperties : IComponentData
{
    public float3 fieldDimenions;
    public int numEnemiesToSpawn;
    public Entity enemyPrefab;
    public float enemySpawnRate;
}

public struct EnemySpawnTimer : IComponentData
{
    public float value;
}
