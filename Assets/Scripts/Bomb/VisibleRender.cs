using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer), typeof(Bomb))]
public class VisibleRender : MonoBehaviour
{
    private Material _material;

    private void Awake()
    {
        _material = GetComponent<Renderer>().material;
    }

    public void ChangeVisible(float time)
    {
        StartCoroutine(ChangeState(time));
    }

    private IEnumerator ChangeState(float time)
    {
        float delay = 1;
        float speed = 5f;

        WaitForSeconds wait = new(delay);
        Color color = _material.color;

        while (color.a > 0)
        {
            color.a = Mathf.MoveTowards(color.a, 0, speed * Time.deltaTime);
            _material.color = color;
            yield return wait;
        }
    }
}
