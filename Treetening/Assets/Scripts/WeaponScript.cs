using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponScript : MonoBehaviour
{
    public List<Weapon> weaponLst = new List<Weapon>();
    [SerializeField] private int equippedWeaponId;
    [SerializeField] private float pushForce = 500f;

    [Header("Raycast settings")]
    [SerializeField] private float raycastDistance = 100f;
    [SerializeField] private LayerMask raycastLayers;
    private Camera mainCamera;

    void Start()
    {
        if (weaponLst.Count == 0)
        {
            weaponLst.Add(new Weapon(0, "AirShotgun", 5f, 6f));
        }
        equippedWeaponId = weaponLst[0].WeaponId;

        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Main camera not found! (WeaponScript)");
            return;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            shoot();

        float scroll = Input.GetAxisRaw("Mouse ScrollWheel");
        if (scroll < 0f)
        {
            SwitchToPreviousWeapon();
        }
        else if (scroll > 0f)
        {
            SwitchToNextWeapon();
        }
    }

    //----------------------------------------------
    //Use mouse scroll Wheel to switch equped weapon
    //----------------------------------------------
    private void SwitchToPreviousWeapon()
    {
        if (equippedWeaponId > 0)
            equippedWeaponId--;
        else
            equippedWeaponId = weaponLst.Count - 1;

        Debug.Log("D");             //Debug
    }
    private void SwitchToNextWeapon()
    {
        if (equippedWeaponId < weaponLst.Count - 1)
            equippedWeaponId++;
        else
            equippedWeaponId = 0;

        Debug.Log("U");             //Debug
    }

    //------------------------------------------------------------------------------------------------------------------------------------------------------------
    //Este modo de fazer o tiro é temporário, futuramente armas diferentes poderam utilizar tipos de hot detection diferentes e por isso esta lógica será alterada
    //-------------------------------------------------------------------------------------------------------------------------------------------------------------
    private void shoot()
    {
        if (weaponLst[equippedWeaponId].WeaponAmmunition <= 0)
        {
            Debug.Log($"No ammo in {weaponLst[equippedWeaponId].WeaponName}!");
            return;
        }

        Vector2 screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = mainCamera.ScreenPointToRay(screenCenter);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, raycastDistance, raycastLayers))
        {
            Debug.Log($"Hit: {hit.collider.gameObject.name} at {hit.distance}");

            if (hit.collider.CompareTag("Tree"))
            {
                TreeScript treeScript = hit.collider.gameObject.GetComponent<TreeScript>();
                if (treeScript != null)
                {
                    treeScript.UnfreezeTree();
                }
                else
                {
                    Debug.LogError("No treeScript in hit!");
                }

                Rigidbody treeRb = hit.collider.gameObject.GetComponent<Rigidbody>();
                if (treeRb != null)
                {
                    Vector3 forceDirection = mainCamera.transform.forward;
                    treeRb.AddForce(forceDirection * pushForce, ForceMode.Impulse);

                    bool hasRoot = treeScript.getHasRoot();
                    Debug.LogWarning("HasRoot = " + hasRoot); //debug

                    if (hasRoot)
                    {
                        RootScript rootScript = hit.collider.gameObject.GetComponentInChildren<RootScript>();
                        if (rootScript == null)
                            Debug.LogError("No rootScript in hit!");
                        else
                        {
                            rootScript.EnableRootCollider();
                            Debug.Log("Root Collider enabled!");
                        }
                        treeScript.setHassRoot(false);
                    }
                }
            }
        }
    }
}
