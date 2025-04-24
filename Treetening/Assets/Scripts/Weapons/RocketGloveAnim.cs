using UnityEngine;

public class RocketGloveAnim : MonoBehaviour
{
    private RocketGlovesBehaviour weapon;

    public void Initialize(RocketGlovesBehaviour weaponRef)
    {
        weapon = weaponRef;
    }

    public void EnableHitbox()
    {
        weapon.EnableHitbox();
    }

    public void DisableHitbox()
    {
        weapon.DisableHitbox();
    }
}
