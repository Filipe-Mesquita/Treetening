using UnityEngine;

public class rayCastScript : MonoBehaviour
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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Explode();
        }
    }

    void Explode()
    {
        Vector3 explosionPoint = playerPos.position + Camera.main.transform.forward * 1.5f;
        Debug.Log($"playerPos.position: {playerPos.position}");
        Collider[] hits = Physics.OverlapSphere(explosionPoint, hitAcceptanceRadius, hitLayers);

        // Visualize explosion point and radius
        VisualizeExplosion(explosionPoint);

        foreach (Collider hit in hits)
        {
            Debug.Log($"Hit: {hit.gameObject.name}");
            // Apply damage, spawn effects, etc.
        }
    }

    void VisualizeExplosion(Vector3 explosionPoint)
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
