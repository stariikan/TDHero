using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FlameWall : MagicBase
{
    public Vector3 offset;
    public float tickPerSec;
    private List<Enemy_stats> enemiesInRange = new List<Enemy_stats>();

    protected override void Start()
    {
        base.Start();
        StartCoroutine(DealDamageOverTime());
    }

    protected override void Update()
    {
        base.Update();
    }

    private IEnumerator DealDamageOverTime()
    {
        while (timer < lifeTime)
        {
            yield return new WaitForSeconds(1/tickPerSec);
            foreach (var enemy in enemiesInRange)
            {
                if (enemy != null)
                {
                    enemy.GetDamage(damage);
                }
            }
        }
    }

    protected override void ApplyEffect(Collider other)
    {
        if (other.TryGetComponent(out Enemy_stats enemy) && !enemiesInRange.Contains(enemy))
        {
            enemiesInRange.Add(enemy);
        }
    }
    protected override void CancelEffect(Collider other)
    {
        if (other.TryGetComponent(out Enemy_stats enemy) && enemiesInRange.Contains(enemy))
        {
            enemiesInRange.Remove(enemy);
        }
    }

    protected override void OnMagicEnd()
    {
        enemiesInRange.Clear();
    }
}
