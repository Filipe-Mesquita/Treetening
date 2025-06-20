using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public Transform weaponSlot;
    public PlayerInventory inventory;
    public WeaponBehaviour currentWeapon;

    [SerializeField] InteractScript interactScript;

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

        if (data == null || instance == null)
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
            if (!interactScript.getIsShoping())
                currentWeapon.shoot();

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            string nextWeaponID = inventory.nextWeapon(currentWeapon.data.weaponId);
            EquipWeapon(nextWeaponID);
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            string previousWeaponID = inventory.previousWeapon(currentWeapon.data.weaponId);
            EquipWeapon(previousWeaponID);
        }
    }
}
