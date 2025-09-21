using UnityEngine;

public class LightningChain : MagicBase
{
    public int maxBounce = 4;
    private int currentBounce = 0;
    public GameObject lightningPrefab;
    private bool isDamaged = false;
    public Vector3 offset;
    protected override void Start()
    {
        this.transform.position = this.transform.position + offset;
        maxBounce += level;
    }
    protected override void ApplyEffect(Collider other)
    {
        if (other.TryGetComponent(out Enemy_stats enemy))
        {
            if (isDamaged == false && currentBounce <= maxBounce)
            {
                enemy.GetDamage(damage);
                currentBounce += 1;
                SpawnNextChain(enemy);
                isDamaged = true;
            }
        }
    }
    protected override void CancelEffect(Collider other)
    {
        if (other.TryGetComponent(out Enemy_stats enemy))
        {

        }
    }
    private void DecreaseChainDamage() 
    {
        damage = damage * 0.9f;
    }
    private void IncreaseCurrentBounce(int amount) 
    {
        currentBounce = amount;
    }
    private void SpawnNextChain(Enemy_stats target)
    {
        GameObject magic = Instantiate(lightningPrefab, target.transform.position, target.transform.rotation);
        magic.GetComponent<LightningChain>().DecreaseChainDamage();
        magic.GetComponent<LightningChain>().IncreaseCurrentBounce(currentBounce);
        Vector3 behindPosition = target.transform.position - target.transform.forward * offset.magnitude;
            
        magic.transform.rotation = magic.transform.rotation * Quaternion.Euler(0, 180, 0);
        magic.transform.position = behindPosition + offset;
        //magic.transform.position = magic.transform.position + offset;
        magic.name = "LightningChain " + currentBounce;
        Debug.Log(magic.name);
    }
}
