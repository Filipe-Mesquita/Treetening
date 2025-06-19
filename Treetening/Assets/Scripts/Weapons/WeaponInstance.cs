using System;

[Serializable]
public class WeaponInstance
{
    public string weaponId;
    //public bool isUnlocked;
    public int attribute1Level;
    public int attribute2Level;

    public float GetAttribute1Value(WeaponData data)
    {
        return data.baseAttribute1 * (1 + 0.1f * attribute1Level);
    }

    public float GetAttribute2Value(WeaponData data)
    {
        return data.baseAttribute2 * (1 + 0.1f * attribute2Level);
    }
}