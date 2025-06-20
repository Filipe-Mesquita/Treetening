using System.Collections;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class RootScript : MonoBehaviour
{
    [SerializeField] TreeScript treeScript;
    [SerializeField] float timeToActivateRoots = 1.5f;

    void Start()
    {
        DisableRootCollider();
    }
    public void DestroyRoot()
    {
        Destroy(gameObject);
        treeScript.destroyTree();
    }

    public void DisableRootCollider()
    {
        GetComponent<SphereCollider>().enabled = false;
    }

    public void EnableRootCollider()
    {
        StartCoroutine(waitAndEnable());
    }

    private IEnumerator waitAndEnable()
    {
        float elapsedTime = 0f;
        while (elapsedTime < timeToActivateRoots)
        {
            elapsedTime += Time.deltaTime; //Increases the time elapsed
            yield return null; // Waits one frame
        }

        GetComponent<SphereCollider>().enabled = true;
    }

    public int getRootValue()
    {

        return treeScript.getRootValue();
    }
}
