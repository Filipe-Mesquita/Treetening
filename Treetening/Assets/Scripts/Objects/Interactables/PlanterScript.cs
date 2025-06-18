using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class PlanterScript : MonoBehaviour
{
    public List<SeedData> allSeedData; //List with every seed's SeedDatas (populate manualy in the inspector)
    public GameObject groundObject; //Object where the seeds will be planted
    //public float plantCooldown; //Cooldown between each seed is planted

    private List<int> availableSeeds = new List<int>(); //List of each seed quantity available inside the planter. The seed's ID corresponds to it index inside the list
    private Bounds groundBounds;

    //private float nextPlantTime = 0f;
    private bool isPlanting = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Initialize the availableSeeds list with the number of different seeds available
        foreach (SeedData seed in allSeedData)
        {
            availableSeeds.Add(0);
        }

        // Get the bounds of the object where the trees will be planted
        if (groundObject != null)
        {
            Collider col = groundObject.GetComponent<Collider>();
            if (col != null)
            {
                groundBounds = col.bounds;
            }
            else
            {
                Debug.LogError("No collider in Ground object!");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlanting)
        {
            for (int i = 0; i < availableSeeds.Count; i++)
            {
                if (availableSeeds[i] > 0)
                {
                    StartCoroutine(PlantSeed(i));
                    break;
                }
            }
        }
    }

    public int GetSeedQuantity(string seedID)
    {
        int index = allSeedData.FindIndex(seed => seed.seedID == seedID);
        return availableSeeds[index];
    }

    public void addSeeds(string seedID, int seedQTT)
    {
        int index = allSeedData.FindIndex(seed => seed.seedID == seedID);
        availableSeeds[index] = availableSeeds[index] + seedQTT;
        updateCard(index, availableSeeds[index]);
    }

    public void updateCard(int ID, int newQtt)
    {
        Transform card;
        switch (ID)
        {
            case 0:
                card = transform.Find("SeedsCanvas/SeedCardContainer/MahoganyCard/SeedQtt");
                break;
            case 1:
                card = transform.Find("SeedsCanvas/SeedCardContainer/PineCard/SeedQtt");
                break;
            default:
                card = transform.Find("SeedsCanvas/SeedCardContainer/MahoganyCard/SeedQtt");
                break;
        }

        if (card != null)
        {
            TextMeshProUGUI cardText = card.GetComponent<TextMeshProUGUI>();
            cardText.text = newQtt.ToString();
        }
    }


    IEnumerator PlantSeed(int index)
    {
        isPlanting = true;

        float waitTime = allSeedData[index].growthTime;
        Debug.Log($"Waiting for {waitTime} seconds");
        yield return new WaitForSeconds(waitTime);

        TryPlantSeed(index);

        isPlanting = false;
    }
    public void TryPlantSeed(int index)
    {
        if (availableSeeds[index] > 0)
        {

            GameObject treePrefab = allSeedData[index].treePrefab;

            // get a random position inside the ground bounds
            Vector3 randomPos = new Vector3(
                Random.Range(groundBounds.min.x, groundBounds.max.x),
                groundBounds.max.y + 5f, // lançar o raycast de cima
                Random.Range(groundBounds.min.z, groundBounds.max.z)
            );

            RaycastHit hit;
            if (Physics.Raycast(randomPos, Vector3.down, out hit, 10f))
            {
                Instantiate(treePrefab, hit.point, Quaternion.identity);
                Debug.Log($"Seed {index} planted at {hit.point}");

                availableSeeds[index]--;
                updateCard(index, availableSeeds[index]);
            }
            else
            {
                Debug.LogWarning("No ground available for planting was found!");
            }
        }
        else
        {
            Debug.Log("No seeds available to plant!");
        }
    }

}
