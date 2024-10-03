using System;
using UnityEngine;
using UnityEngine.Pool;

public abstract class Spawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] protected T Prefab;

    protected ObjectPool<T> Pool;

    private int _quantityCreatedObject;
    private int _quantitySpawnedObject;

    public event Action<int> ChangedQuantitySpawnedObject;
    public event Action<int> ChangedQuantityCreatedObject;
    public event Action<int> ChangedQuantityActiveObject;

    protected virtual void Awake()
    {
        Pool = new ObjectPool<T>
            (
            createFunc: () => CreateObject(),
            actionOnGet: (obj) => Spawn(obj),
            actionOnRelease: (obj) => Release(obj)
           );

        ChangedQuantityCreatedObject?.Invoke(_quantityCreatedObject);
        ChangedQuantitySpawnedObject?.Invoke(_quantitySpawnedObject);
        ChangedQuantityActiveObject?.Invoke(Pool.CountActive);
    }

    protected virtual T CreateObject()
    {
        _quantityCreatedObject++;
        ChangedQuantityCreatedObject?.Invoke(_quantityCreatedObject);
        ChangedQuantityActiveObject?.Invoke(Pool.CountActive);
        return Instantiate(Prefab);
    }

    protected virtual void Spawn(T spawnedObject)
    {
        spawnedObject.gameObject.SetActive(true);
        _quantitySpawnedObject++;

        ChangedQuantitySpawnedObject?.Invoke(_quantitySpawnedObject);
        ChangedQuantityActiveObject?.Invoke(Pool.CountActive);
    }

    protected virtual void Release(T spawnedObject)
    {
        spawnedObject.gameObject.SetActive(false);
        ChangedQuantityActiveObject?.Invoke(Pool.CountActive);
    }

    protected T GetObject()
    {
        return Pool.Get();
    }
}
