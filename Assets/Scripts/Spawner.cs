using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public abstract class Spawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] protected T _prefab;

    protected ObjectPool<T> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<T>
            (
            createFunc: () => CreatedFunk(),
            actionOnGet: (obj) => ActionOnGet(obj),
            actionOnRelease: (obj) => ActionOnRelease(obj)
           );
    }

    protected abstract T CreatedFunk();

    protected virtual void ActionOnGet(T spawnedObject)
    {
        spawnedObject.gameObject.SetActive(true);
    }

    protected virtual void ActionOnRelease(T spawnedObject)
    {
        spawnedObject.gameObject.SetActive(false);
    }

    protected T GetObject()
    {
        return _pool.Get();
    }
}
