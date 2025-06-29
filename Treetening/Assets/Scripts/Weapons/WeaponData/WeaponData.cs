using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/WeaponData")]
public class WeaponData : ScriptableObject
{
    public string weaponId;
    public string weaponName;
    public GameObject weaponPrefab;

    public float baseAttribute1;
    public float baseAttribute2;

    public string attribute1Name;
    public string attribute2Name;

    public Sprite weaponSprite;
}
