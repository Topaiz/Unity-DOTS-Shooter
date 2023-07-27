using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

[BurstCompile]
[UpdateInGroup(typeof(InitializationSystemGroup))]
public partial struct SpawnEnemySystem : ISystem 
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<SpawnerProperties>();
    }

    [BurstCompile]
    public void OnDestroy(ref SystemState state)
    {

    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var deltaTime = SystemAPI.Time.DeltaTime;
        var ecbSingleton = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();

        new SpawnEnemyJob
        {
            DeltaTime = deltaTime,
            ECB = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged)
        }.Run();


        /*state.Enabled = false;
        var spawnerEntity = SystemAPI.GetSingletonEntity<SpawnerProperties>();
        var spawner = SystemAPI.GetAspect<SpawnerAspect>(spawnerEntity);

        var ecb = new EntityCommandBuffer(Unity.Collections.Allocator.Temp);

        for (int i = 0; i < spawner.numEnemiesToSpawn; i++)
        {
            var newEnemy = ecb.Instantiate(spawner.enemyPrefab);
            var newEnemyTransform = spawner.GetRandomEnemyTransform();
            ecb.SetComponent(newEnemy, newEnemyTransform);
        }

        ecb.Playback(state.EntityManager);*/
    }
}

[BurstCompile]
public partial struct SpawnEnemyJob : IJobEntity
{
    public float DeltaTime;
    public EntityCommandBuffer ECB;
    private void Execute(SpawnerAspect spawner)
    {
        spawner.enemySpawnTimer -= DeltaTime;
        if (!spawner.timeToSpawnEnemy) return;

        spawner.enemySpawnTimer = spawner.enemySpawnRate;
        var newEnemy = ECB.Instantiate(spawner.enemyPrefab);

        var newEnemyTransform = spawner.GetRandomEnemyTransform();
        ECB.SetComponent(newEnemy, newEnemyTransform);
    }
}
