using UnityEngine;

public class LightningStun : MagicBase
{
    public float explosionRadius = 5f;
    public Vector3 offset;
    private Transform projectileTransform;

    protected override void Start()
    {
        base.Start();

        // Adjust position and rotation
        projectileTransform = transform;
        projectileTransform.position += offset;
        projectileTransform.rotation = Quaternion.Euler(90, 0, 0);
    }

    protected override void ApplyEffect(Collider other)
    {
        if (other.TryGetComponent(out Enemy_stats enemy))
        {
            enemy.GetDamage(damage);
            enemy.Stun(); // Apply the stun effect
        }
    }
    protected override void CancelEffect(Collider other)
    {
        if (other.TryGetComponent(out Enemy_stats enemy))
        {
            
        }
    }
}
