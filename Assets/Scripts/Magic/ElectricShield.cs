using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ElectricShield : MagicBase
{
    private List<Enemy_stats> enemiesInShield = new List<Enemy_stats>();
    public Transform player;
    public Vector3 offset;

    protected override void Start()
    {
        base.Start();
        if (player == null)
        {
            Debug.LogError("Player reference is not assigned!");
            Destroy(gameObject);
            return;
        }
        player.GetComponent<PlayerStats>().godmode = true;
        StartCoroutine(DealDamageOverTime());
    }

    protected override void Update()
    {
        base.Update();
        if (player != null)
        {
            transform.position = player.position + player.TransformDirection(offset);
        }
    }

    private IEnumerator DealDamageOverTime()
    {
        while (timer < lifeTime)
        {
            yield return new WaitForSeconds(1f);
            foreach (var enemy in enemiesInShield)
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
        if (other.TryGetComponent(out Enemy_stats enemy) && !enemiesInShield.Contains(enemy))
        {
            enemiesInShield.Add(enemy);
        }
    }

    protected override void OnMagicEnd()
    {
        player.GetComponent<PlayerStats>().godmode = false;
        enemiesInShield.Clear();
    }
}
