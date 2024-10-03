using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(VisibleRender))]
public class Bomb : MonoBehaviour
{
    private VisibleRender _visible;

    public event Action<Bomb> Releasing;

    private void Awake()
    {
        _visible = GetComponent<VisibleRender>();
    }

    public void Init()
    {
        float timeLife = GetRandonTimeLife();

        _visible.ChangeVisible(timeLife);
        StartCoroutine(Release(timeLife));
    }

    private float GetRandonTimeLife()
    {
        float minTime = 2f;
        float maxTime = 5f;

        return Random.Range(minTime, maxTime + 1);
    }

    private IEnumerator Release(float time)
    {
        yield return new WaitForSeconds(time);
        _visible.ReturnDefaultState();
        Releasing?.Invoke(this);
    }
}
