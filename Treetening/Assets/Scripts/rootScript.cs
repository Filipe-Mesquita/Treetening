using UnityEngine;

public class RootScript : MonoBehaviour
{
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
}
