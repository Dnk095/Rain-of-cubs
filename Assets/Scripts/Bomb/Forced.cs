using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Forced : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void AddForce(Vector3 position, float force, float radius)
    {
        _rigidbody.AddExplosionForce(force, position, radius);
    }
}
