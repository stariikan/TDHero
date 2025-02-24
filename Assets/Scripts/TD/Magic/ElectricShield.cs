<<<<<<< HEAD
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ElectricShield : MonoBehaviour
{
    public float damage;
    public float duration;
    private float timer;

    public Transform player;
    public Vector3 offset;
    private CapsuleCollider magicCollider;
    private List<Enemy_stats> enemiesInShield = new List<Enemy_stats>();

    void Start()
    {
        if (player == null)
        {
            Debug.LogError("Player reference is not assigned!");
            Destroy(gameObject);
            return;
        }

        magicCollider = GetComponent<CapsuleCollider>();
        duration = player.GetComponent<TDCombat>()?.coolDownTimerDuaration ?? 5f;
        timer = 0;

        player.GetComponent<PlayerStats>().godmode = true; // Activate godmode when shield starts

        // Start dealing damage every second
        StartCoroutine(DealDamageOverTime());
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (Input.GetMouseButtonDown(1) || timer >= duration)
        {
            StopMagic();
        }

        if (player != null)
        {
            transform.position = player.position + player.TransformDirection(offset);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("Enemy") || other.CompareTag("Flying_Enemy")) && other.TryGetComponent(out Enemy_stats enemy))
        {
            if (!enemiesInShield.Contains(enemy))
            {
                enemiesInShield.Add(enemy);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Enemy_stats enemy))
        {
            enemiesInShield.Remove(enemy);
        }
    }

    private IEnumerator DealDamageOverTime()
    {
        while (timer < duration)
        {
            yield return new WaitForSeconds(1f); // Deal damage every second

            for (int i = 0; i < enemiesInShield.Count; i++)
            {
                if (enemiesInShield[i] != null)
                {
                    enemiesInShield[i].GetDamage(damage);
                }
            }
        }
    }

    private void StopMagic()
    {
        player.GetComponent<PlayerStats>().godmode = false;
        enemiesInShield.Clear();
        Destroy(gameObject);
    }
}
=======
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ElectricShield : MonoBehaviour
{
    public float damage;
    public float duration;
    private float timer;

    public Transform player;
    public Vector3 offset;
    private CapsuleCollider magicCollider;
    private List<Enemy_stats> enemiesInShield = new List<Enemy_stats>();

    void Start()
    {
        if (player == null)
        {
            Debug.LogError("Player reference is not assigned!");
            Destroy(gameObject);
            return;
        }

        magicCollider = GetComponent<CapsuleCollider>();
        duration = player.GetComponent<TDCombat>()?.coolDownTimerDuaration ?? 5f;
        timer = 0;

        player.GetComponent<PlayerStats>().godmode = true; // Activate godmode when shield starts

        // Start dealing damage every second
        StartCoroutine(DealDamageOverTime());
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (Input.GetMouseButtonDown(1) || timer >= duration)
        {
            StopMagic();
        }

        if (player != null)
        {
            transform.position = player.position + player.TransformDirection(offset);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("Enemy") || other.CompareTag("Flying_Enemy")) && other.TryGetComponent(out Enemy_stats enemy))
        {
            if (!enemiesInShield.Contains(enemy))
            {
                enemiesInShield.Add(enemy);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Enemy_stats enemy))
        {
            enemiesInShield.Remove(enemy);
        }
    }

    private IEnumerator DealDamageOverTime()
    {
        while (timer < duration)
        {
            yield return new WaitForSeconds(1f); // Deal damage every second

            for (int i = 0; i < enemiesInShield.Count; i++)
            {
                if (enemiesInShield[i] != null)
                {
                    enemiesInShield[i].GetDamage(damage);
                }
            }
        }
    }

    private void StopMagic()
    {
        player.GetComponent<PlayerStats>().godmode = false;
        enemiesInShield.Clear();
        Destroy(gameObject);
    }
}
>>>>>>> 8341d68b8fd658505bbd1e276ebbe49078627311
