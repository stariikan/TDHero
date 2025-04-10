using UnityEngine;

public class SunStrike : MagicBase
{
    public float timerSpeed;
    public GameObject sunStrikeHit;
    private bool isStruck = false;

    protected override void Update()
    {
        timerSpeed *= 1 + (level / 10);
        base.Update();
        ShrinkAndStrike();
    }
    public void SetChargeTime(float timeSpeed)
    {
        timerSpeed = timeSpeed;
    }
    private void ShrinkAndStrike()
    {
        // Reduce scale over time
        transform.localScale -= Vector3.one * timerSpeed * Time.deltaTime;

        // Ensure scale does not go below a minimum value
        transform.localScale = Vector3.Max(transform.localScale, new Vector3(0.5f, 0.5f, 0.5f));

        // When fully shrunk, spawn the SunStrikeHit object
        if (transform.localScale == new Vector3(0.7f, 0.7f, 0.7f) && !isStruck && sunStrikeHit != null)
        {
            Instantiate(sunStrikeHit, new Vector3(transform.position.x, 50f, transform.position.z), Quaternion.Euler(90, 0, 0));
            isStruck = true;
            Destroy(gameObject, 0.3f);
        }
    }
    // Empty implementation to satisfy the abstract method requirement
    protected override void ApplyEffect(Collider other)
    {
        if (other.CompareTag(enemyTag) || other.CompareTag(enemyTag2))
        {
            Enemy_stats enemyStats = other.GetComponent<Enemy_stats>();
            if (enemyStats != null && isStruck == true)
            {
                enemyStats.GetDamage(damage);
            }
        }
    }
}
