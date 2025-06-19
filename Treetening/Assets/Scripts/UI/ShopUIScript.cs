using System.Collections.Generic;
using System.Linq;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopUIScript : MonoBehaviour
{
    [Header("Close shop functionality")]
    [SerializeField] Collider shopCollider;

    [Header("Scripts")]
    [SerializeField] InteractScript interactScript;
    [SerializeField] PlayerInventory inventoryScript;
    [SerializeField] WeaponManager weaponManager;
    [SerializeField] BuyInfoScript buyInfoScript;

    [Header("Money")]
    [SerializeField] TextMeshProUGUI moneyTxt;

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
    [SerializeField] TextMeshProUGUI lockedWeaponPriceTxt;
    [SerializeField] TextMeshProUGUI unlockedWeaponPriceTxt;
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

    public void UpdateMoney()
    {
        moneyTxt.text = $"MONEY: {inventoryScript.getMoney()}$";
    }

    public void openSeedInfo(string seedID)
    {
        desableAllInfos();
        if (seedInfo != null) seedInfo.SetActive(true);

        //Set item info in case the player buys the item
        buyInfoScript.setItemID(seedID);

        //Filters allSeedDatas to obtain the correct seedData
        SeedData seedData = allSeedDatas.First(seed => seed.seedID == seedID);

        //Set Seed Sprite
        seedSprite.sprite = seedData.seedSprite;

        //Set Seed Name
        seedNameTxt.text = seedData.seedName;

        //Set Growth Time
        growthTimeTxt.text = $"Growth Time: {seedData.growthTime}s";

        //Set Root Value
        rootValueTxt.text = $"Root Value: {seedData.treePrefab.GetComponent<TreeScript>().getRootValue()}$";

        //Set Price
        priceTxt.text = $"Seed Price: {seedData.seedPrice}$";

        //Set Owned Qtt
        int index = allSeedDatas.FindIndex(seed => seed.seedID == seedID);
        List<int> inventorySeeds = inventoryScript.getOwnedSeeds();
        ownedQttTxt.text = $"Owned Seeds: {inventorySeeds[index]}";
    }

    public void openWeaponInfo(string weaponID)
    {
        //Filters allWeaponDatas to obtain the correct weaponData
        WeaponData weaponData = allWeaponDatas.First(weapon => weapon.weaponId == weaponID);

        //Set item info in case the player buys the item
        buyInfoScript.setItemID(weaponID);

        desableAllInfos();
        //If the weapon is in the ownedWeapons List in the inventory then it is unlocked
        if (inventoryScript.GetWeaponInstance(weaponID) != null)
        {
            if (unlockedWeaponInfo != null) unlockedWeaponInfo.SetActive(true);


            //Set Weapon Sprite
            unlockedWeaponSprite.sprite = weaponData.weaponSprite;

            //Set Weapon Name
            unlockedWeaponNameTxt.text = weaponData.weaponName;

            //Set Price
            unlockedWeaponPriceTxt.text = $"Upgrade Price: {inventoryScript.getWeaponPrice()}$";

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
            lockedWeaponPriceTxt.text = $"Unlock Price: {inventoryScript.getWeaponPrice()}$";
        }
    }



    public void desableAllInfos()
    {
        if (seedInfo != null) seedInfo.SetActive(false);
        if (lockedWeaponInfo != null) lockedWeaponInfo.SetActive(false);
        if (unlockedWeaponInfo != null) unlockedWeaponInfo.SetActive(false);
    }

    public void buySeed()
    {
        //Gets seedID from buyInfo
        string seedID = buyInfoScript.getItemID();

        //Gets player's money
        int money = inventoryScript.getMoney();
        //Gets seed's price
        SeedData seed = allSeedDatas.Find(seed => seed.seedID == seedID);
        int price = seed.seedPrice;

        //calculates the money after buying
        int newMoney = money - price;
        if (newMoney >= 0)  //If the player has enough money
        {
            inventoryScript.setMoney(newMoney); //Updates player's money
            inventoryScript.incOwnedSeed(seedID);   //Updates player's owned seeds
            UpdateMoney();  //Updates the money in the shop UI
            openSeedInfo(seedID);   //Updates the seed number in the shop UI
        }
        else
        {
            Debug.Log("Not enough money to buy seeds!");
        }

    }

    public void buyUpgrade(int habNumber)
    {
        //Gets player's money
        int money = inventoryScript.getMoney();

        //Gets upgrade price
        int price = inventoryScript.getWeaponPrice();

        //calculates the money after buying
        int newMoney = money - price;

        if (newMoney >= 0)  //If the player has enough money
        {
            //Gets weaponID from buyInfo
            string weaponID = buyInfoScript.getItemID();

            WeaponInstance weaponInstance = inventoryScript.GetWeaponInstance(weaponID);    //Updates weaponAttribute level
            if (habNumber == 1)
                weaponInstance.attribute1Level++;
            else
                weaponInstance.attribute2Level++;

            inventoryScript.IncreaseWeaponPrice();  //Increases the price for buying new weapons and upgrades
            inventoryScript.setMoney(newMoney); //Updates player's money
            UpdateMoney();  //Updates the money in the shop UI
            openWeaponInfo(weaponID);   //Updates the upgrade price in the shop UI
        }
        else
        {
            Debug.Log("Not enough money to buy the upgrade!");
        }
    }

    public void unlockWeapon()
    {
        //Gets player's money
        int money = inventoryScript.getMoney();

        //Gets unlock price
        int price = inventoryScript.getWeaponPrice();

        //calculates the money after buying
        int newMoney = money - price;

        if (newMoney >= 0)  //If the player has enough money
        {
            //Gets weaponID from buyInfo
            string weaponID = buyInfoScript.getItemID();

            inventoryScript.UnlockWeapon(weaponID); //Unlocks the new weapon in the inventory
            weaponManager.EquipWeapon(weaponID);    //Equips the new weapon

            inventoryScript.IncreaseWeaponPrice();  //Increases the price for buying new weapons and upgrades
            inventoryScript.setMoney(newMoney); //Updates player's money
            UpdateMoney();  //Updates the money in the shop UI
            openWeaponInfo(weaponID);   //Updates the upgrade price in the shop UI
        }
    }
}
