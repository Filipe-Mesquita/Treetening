using UnityEngine;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{
    [Header("Weapons")]
    public List<WeaponData> allWeaponData; //List with every weapon's WeaponDatas (populate manualy in the inspector)
    public List<WeaponInstance> ownedWeapons = new List<WeaponInstance>();

    public WeaponManager weaponManager;


    private int money;
    public List<SeedData> allSeedData; //List with every seed's SeedDatas (populate manualy in the inspector)
    private List<int> ownedSeeds = new List<int>();

    public WeaponInstance GetWeaponInstance(string id)
    {
        return ownedWeapons.Find(w => w.weaponId == id);
    }

    public WeaponData GetWeaponData(string id)
    {
        return allWeaponData.Find(W => W.weaponId == id);
    }

    public void UnlockWeapon(string id)
    {
        WeaponInstance instance = GetWeaponInstance(id);
        if (instance != null)
            instance.isUnlocked = true;
    }

    public List<int> getOwnedSeeds()
    {
        return ownedSeeds;
    }

    public void setOwnedSeed(int seedID, int qtt)
    {
        ownedSeeds[seedID] = qtt;
    }

    void Awake()
    {
        if (ownedWeapons.Count == 0)
        {
            Debug.LogWarning("No weapons available, unlocking Rocketgloves!"); //Debug
            ownedWeapons.Add(new WeaponInstance { weaponId = "rocket_gloves", isUnlocked = true });
            weaponManager.EquipWeapon(ownedWeapons[0].GetWeaponId());
        }

        foreach (SeedData seed in allSeedData)
        {
            ownedSeeds.Add(0);
        }

        //Debug
        ownedSeeds[0] = 3;
        ownedSeeds[1] = 7;
    }
}
