using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FreezeThrower : MagicBase
{
    public float freezingPower;
    public Transform player;
    public Vector3 offset;
    private List<Enemy_stats> enemiesInRange = new List<Enemy_stats>();

    protected override void Start()
    {
        freezingPower *= 1 + (level / 10);
        base.Start();
        if (player == null)
        {
            Debug.LogError("Player reference is not assigned!");
            Destroy(gameObject);
            return;
        }
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
                    enemy.ReduceSpeed(freezingPower);
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
