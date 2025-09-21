using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpBlood : MonoBehaviour
{
    private float healPower;
    public string playerTag;    // Choose enemy tag
    private float timer;
    public float totalDuration = 60f;
    private Collider zoneCollider;
    private Vector3 originalScale;

    void Start()
    {
        timer = totalDuration;
        originalScale = transform.localScale;
        healPower = 1f;
        zoneCollider = this.gameObject.GetComponent<BoxCollider>();
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            float scalePercent = timer / totalDuration; // from 1 to 0
            transform.localScale = originalScale * scalePercent;
        }
        if (timer < 0)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            PlayerStats pS = other.GetComponent<PlayerStats>();
            float playerMaxHp = pS.maxhp;
            float playerHp = pS.hp;
            if (pS != null)
            {
                if (playerHp < playerMaxHp) 
                {
                    pS.PlayerHealed(healPower);
                    Destroy(gameObject);
                }  
            }
        }
        else return;
    }
}
