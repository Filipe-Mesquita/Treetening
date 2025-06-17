using System.Collections.Generic;
using System.Linq;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopUIScript : MonoBehaviour
{
    [Header("Close shop functionality")]
    [SerializeField] InteractScript interactScript;
    [SerializeField] Collider shopCollider;

    [Header("Seed button functionality")]
    [SerializeField] List<SeedData> allSeedDatas;
    [SerializeField] PlayerInventory inventoryScript;
    [SerializeField] UnityEngine.UI.Image seedSprite;
    [SerializeField] TextMeshProUGUI nameTxt;
    [SerializeField] TextMeshProUGUI growthTimeTxt;
    [SerializeField] TextMeshProUGUI rootValueTxt;
    [SerializeField] TextMeshProUGUI priceTxt;
    [SerializeField] TextMeshProUGUI ownedQttTxt;

    [Header("Every Info Panel")]
    [SerializeField] GameObject seedInfo;
    [SerializeField] GameObject lockedWeaponInfo;
    [SerializeField] GameObject unlockedWeaponInfo;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void closeShop()
    {
        interactScript.handleShop(shopCollider);
    }
    public void openSeedInfo(int ID)
    {
        desableAllInfos();
        if (seedInfo != null) seedInfo.SetActive(true);


        //Filters allSeedDatas to obtain the correct seedData
        SeedData seedData = allSeedDatas.First(seed => seed.seedID == ID);

        //Set Sprite
        seedSprite.sprite = seedData.seedSprite;

        //Set Seed Name
        nameTxt.text = seedData.seedName;

        //Set Growth Time
        growthTimeTxt.text = $"Growth Time: {seedData.growthTime}s";

        //Set Root Value
        rootValueTxt.text = $"Root Value: {seedData.treePrefab.GetComponent<TreeScript>().getRootValue()}$";

        //Set Price
        // To be implemented ---------------------------------------------------------------------------

        //Set Owned Qtt
        List<int> inventorySeeds = inventoryScript.getOwnedSeeds();
        ownedQttTxt.text = $"Owned Seeds: {inventorySeeds[ID]}";
    }



    private void desableAllInfos()
    {
        if (seedInfo != null) seedInfo.SetActive(false);
        if (lockedWeaponInfo != null) lockedWeaponInfo.SetActive(false);
        if (unlockedWeaponInfo != null) unlockedWeaponInfo.SetActive(false);
    }
}
