using UnityEngine;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{
    [Header("Weapons")]
    public List<WeaponData> allWeaponData; //List with every weapon's WeaponDatas (populate manualy in the inspector)
    public List<WeaponInstance> ownedWeapons = new List<WeaponInstance>();
    public WeaponManager weaponManager;
    [SerializeField] private int weaponPrice;
    [SerializeField] private float weaponPriceMult;

    [Header("Money")]
    private int money;

    [Header("Seeds")]
    public List<SeedData> allSeedData; //List with every seed's SeedDatas (populate manualy in the inspector)
    private List<int> ownedSeeds = new List<int>();

    public WeaponInstance GetWeaponInstance(string weaponID)
    {
        return ownedWeapons.Find(w => w.weaponId == weaponID);
    }

    public WeaponData GetWeaponData(string weaponID)
    {
        return allWeaponData.Find(W => W.weaponId == weaponID);
    }

    public void UnlockWeapon(string weaponID)
    {
        ownedWeapons.Add(new WeaponInstance { weaponId = weaponID/*, isUnlocked = true*/});
    }

    public int getWeaponPrice()
    {
        return weaponPrice;
    }

    public void IncreaseWeaponPrice()
    {
        float newPrice = weaponPrice * weaponPriceMult;
        weaponPrice = (int)newPrice;
        Debug.Log($"Weapon price increased to {weaponPrice}$");
    }

    public int getMoney()
    {
        return money;
    }

    public void setMoney(int money)
    {
        this.money = money;
    }

    public List<int> getOwnedSeeds()
    {
        return ownedSeeds;
    }

    public void setOwnedSeed(string seedID, int qtt)
    {
        int index = allSeedData.FindIndex(seed => seed.seedID == seedID);
        ownedSeeds[index] = qtt;
    }

    public void incOwnedSeed(string seedID)
    {
        int index = allSeedData.FindIndex(seed => seed.seedID == seedID);
        ownedSeeds[index]++;
    }

    void Awake()
    {
        /*
        if (ownedWeapons.Count == 0)
        {
            Debug.LogWarning("No weapons available, unlocking Rocketgloves!"); //Debug
            ownedWeapons.Add(new WeaponInstance { weaponId = "rocket_gloves", isUnlocked = true });
            weaponManager.EquipWeapon(ownedWeapons[0].weaponId);
        }
        */

        foreach (SeedData seed in allSeedData)
        {
            ownedSeeds.Add(0);
        }

        //Debug
        ownedSeeds[0] = 3;
        ownedSeeds[1] = 7;
        money = 80;
    }
}
