using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _cubePrefab;
    [SerializeField] private GameObject _startPosition;

    private ObjectPool<GameObject> _cubePool;

    private void Awake()
    {
        _cubePool = new ObjectPool<GameObject>(
            createFunc: () => Instantiate(_cubePrefab),
            actionOnGet: (cube) => GetCubeParametrs(cube),
            actionOnRelease: (cube) => cube.SetActive(false),
            defaultCapacity: 4,
            maxSize: 10
           );
    }

    private void Start()
    {
        float delay = 1.0f;

        InvokeRepeating(nameof(GetCube), 0f, delay);
    }

    public void ReleaseCube(GameObject cube)
    {
        StartCoroutine(Release(cube));
    }

    private void GetCubeParametrs(GameObject gameObject)
    {
        float randomValue = 20f;
        float positionX = Random.Range(-randomValue, randomValue);
        float positionZ = Random.Range(-randomValue, randomValue);

        gameObject.transform.position = new Vector3(_startPosition.transform.position.x + positionX,
            _startPosition.transform.position.y,
            _startPosition.transform.position.z + positionZ);

        gameObject.SetActive(true);

        gameObject.TryGetComponent(out Cube cube);
        cube.PaintDefaultColor();

        if (cube.IsRealised == true)
            cube.ChangeRealease();
    }

    private void GetCube()
    {
        _cubePool.Get();
    }

    private IEnumerator Release(GameObject cube)
    {
        int minValue = 2;
        int maxValue = 5;
        int delay = Random.Range(minValue, maxValue + 1);

        yield return new WaitForSeconds(delay);

        _cubePool.Release(cube);
    }
}
