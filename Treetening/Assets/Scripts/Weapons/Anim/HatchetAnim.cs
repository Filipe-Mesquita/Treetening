using UnityEngine;

public class HatchetAnim : MonoBehaviour
{
    private HatchetBehaviour weapon;

    public void Initialize(HatchetBehaviour weaponRef)
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
}