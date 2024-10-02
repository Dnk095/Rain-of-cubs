using System.Collections;
using UnityEngine;

public class CubeSpawner : Spawner<Cube>
{
    [SerializeField] private Transform _startPosition;
    [SerializeField] private BombSpawner _bombSpawner;

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    protected override Cube CreatedFunk()
    {
        return Instantiate(_prefab);
    }

    protected override void ActionOnGet(Cube cube)
    {
        float randomValue = 20f;
        float positionX = Random.Range(-randomValue, randomValue);
        float positionZ = Random.Range(-randomValue, randomValue);

        cube.gameObject.transform.position = new Vector3(_startPosition.position.x + positionX,
            _startPosition.position.y,
            _startPosition.position.z + positionZ);

        cube.gameObject.SetActive(true);
        cube.CubeRelease += ReleaseCube;
        cube.ResetColor();
    }

    private void ReleaseCube(Cube cube)
    {
        _bombSpawner.GetBomb(cube.transform);
        cube.CubeRelease -= ReleaseCube;
        _pool.Release(cube);
    }

    private IEnumerator Spawn()
    {
        float delay = 1f;

        WaitForSeconds wait = new(delay);

        while (enabled)
        {
            yield return wait;
            GetObject();
        }
    }
}
