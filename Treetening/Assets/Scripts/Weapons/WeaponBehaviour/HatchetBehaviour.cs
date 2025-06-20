using System.Collections;
using UnityEngine;

public class HatchetBehaviour : WeaponBehaviour
{
    public LayerMask hitLayer;

    [Tooltip("Value of the force applied to the tree when it dies")]
    [SerializeField] private float pushForce = 500f;

    [Header("Animation")]
    public Animator animator;
    private HatchetAnim animScript;
    public bool canShoot = true;    //Used to make sure the weapon only can shoot when not in the midle of an animation

    [Header("Hitbox")]
    public GameObject hitboxObject; // Reference to the hitbox's GameObject
    private HatchetHitbox hitboxScript;

    [Header("Hatchet")]
    public GameObject hatchetObject;


    void Start()
    {
        hitboxScript = hitboxObject.GetComponent<HatchetHitbox>();
        hitboxScript.Initialize(this);
        hitboxObject.SetActive(false);

        animScript = hatchetObject.GetComponent<HatchetAnim>();
        animScript.Initialize(this);
    }

    public override void shoot()
    {
        if (canShoot)
        {
            //EnableHitbox();
            canShoot = false;

            //Triggers the animation
            if (animator != null)
            {
                animator.speed = instance.GetAttribute2Value(data); //Sets the speed according to the attribute2 level
                animator.SetTrigger("attack");    //treiggers the animation
            }
            else
            {
                Debug.LogWarning("Animator not defined!");
            }
        }
    }

    public void EnableHitbox()
    {
        //Debug.Log("Hitbox ativa");
        hitboxObject.SetActive(true);
    }

    public void DisableHitbox()
    {
        //Debug.Log("Hitbox inativa");
        hitboxObject.SetActive(false);
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
                Vector3 forceDirection = -cam.right;
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
        RootScript rootScript = hit.gameObject.GetComponentInChildren<RootScript>();
        TreeScript treeScript = hit.gameObject.GetComponent<TreeScript>();
        if (rootScript == null || treeScript == null)
            Debug.LogError("No rootScript in hit! // No treeScript in hit!");
        else
        {
            rootScript.EnableRootCollider();
            Debug.Log("Root Collider enabled!");
            yield return null;
        }
    }
}
