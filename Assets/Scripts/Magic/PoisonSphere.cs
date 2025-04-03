using UnityEngine;

public class PoisonSphere : MagicBase
{
    public float poisonDamagePerSec;
    public float projectileSpeed = 10f;
    public Vector3 offset;
    private Transform projectileTransform;

    protected override void Start()
    {
        projectileTransform = transform;
        projectileTransform.position += offset;
    }

    protected override void ApplyEffect(Collider other)
    {
        if (other.TryGetComponent(out Enemy_stats enemy))
        {
            enemy.EnemyPoison(poisonDamagePerSec);
        }
    }

    protected override void Update()
    {
        base.Update();
        projectileTransform.position += projectileTransform.forward * projectileSpeed * Time.deltaTime;
    }
}
