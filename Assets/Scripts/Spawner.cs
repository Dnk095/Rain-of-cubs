using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private GameObject _startPosition;

    private ObjectPool<Cube> _cubePool;

    private void Awake()
    {
        _cubePool = new ObjectPool<Cube>(
            createFunc: () => Instantiate(_cubePrefab),
            actionOnGet: (cube) => GetCubeParametrs(cube),
            actionOnRelease: (cube) => cube.gameObject.SetActive(false),
            defaultCapacity: 4,
            maxSize: 10
           );
    }

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private void GetCubeParametrs(Cube cube)
    {
        float randomValue = 20f;
        float positionX = Random.Range(-randomValue, randomValue);
        float positionZ = Random.Range(-randomValue, randomValue);

        cube.gameObject.transform.position = new Vector3(_startPosition.transform.position.x + positionX,
            _startPosition.transform.position.y,
            _startPosition.transform.position.z + positionZ);

        cube.gameObject.SetActive(true);

        cube.PaintDefaultColor();

        if (cube.IsRealised == true)
            cube.ChangeRealease();
    }

    private Cube GetCube()
    {
        return _cubePool.Get();
    }

    public void ReleaseCube(Cube cube)
    {
        cube.CubeCollision -= ReleaseCube;
        _cubePool.Release(cube);
    }

    private IEnumerator Spawn()
    {
        float delay = 1f;

        while (enabled)
        {
            yield return new WaitForSeconds(delay);

            Cube cube = GetCube();
            cube.CubeCollision += ReleaseCube;
        }
    }
}
