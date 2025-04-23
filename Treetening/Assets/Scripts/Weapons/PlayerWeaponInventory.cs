using UnityEngine;
using System.Collections.Generic;

public class PlayerWeaponInventory : MonoBehaviour
{
    public List<WeaponData> allWeaponData; //List with every weapon's WeaponDatas (populate manualy in the inspector)
    public List<WeaponInstance> ownedWeapons = new List<WeaponInstance>();

    public WeaponManager weaponManager;

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
        if(instance != null)
            instance.isUnlocked = true;
    }

    void Awake()
    {
        if(ownedWeapons.Count == 0)
        {
            Debug.LogWarning("No weapons available, unlocking Rocketgloves!"); //Debug
            ownedWeapons.Add(new WeaponInstance {weaponId = "rocket_gloves", isUnlocked = true});
            weaponManager.EquipWeapon(ownedWeapons[0].GetWeaponId());
        }
    }
}
