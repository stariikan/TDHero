using UnityEngine;

public class MagicExplosion : MagicBase
{
    public float explosionRadius = 5f;
    private SphereCollider sphereCollider;

    protected override void Start()
    {
        base.Start();
        sphereCollider = GetComponent<SphereCollider>();
        sphereCollider.radius = explosionRadius;
    }

    protected override void ApplyEffect(Collider other)
    {
        if (other.TryGetComponent(out Enemy_stats enemy))
        {
            enemy.GetDamage(damage);
        }
    }
}
