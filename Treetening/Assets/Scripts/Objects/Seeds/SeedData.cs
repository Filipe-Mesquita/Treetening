using UnityEngine;

[CreateAssetMenu(fileName = "SeedData", menuName = "Scriptable Objects/SeedsData")]
public class SeedData : ScriptableObject
{
    public string seedID;

    public string seedName;

    public float growthTime;

    public int seedPrice;

    public GameObject treePrefab;

    public Sprite seedSprite;
}
