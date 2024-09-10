using UnityEngine;

public class Cube : MonoBehaviour
{
    private Color _defaultColor;

    private bool _isRelease=false;

    public bool IsRealised=>_isRelease;

    private void Awake()
    {
        _defaultColor = GetComponent<Renderer>().material.color;
    }

    public void PaintDefaultColor()
    {
        GetComponent<MeshRenderer>().material.color = _defaultColor;
    }

    public void Paint()
    {
        GetComponent<MeshRenderer>().material.color = Color.green;
    }

    public void ChangeRealease()
    {
        if(_isRelease==false)
            _isRelease=true;
        else
            _isRelease=false;
    }
}
