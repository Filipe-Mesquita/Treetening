using UnityEngine;

public class treeScript : MonoBehaviour
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
    [SerializeField] private GameObject rootObject;
    [SerializeField] private Mesh rootMesh;
    [SerializeField] private Material rootMeshMat;
    [SerializeField] private MeshFilter rootMeshFilter;
    [SerializeField] private MeshRenderer rootMeshRend;

    void Start()
    {
        
    }

    void destroyRoot()
    {
        Destroy(rootObject);
    }
}
