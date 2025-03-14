using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningChain : MonoBehaviour
{
    public float lifeTime = 1.5f; // Time before the projectile is destroyed
    public float damage;         // Damage dealt by the projectile
    public string enemyTag;          // Tag to identify enemies
    public string enemyTag2;          // Tag to identify enemies
    private float timer;             // Timer to track lifetime
    private bool secondChain = false;
    private bool isFired = false;
    private int currentBounce; 
    public int maxBounce;          // Maximum number of bounces

    public Transform player;
    private Transform magicTranform;
    public GameObject lightningObject;
    public Vector3 offset;

    void Start()
    {
        timer = 0f;
        lifeTime = 1f;
        magicTranform = transform;
        isFired = false;
        if (currentBounce < 1) currentBounce = 0;
        if (maxBounce < 5) maxBounce = 5;
    }
    public void SetDamage(float dmg)
    {
        damage = dmg;
    }
    public void SecondChain()
    {
        secondChain = true;
    }
    public void SetCurrentBounce(int bounce) 
    {
        currentBounce = bounce;
    }
    public void SetMaxBounce (int maxBounceNumber)
    {
        maxBounce = maxBounceNumber;
    }
    private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag(enemyTag) || other.CompareTag(enemyTag2)) && isFired == false && currentBounce < maxBounce)
        {
            isFired = true;
            currentBounce += 1;
            Enemy_stats enemyStats = other.GetComponent<Enemy_stats>();
            if (enemyStats != null)
            {
                GameObject magic = null;
                enemyStats.GetDamage(damage);
                if (lightningObject != null)
                {
                    magic = Instantiate(lightningObject, enemyStats.transform.position, enemyStats.transform.rotation);
                    magic.GetComponent<LightningChain>().SecondChain();
                    magic.GetComponent<LightningChain>().SetCurrentBounce(currentBounce);
                    magic.GetComponent<LightningChain>().SetDamage(damage * 0.9f);
                    magic.transform.rotation = magic.transform.rotation * Quaternion.Euler(0, 180, 0);
                    magic.transform.position = magic.transform.position + (offset* -1) ;
                    magic.name = "LightningChain " + Random.Range(0, 100);
                    Destroy(gameObject, 0.3f);
                }
            }
        }
        else return;
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (Input.GetMouseButtonDown(1) || timer >= lifeTime)
        {
            Destroy(gameObject);
        }

        if (player != null && secondChain == false)
        {
            magicTranform.position = player.position + player.TransformDirection(offset);
            magicTranform.rotation = player.rotation * Quaternion.Euler(0, 0, 0);
        }
    }
}
