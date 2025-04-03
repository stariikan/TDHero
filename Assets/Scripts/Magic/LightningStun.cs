using UnityEngine;

public class LightningStun : MagicBase
{
    public float explosionRadius = 5f;
    private BoxCollider boxCollider;
    public Vector3 offset;
    private Transform projectileTransform;

    protected override void Start()
    {
        base.Start();

        boxCollider = GetComponent<BoxCollider>();
        if (boxCollider == null)
        {
            Debug.LogError("BoxCollider is missing on LightningStun!");
            return;
        }

        // Set the Z-axis size for BoxCollider
        Vector3 newSize = boxCollider.size;
        newSize.z = explosionRadius;
        boxCollider.size = newSize;

        // Adjust position and rotation
        projectileTransform = transform;
        projectileTransform.position += offset;
        projectileTransform.rotation = Quaternion.Euler(90, 0, 0);
    }

    protected override void ApplyEffect(Collider other)
    {
        if (other.TryGetComponent(out Enemy_stats enemy))
        {
            enemy.Stun(); // Apply the stun effect
        }
    }
}
