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

    [Header("General")]
    [SerializeField] PlayerInventory inventoryScript;

    [Header("Seed button functionality")]
    [SerializeField] List<SeedData> allSeedDatas;
    [SerializeField] UnityEngine.UI.Image seedSprite;
    [SerializeField] TextMeshProUGUI seedNameTxt;
    [SerializeField] TextMeshProUGUI growthTimeTxt;
    [SerializeField] TextMeshProUGUI rootValueTxt;
    [SerializeField] TextMeshProUGUI priceTxt;
    [SerializeField] TextMeshProUGUI ownedQttTxt;

    [Header("Weapon button functionality")]
    [SerializeField] List<WeaponData> allWeaponDatas;
    [SerializeField] UnityEngine.UI.Image lockedWeaponSprite;
    [SerializeField] UnityEngine.UI.Image unlockedWeaponSprite;
    [SerializeField] TextMeshProUGUI lockedWeaponNameTxt;
    [SerializeField] TextMeshProUGUI unlockedWeaponNameTxt;
    [SerializeField] TextMeshProUGUI hability1Txt;
    [SerializeField] TextMeshProUGUI hability2Txt;

    [Header("Every Info Panel")]
    [SerializeField] GameObject seedInfo;
    [SerializeField] GameObject lockedWeaponInfo;
    [SerializeField] GameObject unlockedWeaponInfo;

    public void closeShop()
    {
        interactScript.handleShop(shopCollider);
    }

    public void openSeedInfo(string ID)
    {
        desableAllInfos();
        if (seedInfo != null) seedInfo.SetActive(true);


        //Filters allSeedDatas to obtain the correct seedData
        SeedData seedData = allSeedDatas.First(seed => seed.seedID == ID);

        //Set Seed Sprite
        seedSprite.sprite = seedData.seedSprite;

        //Set Seed Name
        seedNameTxt.text = seedData.seedName;

        //Set Growth Time
        growthTimeTxt.text = $"Growth Time: {seedData.growthTime}s";

        //Set Root Value
        rootValueTxt.text = $"Root Value: {seedData.treePrefab.GetComponent<TreeScript>().getRootValue()}$";

        //Set Price
        // To be implemented ---------------------------------------------------------------------------

        //Set Owned Qtt
        int index = allSeedDatas.FindIndex(seed => seed.seedID == ID);
        List<int> inventorySeeds = inventoryScript.getOwnedSeeds();
        ownedQttTxt.text = $"Owned Seeds: {inventorySeeds[index]}";
    }

    public void openWeaponInfo(string ID)
    {
        //Filters allWeaponDatas to obtain the correct weaponData
        WeaponData weaponData = allWeaponDatas.First(weapon => weapon.weaponId == ID);

        desableAllInfos();
        //If the weapon is in the ownedWeapons List in the inventory then it is unlocked
        if (inventoryScript.GetWeaponInstance(ID) != null)
        {
            if (unlockedWeaponInfo != null) unlockedWeaponInfo.SetActive(true);


            //Set Weapon Sprite
            unlockedWeaponSprite.sprite = weaponData.weaponSprite;

            //Set Weapon Name
            unlockedWeaponNameTxt.text = weaponData.weaponName;

            //Set Price
            // To be implemented ---------------------------------------------------------------------------

            //Set hability 1
            hability1Txt.text = weaponData.hability1Name;

            //Set hability 2
            hability2Txt.text = weaponData.hability2Name;
        }
        //If its not found, then its locked
        else
        {
            if (lockedWeaponInfo != null) lockedWeaponInfo.SetActive(true);


            //Set Weapon Sprite
            lockedWeaponSprite.sprite = weaponData.weaponSprite;

            //Set Weapon Name
            lockedWeaponNameTxt.text = weaponData.weaponName;

            //Set Price
            // To be implemented ---------------------------------------------------------------------------
        }
    }



    private void desableAllInfos()
    {
        if (seedInfo != null) seedInfo.SetActive(false);
        if (lockedWeaponInfo != null) lockedWeaponInfo.SetActive(false);
        if (unlockedWeaponInfo != null) unlockedWeaponInfo.SetActive(false);
    }
}
