using Unity.Collections;
using UnityEngine;
using UnityEngine.Animations;

public class TreeScript : MonoBehaviour
{
    [Header("Params")]
    [SerializeField] private float hp;
    [SerializeField] private float rootValue;
    [SerializeField] private string treeName;
    
    [Header("Tree Mesh")]
    [SerializeField] private Mesh treeMesh;
    [SerializeField] private Material treeMeshMat;
    [SerializeField] private MeshFilter treeMeshFilter;
    [SerializeField] private MeshRenderer treeMeshRend;

    [Header("Root Mesh")]
    [SerializeField] private Mesh rootMesh;
    [SerializeField] private Material rootMeshMat;
    [SerializeField] private MeshFilter rootMeshFilter;
    [SerializeField] private MeshRenderer rootMeshRend;

    void Start()
    {
        FreezeTree();
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

    void UnfreezeTree()
    {
        Rigidbody treeRB = GetComponent<Rigidbody>();
        if(treeRB != null)
            treeRB.freezeRotation = false;
        else
            Debug.LogWarning("Unable to reference root's RB to freeze.");
    }
}
