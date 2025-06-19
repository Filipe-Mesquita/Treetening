using UnityEngine;

public class RootScript : MonoBehaviour
{
    [SerializeField] TreeScript treeScript;

    void Start()
    {
        DisableRootCollider();
    }
    public void DestroyRoot()
    {
        Destroy(gameObject);
    }

    public void DisableRootCollider()
    {
        GetComponent<SphereCollider>().enabled = false;
    }

    public void EnableRootCollider()
    {
        GetComponent<SphereCollider>().enabled = true;
    }

    public int getRootValue()
    {
        
        return treeScript.getRootValue();
    }
}
