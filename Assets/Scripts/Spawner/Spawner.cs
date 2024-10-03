using System;
using UnityEngine;
using UnityEngine.Pool;

public abstract class Spawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private T _prefab;

    private ObjectPool<T> _pool;

    private int _quantityCreatedObject;
    private int _quantitySpawnedObject;

    public event Action<int> ChangedQuantitySpawnedObject;
    public event Action<int> ChangedQuantityCreatedObject;
    public event Action<int> ChangedQuantityActiveObject;

    protected virtual void Awake()
    {
        _pool = new ObjectPool<T>
            (
            createFunc: () => CreateObject(),
            actionOnGet: (obj) => Spawn(obj),
            actionOnRelease: (obj) => Release(obj)
           );

        ChangedQuantityCreatedObject?.Invoke(_quantityCreatedObject);
        ChangedQuantitySpawnedObject?.Invoke(_quantitySpawnedObject);
        ChangedQuantityActiveObject?.Invoke(_pool.CountActive);
    }

    protected T CreateObject()
    {
        _quantityCreatedObject++;
        ChangedQuantityCreatedObject?.Invoke(_quantityCreatedObject);
        ChangedQuantityActiveObject?.Invoke(_pool.CountActive);
        return Instantiate(_prefab);
    }

    protected virtual void Spawn(T spawnedObject)
    {
        spawnedObject.gameObject.SetActive(true);
        _quantitySpawnedObject++;

        ChangedQuantitySpawnedObject?.Invoke(_quantitySpawnedObject);
        ChangedQuantityActiveObject?.Invoke(_pool.CountActive);
    }

    protected void Release(T spawnedObject)
    {
        spawnedObject.gameObject.SetActive(false);
        ChangedQuantityActiveObject?.Invoke(_pool.CountActive);
    }

    protected T GetObject()
    {
        return _pool.Get();
    }

    protected void ReturnInPool(T spawnedObject)
    {
        _pool.Release(spawnedObject);
    }
}
