using UnityEngine;

public class RootScript : MonoBehaviour
{
    //deletes the gameObject the script is atached to
    public void DestroyRoot()
    {
        Destroy(gameObject);
    }
}
