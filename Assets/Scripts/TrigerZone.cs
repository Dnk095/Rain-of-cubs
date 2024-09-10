using UnityEngine;

public class TrigerZone : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;

    private void OnTriggerEnter(Collider other)
    {
        other.TryGetComponent(out Cube cube);

        if (cube != null && cube.IsRealised == false)
        {
            cube.ChangeRealease();
            cube.Paint();
            _spawner.ReleaseCube(cube.gameObject);
        }
    }
}
