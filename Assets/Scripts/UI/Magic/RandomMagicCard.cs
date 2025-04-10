using UnityEngine;

public class RandomMagicCard : MonoBehaviour
{
    public GameObject[] slots;
    public GameObject[] magicCards;
    public GameObject player;
    private int playerLevel;
    private bool playerFullOfMagic;

    private bool[] usedSlots = new bool[4];
    private GameObject[] usedSlotByCard = new GameObject[4];
    private int maxTries = 100;

    public void OpenLevelUpWindow() 
    {
        playerLevel = player.GetComponent<PlayerStats>().playerLevel;
        playerFullOfMagic = player.GetComponent<TDCombat>().playerSpentAllSlots;

        // Clear slot usage
        for (int i = 0; i < usedSlots.Length; i++) usedSlots[i] = false;
        for (int i = 0; i < usedSlotByCard.Length; i++) usedSlotByCard[i] = null;
        for (int i = 0; i < magicCards.Length; i++) magicCards[i].SetActive(false);

        if (playerLevel == 1) GenerateCards_FirstLevel();
        if (playerLevel > 1 && !playerFullOfMagic) GenerateMixedCards(2, 2);
        if (playerFullOfMagic) GenerateUpgradeCards(4);
    }
    void GenerateCards_FirstLevel()
    {
        Debug.Log("Generate Cards For First Level!");
        GenerateNewCards(4);
    }

    void GenerateMixedCards(int newCount, int upgradeCount)
    {
        Debug.Log("Generate Mixed Cards!");
        GenerateNewCards(newCount);
        GenerateUpgradeCards(upgradeCount);
    }

    void GenerateNewCards(int count)
    {
        int tries = 0;
        while (count > 0 && tries < maxTries)
        {
            int rand = Random.Range(0, magicCards.Length);
            var card = magicCards[rand].GetComponent<MagicCard>();
            card.OpenMagicCard();

            if (card.magicLevel < 1 && TryPlaceCard(rand))
            {
                Debug.Log("Generated New Card: " + magicCards[rand] + " Card Magic Level: "+ card.magicLevel);
                count--;
            }
            tries++;
        }
    }
    void GenerateUpgradeCards(int count)
    {
        int tries = 0;
        while (count > 0 && tries < maxTries)
        {
            int rand = Random.Range(0, magicCards.Length);
            var card = magicCards[rand].GetComponent<MagicCard>();
            card.OpenMagicCard();
            if (card.magicLevel >= 1 && TryPlaceCard(rand)) 
            {
                Debug.Log("Generated Upgraded Card: " + magicCards[rand] + " Card Magic Level: " + card.magicLevel);
                count--;
            }
            tries++;
        }
    }

    bool TryPlaceCard(int cardIndex)
    {
        for (int i = 0; i < usedSlots.Length; i++)
        {
            if (!usedSlots[i] && usedSlotByCard[0] != magicCards[cardIndex] && usedSlotByCard[1] != magicCards[cardIndex] && usedSlotByCard[2] != magicCards[cardIndex] && usedSlotByCard[3] != magicCards[cardIndex])
            {
                magicCards[cardIndex].transform.position = slots[i].transform.position;
                usedSlotByCard[i] = magicCards[cardIndex];
                magicCards[cardIndex].SetActive(true);
                magicCards[cardIndex].GetComponent<MagicCard>().OpenMagicCard();
                usedSlots[i] = true;
                return true;
            }
        }
        return false; // No free slot
    }
}
