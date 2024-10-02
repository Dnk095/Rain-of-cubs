using System.Collections;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

[RequireComponent(typeof(MeshRenderer))]
public class Cube : MonoBehaviour
{
    private Color _defaultColor;
    private Material _material;

    private bool _isCollide = false;

    public event Action<Cube> CubeRelease;

    private void Awake()
    {
        _material = GetComponent<MeshRenderer>().material;
        _defaultColor = _material.color;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Platform _) && _isCollide == false)
        {
            _isCollide = true;
            Recolour();
            StartCoroutine(Release());
        }
    }

    public void ResetColor()
    {
        _material.color = _defaultColor;
    }

    private void Recolour()
    {
        _material.color = Random.ColorHSV();
    }

    private IEnumerator Release()
    {
        int minValue = 2;
        int maxValue = 5;
        int delay = Random.Range(minValue, maxValue + 1);

        yield return new WaitForSeconds(delay);

        CubeRelease?.Invoke(this);
        _isCollide = false;
    }
}