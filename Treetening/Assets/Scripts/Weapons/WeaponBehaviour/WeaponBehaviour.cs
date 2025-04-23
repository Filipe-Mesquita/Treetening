using UnityEngine;

public abstract class WeaponBehaviour : MonoBehaviour
{
    public WeaponInstance instance;
    public WeaponData data;

    public virtual void Initialize(WeaponInstance i, WeaponData d)
    {
        instance = i;
        data = d;
    }

    public abstract void shoot();
}
