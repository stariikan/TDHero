using UnityEngine;

public class LowerEnemies : MagicBase
{
    public float effectTime;
    public float explosionRadius;
    private SphereCollider sphereCollider;
    private Transform projectileTransform;
    public Vector3 offset;

    protected override void Start()
    {
        base.Start();
        sphereCollider = GetComponent<SphereCollider>();
        sphereCollider.radius = explosionRadius;

        // Adjust position and rotation
        projectileTransform = transform;
        projectileTransform.position += offset;
        projectileTransform.rotation = Quaternion.Euler(-90, 0, 0);
        explosionRadius *= 1 + (level / 10);
        effectTime *= 1 + (level / 10);
    }

    protected override void ApplyEffect(Collider other)
    {
        if (other.TryGetComponent(out Enemy_stats enemy))
        {
            enemy.ChangeEnemyTag(effectTime);
        }
    }
}
