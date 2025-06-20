using System.Collections;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Animations;

public class TreeScript : MonoBehaviour
{
    [Header("Params")]
    [SerializeField] private float hp;
    [SerializeField] private int rootValue;
    [SerializeField] private string treeName;
    [SerializeField] private float timeToBeginDestroyTree = 5f;
    [SerializeField] private float sinkTreeDuration = 4f;
    [SerializeField] private float sinkSpeed = 0.5f;
    private bool fallenTree;

    void Start()
    {
        FreezeTree();
        fallenTree = false;
    }

    void FreezeTree()
    {
        Rigidbody treeRB = GetComponent<Rigidbody>();
        if (treeRB != null)
            treeRB.freezeRotation = true;
        else
            Debug.LogWarning("Unable to reference root's RB to freeze.");
    }

    public void UnfreezeTree()
    {
        Rigidbody treeRB = GetComponent<Rigidbody>();
        if (treeRB != null)
            treeRB.freezeRotation = false;
        else
            Debug.LogWarning("Unable to reference root's RB to freeze.");
    }

    public bool getFallenTree()
    {
        return fallenTree;
    }

    public void setFallenTree(bool fallenTree)
    {
        this.fallenTree = fallenTree;
    }

    public bool takeDamage(float dmg)
    {
        hp = hp - dmg;
        Debug.Log($"Tree hp = {hp}");

        if (hp <= 0f)
        {
            UnfreezeTree();
            return true;
        }
        else
        {
            return false;
        }
    }

    public int getRootValue()
    {
        return rootValue;
    }

    public void destroyTree()
    {
        StartCoroutine(waitAndDestroy());
    }

    private IEnumerator waitAndDestroy()
    {
        //Wait for the time to start destroying the tree
        float elapsedTime = 0f;
        while (elapsedTime < timeToBeginDestroyTree)
        {
            elapsedTime += Time.deltaTime; //Increases the time elapsed
            yield return null; // Waits one frame
        }

        //Freeze tree's rotation
        FreezeTree();

        // Desable the collider
        Collider treeCollider = GetComponent<Collider>();
        if (treeCollider != null)
            treeCollider.enabled = false;

        // Turn Rigidbody kinematic
        Rigidbody treeRB = GetComponent<Rigidbody>();
        if (treeRB != null)
            treeRB.isKinematic = true;

        // Slowly sink the tree
        float sinkElapsed = 0f;

        while (sinkElapsed < sinkTreeDuration)
        {
            transform.position -= new Vector3(0, sinkSpeed * Time.deltaTime, 0);
            sinkElapsed += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}
