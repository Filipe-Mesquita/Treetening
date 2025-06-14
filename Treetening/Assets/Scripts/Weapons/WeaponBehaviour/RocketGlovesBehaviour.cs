using System.Collections;
using UnityEngine;

public class RocketGlovesBehaviour : WeaponBehaviour
{
    public LayerMask hitLayer;

    [Tooltip("Value of the force applied to the tree when it dies")]
    [SerializeField] private float pushForce = 500f;

    [Tooltip("Time it takes to activate a root collider once the tree dies")]
    [SerializeField] private float timeToActivateRoots = 1.5f;

    [Header("Animation")]
    public Animator animator;

    private bool punchAnimToggle = false;   //Used to alternate between left and right punches
    public bool canShoot = true;    //Used to make sure the weapon only can shoot when not in the midle of an animation

    [Header("Hitbox")]
    public GameObject rightHitboxObject; // Reference to the right hitbox's GameObject
    public GameObject leftHitboxObject; // Reference to the left hitbox's GameObject
    private RocketGloveHitbox rightHitboxScript;
    private RocketGloveHitbox leftHitboxScript;

    [Header("RocketGloves")]
    public GameObject glovesObject;
    private RocketGloveAnim animScript;

    void Start()
    {
        rightHitboxScript = rightHitboxObject.GetComponent<RocketGloveHitbox>();
        rightHitboxScript.Initialize(this);
        rightHitboxObject.SetActive(false);

        leftHitboxScript = leftHitboxObject.GetComponent<RocketGloveHitbox>();
        leftHitboxScript.Initialize(this);
        leftHitboxObject.SetActive(false);

        animScript = glovesObject.GetComponent<RocketGloveAnim>();
        animScript.Initialize(this);
    }

    public override void shoot()
    {
        //Triggers the animation
        if (animator != null)
        {
            if (canShoot)
            {
                if (punchAnimToggle)
                {
                    animator.SetTrigger("right");
                }
                else
                {
                    animator.SetTrigger("left");
                }

                punchAnimToggle = !punchAnimToggle;
            }
        }
        else
        {
            Debug.LogWarning("Animator not defined!");
        }
    }

    public void EnableHitbox()
    {
        //Debug.Log("Hitbox ativa");
        rightHitboxObject.SetActive(true);
        leftHitboxObject.SetActive(true);
    }

    public void DisableHitbox()
    {
        //Debug.Log("Hitbox inativa");
        rightHitboxObject.SetActive(false);
        leftHitboxObject.SetActive(false);
    }

    public void setCanShoot(bool freeToShoot)
    {
        canShoot = freeToShoot;
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
