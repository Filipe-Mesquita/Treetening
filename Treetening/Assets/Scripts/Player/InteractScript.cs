using System;
using System.Collections.Generic;
using System.Diagnostics;
using Cinemachine;
using StarterAssets;
using Unity.VisualScripting;
using UnityEngine;

public class InteractScript : MonoBehaviour
{
    [SerializeField] private float hitAcceptanceRadius;
    [SerializeField] private LayerMask hitLayers;
    [SerializeField] private Transform playerPos;

    [Header("User Interface")]
    [SerializeField] private GameObject UI;
    [SerializeField] private GameObject PlayerCameraRoot;
    [SerializeField] private GameObject PlayerFollowCamera;


    [Header("Debug Visualization")]
    [Tooltip("Material for the debug sphere (optional)")]
    public Material debugSphereMaterial;
    [Tooltip("Duration to show the debug sphere (seconds)")]
    public float debugSphereDuration = 2f;
    [Tooltip("Debug sphere?")]
    [SerializeField] private bool debug;

    [Header("External Scripts")]
    [SerializeField] private PlayerInventory inventory;

    private bool isShoping = false; // Controls of the game should show or hide shopUi

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            Detect();
    }

    // Creates a sphere that detects gameObjects inside it and hands them properly
    private void Detect()
    {
        Vector3 explosionPoint = playerPos.position + Camera.main.transform.forward * 1f;
        UnityEngine.Debug.Log($"playerPos.position: {playerPos.position}");
        Collider[] hits = Physics.OverlapSphere(explosionPoint, hitAcceptanceRadius, hitLayers);

        // Visualize explosion point and radius (Debug purposes)
        if (debug)
            VisualizeDetection(explosionPoint);

        foreach (Collider hit in hits)
        {
            UnityEngine.Debug.Log($"Hit: {hit.gameObject.name}");       //prints the name of the gameObjects that were in the hit radius (Debug purposes)

            switch (hit.tag)
            {
                // If a root was detected
                case "Root":
                    handleRoot(hit);
                    break;

                // If the plnter is detected
                case "Planter":
                    handlePlanter(hit);
                    break;

                //If the shop is detected
                case "Shop":
                    handleShop(hit);
                    break;

                /*
                Aqui ficarão o resto das interações
                *fazer um case para cada caso*
                */

                default:
                    UnityEngine.Debug.LogWarning("No case found for this interacin!");
                    break;


            }
        }
    }


    // Functions that handle the detected object
    private void handleRoot(Collider hit)
    {
        //destroyRoot(hit.gameObject);
        hit.gameObject.GetComponent<RootScript>().DestroyRoot();
    }

    private void handlePlanter(Collider hit)
    {
        if (inventory != null)
        {
            // Gets owned seeds form inventory
            List<int> ownedSeeds = inventory.getOwnedSeeds();
            // Gets PLanterScript from planter
            PlanterScript pScirpt = hit.gameObject.GetComponent<PlanterScript>();
            if (pScirpt != null)
            {

                // Adds seeds to the planter and sets them to 0 in the inventory
                for (int i = 0; i < ownedSeeds.Count; i++)
                {
                    pScirpt.addSeeds(i, ownedSeeds[i]);
                    inventory.setOwnedSeed(i, 0);
                }
            }
            else
                UnityEngine.Debug.LogError("No PlanterScript found on hit!");
        }
        else
            UnityEngine.Debug.LogError("No PlayerInventory associated to the interactScript!");
    }

    public void handleShop(Collider hit)    // This one is public so the shopUIScript can access it
    {
        // If the shopUI is not enabled
        if (!isShoping)
        {
            isShoping = true;

            // Desable HUD and enable ShopUI
            if (UI != null)
            {
                Transform shopUI = UI.transform.Find("ShopUI");
                Transform hud = UI.transform.Find("HUD");

                if (shopUI != null) shopUI.gameObject.SetActive(true);
                if (hud != null) hud.gameObject.SetActive(false);
            }
            else
            {
                UnityEngine.Debug.LogWarning("UI Root not associated to the InteractScript!");
            }

            //Show the mouse cursor
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            // Desable player and camera movement
            FirstPersonController fpController = GetComponentInChildren<FirstPersonController>();
            fpController.enabled = false;
        }
        else
        {
            isShoping = false;

            // Enable HUd and desable ShopUI
            if (UI != null)
            {
                Transform shopUI = UI.transform.Find("ShopUI");
                Transform hud = UI.transform.Find("HUD");

                if (shopUI != null) shopUI.gameObject.SetActive(false);
                if (hud != null) hud.gameObject.SetActive(true);
            }
            else
            {
                UnityEngine.Debug.LogWarning("UI Root não está atribuído ao InteractScript!");
            }

            // Hide the mouse cursor
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            // Enable player and camera movement
            FirstPersonController fpController = GetComponentInChildren<FirstPersonController>();
            fpController.enabled = true;
        }
    }

/*----------------------------------------------//-----------------------------------------------*/
    //  Creates a sphere taht represents the area affected by the hit detection (Debug purposes)
    private void VisualizeDetection(Vector3 explosionPoint)
    {
        // Create a sphere primitive for visualization
        GameObject debugSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        debugSphere.transform.position = explosionPoint;

        // Scale the sphere to match the hit acceptance radius (diameter = 2 * radius)
        debugSphere.transform.localScale = Vector3.one * hitAcceptanceRadius * 2f;

        // Remove collider to prevent physics interactions
        Destroy(debugSphere.GetComponent<SphereCollider>());

        // Apply debug material for visibility (optional)
        if (debugSphereMaterial != null)
        {
            Renderer renderer = debugSphere.GetComponent<Renderer>();
            renderer.material = debugSphereMaterial;
        }
        else
        {
            // Default to a semi-transparent red material for visibility
            Renderer renderer = debugSphere.GetComponent<Renderer>();
            renderer.material = new Material(Shader.Find("Standard"));
            renderer.material.color = new Color(1f, 0f, 0f, 0.5f); // Semi-transparent red
        }

        // Destroy the sphere after a set duration
        Destroy(debugSphere, debugSphereDuration);
    }
}
