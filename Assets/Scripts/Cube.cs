using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Cube : MonoBehaviour
{
    public event Action<Cube> CubeCollision;

    private Color _defaultColor;
    private Material _material;

    private bool _isRelease = false;

    public bool IsRealised => _isRelease;

    private void Awake()
    {
        _material = GetComponent<MeshRenderer>().material;
        _defaultColor = _material.color;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Platform platform) && IsRealised == false)
        {
            ChangeRealease();
            Paint();
            StartCoroutine(Release());
        }
    }

    public void ChangeRealease()
    {
        if (_isRelease == false)
            _isRelease = true;
        else
            _isRelease = false;
    }

    public void PaintDefaultColor()
    {
        _material.color = _defaultColor;
    }

    private void Paint()
    {
        _material.color = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
    }

    private IEnumerator Release()
    {
        int minValue = 2;
        int maxValue = 5;
        int delay = UnityEngine.Random.Range(minValue, maxValue + 1);

        yield return new WaitForSeconds(delay);

        CubeCollision?.Invoke(this);
    }
}
