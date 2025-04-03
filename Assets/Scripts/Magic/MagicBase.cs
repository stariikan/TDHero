using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class MagicBase : MonoBehaviour
{
    public float damage;
    public float lifeTime = 5f;
    public string enemyTag = "Enemy";
    public string enemyTag2 = "Flying_Enemy";
    protected float timer = 0f;

    protected virtual void Start()
    {
        ExplosionMousePosition();
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

    protected abstract void ApplyEffect(Collider enemy); // Force each magic to define its effect.
    public void SetDamage(float dmg)
    {
        damage = dmg;
    }
    protected virtual void OnMagicEnd() { }

    protected void ExplosionMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            transform.position = new Vector3(hit.point.x, 0.1f, hit.point.z);
        }
    }
}
