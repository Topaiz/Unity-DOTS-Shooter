using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public readonly partial struct SpawnerAspect : IAspect
{
    public readonly Entity entity;

    private readonly RefRO<LocalTransform> _transform;
    private LocalTransform transform => _transform.ValueRO;

    private readonly RefRO<SpawnerProperties> _spawnerProperties;
    private readonly RefRW<SpawnerRandom> _spawnerRandom;
    private readonly RefRW<EnemySpawnTimer> _enemySpawnTimer; 

    //Anywhere that previously had _transformAspect.Postion, just replace that with Transform.Position

    public int numEnemiesToSpawn => _spawnerProperties.ValueRO.numEnemiesToSpawn;

    public Entity enemyPrefab => _spawnerProperties.ValueRO.enemyPrefab;
    
    public LocalTransform GetRandomEnemyTransform()
    {
        return new LocalTransform
        {
            Position = GetRandomPosition(),
            Rotation = Quaternion.identity,
            Scale = 1
        };
    }

    private float3 GetRandomPosition()
    {
        float3 randomPosition;
        randomPosition = _spawnerRandom.ValueRW.value.NextFloat3(MinCorner, MaxCorner);

        return randomPosition;
    }

    private float3 MinCorner => transform.Position - HalfDimensions;
    private float3 MaxCorner => transform.Position + HalfDimensions;
    private float3 HalfDimensions => new()
    {
        x = _spawnerProperties.ValueRO.fieldDimenions.x * .5f,
        y = _spawnerProperties.ValueRO.fieldDimenions.y * .5f,
        z = _spawnerProperties.ValueRO.fieldDimenions.z * .5f
    };
    public float3 Position => transform.Position;


    public float enemySpawnTimer
    {
        get => _enemySpawnTimer.ValueRO.value;
        set => _enemySpawnTimer.ValueRW.value = value;
    }

    public bool timeToSpawnEnemy => enemySpawnTimer <= 0;
    public float enemySpawnRate => _spawnerProperties.ValueRO.enemySpawnRate;
}
