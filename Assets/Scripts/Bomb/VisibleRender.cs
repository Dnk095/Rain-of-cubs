using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer), typeof(Bomb))]
public class VisibleRender : MonoBehaviour
{
    private Material _material;
    private Color _default;

    private void Awake()
    {
        _material = GetComponent<Renderer>().material;
        _default = _material.color;
    }

    public void ChangeVisible(float time)
    {
        StartCoroutine(ChangeState(time));
    }

    public void ReturnDefaultState()
    {
        _material.color = _default;
    }

    private IEnumerator ChangeState(float time)
    {
        float delay = Time.deltaTime;
        float speed = delay / time;

        WaitForSeconds wait = new(delay);
        Color color = _material.color;

        while (color.a > 0)
        {
            color.a = Mathf.MoveTowards(color.a, 0, speed);
            _material.color = color;
            yield return wait;
        }
    }
}
