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

    protected override void ActionOnGet(Bomb spawnedObject)
    {
        base.ActionOnGet(spawnedObject);
        spawnedObject.Releasing += OnReleasing;
        spawnedObject.Init();
    }

    public void GetBomb(Transform transform)
    {
        Bomb bomb = GetObject();
        bomb.transform.position = transform.position;
    }

    private void OnReleasing(Bomb bomb)
    {
        base.ActionOnRelease(bomb);

        _exploder.Explode(bomb);
        _pool.Release(bomb);
        bomb.Releasing -= OnReleasing;
    }
}
