using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class MagicBase : MonoBehaviour
{
    public float damage;
    public float lifeTime = 5f;
    public string enemyTag = "Enemy";
    public string enemyTag2 = "Flying_Enemy";
    public int level = 0;
    public bool onCoursor;
    protected float timer = 0f;

    protected virtual void Start()
    {
        if (onCoursor == true) ExplosionMousePosition();
    }

    protected virtual void Update()
    {
        timer += Time.deltaTime;
        if (timer >= lifeTime)
        {
            OnMagicEnd();
            Destroy(gameObject);
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(enemyTag) || other.CompareTag(enemyTag2))
        {
            ApplyEffect(other);
        }
    }
    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(enemyTag) || other.CompareTag(enemyTag2))
        {
            CancelEffect(other);
        }
    }

    protected abstract void ApplyEffect(Collider enemy); // Force each magic to define its effect.
    protected abstract void CancelEffect(Collider enemy); // Force each magic to define its effect.
    public void SetDamage(float dmg)
    {
        damage = dmg;
    }
    public void IncreaseMagicLevel() 
    {
        level += 1;
        damage *= 1 + (level / 10);
    }
    protected virtual void OnMagicEnd() { }

    protected void ExplosionMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray, Mathf.Infinity);
        foreach (RaycastHit hit in hits)
        {
            //Debug.Log("Hit: " + hit.collider.gameObject.name + " on layer " + LayerMask.LayerToName(hit.collider.gameObject.layer));

            // Check if the hit object is a tile
            if (hit.collider.gameObject.CompareTag("Tile") || hit.collider.gameObject.CompareTag("Ground"))
            {
                transform.position = new Vector3(hit.point.x, 0.1f, hit.point.z);
                break; // Stop looking once we find a tile
            }
        }
    }
}
