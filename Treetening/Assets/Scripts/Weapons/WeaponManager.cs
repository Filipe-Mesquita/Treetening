using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public Transform weaponSlot;
    public PlayerInventory inventory;
    public WeaponBehaviour currentWeapon;

    public void EquipWeapon(string weaponId)
    {
        //Removes the old weapon
        if (currentWeapon != null)
        {
            Destroy(currentWeapon.gameObject);
            currentWeapon = null;
        }

        //Gets new weapon's data and instance
        WeaponData data = inventory.GetWeaponData(weaponId);
        WeaponInstance instance = inventory.GetWeaponInstance(weaponId);

        if (data == null || instance == null || !instance.isUnlocked)
        {
            Debug.LogWarning("Weapon not found or unlocked");
            return;
        }

        //Instantiates the new weapon
        GameObject weaponGO = Instantiate(data.weaponPrefab, weaponSlot);
        currentWeapon = weaponGO.GetComponent<WeaponBehaviour>();

        if (currentWeapon != null)
            currentWeapon.Initialize(instance, data);
        else
            Debug.LogError("Prefab without WeaponBehaviour");
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && currentWeapon != null)
            currentWeapon.shoot();
    }
}
