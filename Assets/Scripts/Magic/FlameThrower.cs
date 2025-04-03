using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FlameThrower : MagicBase
{
    public Transform player;
    public Vector3 offset;
    private List<Enemy_stats> enemiesInRange = new List<Enemy_stats>();

    protected override void Start()
    {
        base.Start();
        StartCoroutine(DealDamageOverTime());
    }

    protected override void Update()
    {
        base.Update();
        if (player != null)
        {
            transform.position = player.position + player.TransformDirection(offset);
            transform.rotation = player.rotation * Quaternion.Euler(0, -90, 0);
        }
    }

    private IEnumerator DealDamageOverTime()
    {
        while (timer < lifeTime)
        {
            yield return new WaitForSeconds(1f);
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

    protected override void OnMagicEnd()
    {
        enemiesInRange.Clear();
    }
}
