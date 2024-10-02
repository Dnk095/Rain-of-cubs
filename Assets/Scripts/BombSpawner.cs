
using UnityEngine;

public class BombSpawner : Spawner<Bomb>
{
    protected override Bomb CreatedFunk()
    {
        return Instantiate(_prefab);
    }

    public void GetBomb(Transform transform)
    {
        Bomb bomb = GetObject();
        bomb.transform.position = transform.position;
    }
}
