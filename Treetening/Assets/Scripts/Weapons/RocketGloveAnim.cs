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

    public void DisabletHitbox()
    {
        weapon.DisableHitbox();
    }

    public void EnableShoot()
    {
        weapon.setCanShoot(true);
    }

    public void DisableShoot()
    {
        weapon.setCanShoot(false);
    }
}
