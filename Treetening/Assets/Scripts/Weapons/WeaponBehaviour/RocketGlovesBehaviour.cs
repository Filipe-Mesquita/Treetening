using System.Collections;
using UnityEngine;

public class RocketGlovesBehaviour : WeaponBehaviour
{
    public float range = 3f;
    public float radius = 1.3f;
    public LayerMask hitLayer;

    [Tooltip("Value of the force applied to the tree when it dies")]
    [SerializeField] private float pushForce = 500f;

    [Tooltip("Time it takes to activate a root collider once the tree dies")]
    [SerializeField] private float timeToActivateRoots = 1.5f;

    [Header("Animation")]
    public Animator animator;

    [Header("Hitbox")]
    public GameObject hitboxObject; // Reference to the hitbox's GameObject
    private RocketGloveHitbox hitboxScript;

    [Header("RightGlove")]
    public GameObject gloveObject;
    private RocketGloveAnim animScript;

    void Start()
    {
        hitboxScript = hitboxObject.GetComponent<RocketGloveHitbox>();
        hitboxScript.Initialize(this);
        hitboxObject.SetActive(false);

        animScript = gloveObject.GetComponent<RocketGloveAnim>();
        animScript.Initialize(this);
    }

    public override void shoot()
    {
        //Triggers the animation
        if (animator != null)
        {
            animator.SetTrigger("Punch");
            if (animator.GetBool("right"))
                animator.SetBool("right", false);
            else
                animator.SetBool("right", true);
        }
        else
        {
            Debug.LogWarning("Animator not defined!");
        }
    }

    public void EnableHitbox()
    {
        Debug.Log("Hitbox ativa");
        hitboxObject.SetActive(true);
    }

    public void DisableHitbox()
    {
        Debug.Log("Hitbox inativa");
        hitboxObject.SetActive(false);
    }


    public void applyDamage(Collider hit)
    {
        TreeScript treeScript = hit.gameObject.GetComponent<TreeScript>();
        if (treeScript != null)
            treeScript.UnfreezeTree();
        else
            Debug.LogError("No treeScript in hit!");
            
        if (treeScript.takeDamage(instance.GetAttribute1Value(data)))
        {
            treeScript.UnfreezeTree();

            Rigidbody treeRb = hit.gameObject.GetComponent<Rigidbody>();
            if (treeRb != null)
            {
                Transform cam = Camera.main.transform;
                Vector3 forceDirection = cam.forward;
                treeRb.AddForce(forceDirection * pushForce, ForceMode.Impulse);

                bool fallenTree = treeScript.getFallenTree();

                if (!fallenTree)
                {
                    treeScript.setFallenTree(true);
                    Debug.Log("HandleRootCollider");
                    StartCoroutine(HandleRootCollider(hit));
                }
            }
            else
            {
                Debug.LogError("treeRB not found!");
            }

        }
    }

    private IEnumerator HandleRootCollider(Collider hit)
    {
        float elapsedTime = 0f;

        RootScript rootScript = hit.gameObject.GetComponentInChildren<RootScript>();
        TreeScript treeScript = hit.gameObject.GetComponent<TreeScript>();
        if (rootScript == null || treeScript == null)
            Debug.LogError("No rootScript in hit! // No treeScript in hit!");
        else
        {
            while (elapsedTime < timeToActivateRoots)
            {
                elapsedTime += Time.deltaTime; //Increases the time elapsed
                yield return null; // Waits one frame
            }

            rootScript.EnableRootCollider();
            Debug.Log("Root Collider enabled!");
        }
    }
}
