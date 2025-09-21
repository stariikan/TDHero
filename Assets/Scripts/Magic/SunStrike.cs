using UnityEngine;

public class SunStrike : MagicBase
{
    public float timerSpeed;

    protected override void Update()
    {
        base.Update();
    }
    // Empty implementation to satisfy the abstract method requirement
    protected override void ApplyEffect(Collider other)
    {
        if (other.CompareTag(enemyTag) || other.CompareTag(enemyTag2))
        {
            Enemy_stats enemyStats = other.GetComponent<Enemy_stats>();
            if (enemyStats != null)
            {
                enemyStats.GetDamage(damage);
            }
        }
    }
    protected override void CancelEffect(Collider other)
    {
        if (other.TryGetComponent(out Enemy_stats enemy))
        {

        }
    }
}
