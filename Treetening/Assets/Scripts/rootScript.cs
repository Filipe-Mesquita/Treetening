using UnityEngine;

public class rootScript : MonoBehaviour
{
    //deletes the gameObject the script is atached to
    public void DestroyRoot()
    {
        Destroy(gameObject);
    }
}
