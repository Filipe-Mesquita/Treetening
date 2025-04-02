using Unity.Collections;
using UnityEngine;
using UnityEngine.Animations;

public class TreeScript : MonoBehaviour
{
    [Header("Params")]
    [SerializeField] private float hp;
    [SerializeField] private float rootValue;
    [SerializeField] private string treeName;
    private bool fallenTree;

    void Start()
    {
        FreezeTree();
        fallenTree = false;
    }

    // ---------------------------///------------------------------///-------------------------------------///---------------------
    // Warning!!!!! Do not leave this here, instead check the tree hp every time it gets hit and unfreeze the tree when it is <= 0f
    void Update()
    {
        if (hp <= 0f)
            UnfreezeTree();
    }
    // ---------------------------///------------------------------///-------------------------------------///---------------------

    void FreezeTree()
    {
        Rigidbody treeRB = GetComponent<Rigidbody>();
        if(treeRB != null)
            treeRB.freezeRotation = true;
        else
            Debug.LogWarning("Unable to reference root's RB to freeze.");
    }

    public void UnfreezeTree()
    {
        Rigidbody treeRB = GetComponent<Rigidbody>();
        if(treeRB != null)
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
}
