using Unity.VisualScripting;
using UnityEngine;

public class InteractScript : MonoBehaviour
{
    [SerializeField] private float hitAcceptanceRadius;
    [SerializeField] private float hitDamage;
    [SerializeField] private LayerMask hitLayers;
    [SerializeField] private Transform playerPos;

    [Header("Debug Visualization")]
    [Tooltip("Material for the debug sphere (optional)")]
    public Material debugSphereMaterial;
    [Tooltip("Duration to show the debug sphere (seconds)")]
    public float debugSphereDuration = 2f;
    [Tooltip("Debug sphere?")]
    [SerializeField] private bool debug;

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
        Debug.Log($"playerPos.position: {playerPos.position}");
        Collider[] hits = Physics.OverlapSphere(explosionPoint, hitAcceptanceRadius, hitLayers);

        // Visualize explosion point and radius (Debug purposes)
        if(debug)
            VisualizeDetection(explosionPoint);

        foreach (Collider hit in hits)
        {
            Debug.Log($"Hit: {hit.gameObject.name}");       //prints the name of the gameObjects that were in the hit radius (Debug purposes)

            // If a root was detected
            if (hit.CompareTag("Root"))
            {
                //destroyRoot(hit.gameObject);
                hit.gameObject.GetComponent<RootScript>().DestroyRoot();
            }
            // If the plnter is detected
            if (hit.CompareTag("Planter"))
            {
                hit.gameObject.GetComponent<PlanterScript>().addSeeds(0, 2);  //Debug, na versão final os valores do ID e da quantidade devem ser passados conforme o que o player tiver equipado
            }
            /*
            Aqui ficarão o resto das interações
            *fazer um if para cada caso*
            */
        }
    }

    //  Creates a sphere taht represents the area affected by the hit detection (Debug purposes)
    void VisualizeDetection(Vector3 explosionPoint)
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
