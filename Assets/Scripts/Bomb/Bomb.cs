using UnityEngine;

[RequireComponent(typeof(VisibleRender))]
public class Bomb : MonoBehaviour
{
    private VisibleRender _visible;

    private void Awake()
    {
        _visible = GetComponent<VisibleRender>();
    }

    private void Start()
    {
        float timeLife = GetRandonTimeLife();

        _visible.ChangeVisible(timeLife);
        Destroy(gameObject, timeLife);
    }

    private float GetRandonTimeLife()
    {
        float minTime = 2f;
        float maxTime = 5f;

        return Random.Range(minTime, maxTime + 1);
    }
}
