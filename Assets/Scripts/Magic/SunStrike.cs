using Unity.Burst.CompilerServices;
using UnityEngine;

public class SunStrike : MonoBehaviour
{
    public float timerSpeed; // Speed of the reducing scale of the object
    public float damage;         // Damage dealt by the projectile
    public string enemyTag;          // Tag to identify enemies
    public string enemyTag2;          // Tag to identify enemies
    private float timer;             // Timer to track lifetime
    public GameObject sunStrikeHit;
    private Transform transform;     
    private CapsuleCollider collider;
    bool isStruck;

    void Start()
    {
        timer = 0f;
        collider = this.gameObject.GetComponent<CapsuleCollider>();
        collider.enabled = false;
        transform = this.gameObject.transform;
        if (timerSpeed < 1 ) timerSpeed = 1f;
        isStruck = false;
        ExplosionMousePosition();
    }
    public void ExplosionMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 newPosition = hit.point;
            newPosition.y = 0.1f;  // Adjust Y position
            this.gameObject.transform.position = newPosition;  // Assign back to transform
        }

    }
    public void SetDamage(float dmg)
    {
        damage = dmg;
    }
    public void SetChargeTime(float timeSpeed)
    {
        timerSpeed = timeSpeed;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(enemyTag) || other.CompareTag(enemyTag2))
        {
            Enemy_stats enemyStats = other.GetComponent<Enemy_stats>();
            if (enemyStats != null)
            {
                enemyStats.GetDamage(damage);
            }
        }
        else return;
    }
    public void InviteSunStrike()
    {
        if (transform.localScale != new Vector3(0.5f, 0.5f, 0.5f))
        {
            transform.localScale -= new Vector3(timer, timer, timer) * Time.deltaTime;

            // Ensure the scale doesn't go below 0.3f
            transform.localScale = new Vector3(
                Mathf.Max(transform.localScale.x, 0.5f),
                Mathf.Max(transform.localScale.y, 0.5f),
                Mathf.Max(transform.localScale.z, 0.5f)
            );
        }
        if (transform.localScale == new Vector3(0.5f, 0.5f, 0.5f) && sunStrikeHit != null && isStruck == false)
        {
            GameObject magic = null;
            string magicName = "";
            Vector3 newPosition = transform.position;
            newPosition.y = 50f;  // Adjust Y position
            magic = Instantiate(sunStrikeHit, newPosition, transform.rotation);
            magicName = "SunStrikeHit";
            magic.transform.rotation = magic.transform.rotation * Quaternion.Euler(90, 0, 0);
            collider.enabled = true;
            isStruck = true;
            Destroy(gameObject, 0.3f);
        }
    }
    void Update()
    {
        timer += Time.deltaTime * timerSpeed;
        InviteSunStrike();

    }
}