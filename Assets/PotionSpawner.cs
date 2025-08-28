using System.Collections;
using UnityEngine;

public class PotionSpawner : MonoBehaviour
{
    public GameObject knightPotionPrefab;
    public GameObject wizardPotionPrefab;
    public Vector2 spawnAreaMin;
    public Vector2 spawnAreaMax;

    private Attack Attack;

    void Start()
    {
        Attack = FindObjectOfType<Attack>();
        StartCoroutine(PotionCycle());
    }

    IEnumerator PotionCycle()
    {
        while (true)
        {
            
            yield return new WaitForSeconds(2f);

            Vector2 spawnPos = new Vector2(
                Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                Random.Range(spawnAreaMin.y, spawnAreaMax.y)
            );

            GameObject chosenPotion = Random.value > 0.5f ? knightPotionPrefab : wizardPotionPrefab;
            Instantiate(chosenPotion, spawnPos, Quaternion.identity);

           
            yield return new WaitForSeconds(10f);

            
            if (Attack != null)
            {
                Attack.ResetToDefault();
                
            }

        }
    }
}
