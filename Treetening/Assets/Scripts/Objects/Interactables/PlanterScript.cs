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

    public int GetSeedQuantity(int ID)
    {
        return availableSeeds[ID];
    }

    public void addSeeds(int seedID, int seedQTT)
    {
        availableSeeds[seedID] = availableSeeds[seedID] + seedQTT;
        updateCard(seedID, availableSeeds[seedID]);
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


    IEnumerator PlantSeed(int seedID)
    {
        isPlanting = true;

        float waitTime = allSeedData[seedID].growthTime;
        Debug.Log($"Waiting for {waitTime} seconds");
        yield return new WaitForSeconds(waitTime);

        TryPlantSeed(seedID);

        isPlanting = false;
    }
    public void TryPlantSeed(int seedID)
    {
        if (availableSeeds[seedID] > 0)
        {

            GameObject treePrefab = allSeedData[seedID].treePrefab;

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
                Debug.Log($"Seed {seedID} planted at {hit.point}");

                availableSeeds[seedID]--;
                updateCard(seedID, availableSeeds[seedID]);
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
