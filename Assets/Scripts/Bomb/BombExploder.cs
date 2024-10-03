using System.Collections.Generic;
using UnityEngine;

public class BombExploder : MonoBehaviour
{
    [SerializeField] private float _explodeRadius;
    [SerializeField] private float _explodeForce;
    [SerializeField] private ParticleSystem _effect;

    public void Explode(Bomb bomb)
    {
        float delay = 1f;

        ParticleSystem explodeEffect = Instantiate(_effect, bomb.transform.position, Quaternion.identity);

        Destroy(explodeEffect.gameObject, delay);

        foreach (Forced objectS in GetExplodabledObject(bomb))
            objectS.AddForce(bomb.transform.position, _explodeForce, _explodeRadius);
    }

    private List<Forced> GetExplodabledObject(Bomb bomb)
    {
        Collider[] hits = Physics.OverlapSphere(bomb.transform.position, _explodeRadius);

        List<Forced> forsedObject = new();

        foreach (Collider cubeCollider in hits)
            if (cubeCollider.TryGetComponent(out Forced component))
                forsedObject.Add(component);

        return forsedObject;
    }
}
