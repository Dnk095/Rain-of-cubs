using System;
using UnityEngine;
using UnityEngine.Pool;

public abstract class Spawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] protected T _prefab;

    protected ObjectPool<T> _pool;

    private int _quantityCreatedObject;
    private int _quantitySpawnedObject;

    public event Action<int, int, int> CountObject;

    protected virtual void Awake()
    {
        _pool = new ObjectPool<T>
            (
            createFunc: () => CreatedFunk(),
            actionOnGet: (obj) => ActionOnGet(obj),
            actionOnRelease: (obj) => ActionOnRelease(obj)
           );

        CountObject?.Invoke(_quantityCreatedObject, _quantitySpawnedObject, _pool.CountActive);
    }

    protected virtual T CreatedFunk()
    {
        _quantityCreatedObject++;
        return Instantiate(_prefab);
    }

    protected virtual void ActionOnGet(T spawnedObject)
    {
        spawnedObject.gameObject.SetActive(true);
        _quantitySpawnedObject++;

        CountObject?.Invoke(_quantityCreatedObject, _quantitySpawnedObject, _pool.CountActive);
    }

    protected virtual void ActionOnRelease(T spawnedObject)
    {
        spawnedObject.gameObject.SetActive(false);
        CountObject?.Invoke(_quantityCreatedObject, _quantitySpawnedObject, _pool.CountActive);
    }

    protected T GetObject()
    {
        return _pool.Get();
    }
}
