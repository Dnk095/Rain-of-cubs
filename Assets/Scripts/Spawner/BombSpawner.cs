using UnityEngine;

[RequireComponent(typeof(BombExploder))]
public class BombSpawner : Spawner<Bomb>
{
    private BombExploder _exploder;

    protected override void Awake()
    {
        base.Awake();
        _exploder = GetComponent<BombExploder>();
    }

    protected override void Spawn(Bomb spawnedObject)
    {
        base.Spawn(spawnedObject);
        spawnedObject.Releasing += OnReleasing;
        spawnedObject.Init();
    }

    public void GetBomb(Vector3 position)
    {
        Bomb bomb = GetObject();
        bomb.transform.position = position;
    }

    private void OnReleasing(Bomb bomb)
    {
        base.Release(bomb);

        _exploder.Explode(bomb);
        ReturnInPool(bomb);
        bomb.Releasing -= OnReleasing;
    }
}
