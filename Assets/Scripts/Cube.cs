using UnityEngine;

public class Cube : MonoBehaviour
{
    private Color _defoultColor;

    private bool _isRelease=false;

    public bool IsRealised=>_isRelease;

    private void Awake()
    {
        _defoultColor = GetComponent<Renderer>().material.color;
    }

    public void PaintDefoultColor()
    {
        GetComponent<MeshRenderer>().material.color = _defoultColor;
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
