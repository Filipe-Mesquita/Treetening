using UnityEngine;

[CreateAssetMenu(fileName = "SeedData", menuName = "Scriptable Objects/SeedsData")]
public class SeedData : ScriptableObject
{
    public int seedID;

    public string seedName;

    public GameObject treePrefab;
}
