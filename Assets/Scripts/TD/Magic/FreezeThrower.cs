using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FreezeThrower : MonoBehaviour
{
    public float damage;
    public float freezingPower;
    public float duration;
    private float timer;

    public Transform player;
    private Transform magicTranform;
    public Vector3 offset;
    private List<Enemy_stats> enemiesInRange = new List<Enemy_stats>();

    void Start()
    {
        if (player == null)
        {
            Debug.LogError("Player reference is not assigned!");
            Destroy(gameObject);
            return;
        }
        magicTranform = GetComponent<Transform>();
        duration = player.GetComponent<TDCombat>()?.coolDownTimerDuaration ?? 5f;
        timer = 0;

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
            magicTranform.position = player.position + player.TransformDirection(offset);
            magicTranform.rotation = player.rotation * Quaternion.Euler(0, -90, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("Enemy") || other.CompareTag("Flying_Enemy")) && other.TryGetComponent(out Enemy_stats enemy))
        {
            if (!enemiesInRange.Contains(enemy))
            {
                enemiesInRange.Add(enemy);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Enemy_stats enemy))
        {
            enemiesInRange.Remove(enemy);
        }
    }

    private IEnumerator DealDamageOverTime()
    {
        while (timer < duration)
        {
            yield return new WaitForSeconds(1f);

            for (int i = 0; i < enemiesInRange.Count; i++)
            {
                if (enemiesInRange[i] != null)
                {
                    enemiesInRange[i].ReduceSpeed(freezingPower);
                    enemiesInRange[i].GetDamage(damage);
                }
            }
        }
    }

    private void StopMagic()
    {
        enemiesInRange.Clear();
        Destroy(gameObject);
    }
}