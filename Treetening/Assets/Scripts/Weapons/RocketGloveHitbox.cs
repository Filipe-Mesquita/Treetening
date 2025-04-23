using UnityEngine;

public class RocketGloveHitbox : MonoBehaviour
{
    private RocketGlovesBehaviour weapon;

    public void Initialize(RocketGlovesBehaviour weaponRef)
    {
        weapon = weaponRef;
    }

    private void OnTriggerEnter(Collider hit)
    {
        if (((1 << hit.gameObject.layer) & weapon.hitLayer) != 0)           // Converts the object's layer to a bitmask and uses bitwise AND to check if it's in the allowed hitLayer
        {                                                                   //For exmample, if 1 << hit.gameObject.layer returns 00010 and weapon.hitLayer is 01010 then the bitwise & will return 00010, wich is different from 0
            Debug.Log("RocketGlove acertou em: " + hit.name); //Debug       //In the sequence 00010 every bit is a layer from the ones defined in the engine, 0 means its no the selecetd layer and 1 means its the selected one
            weapon.applyDamage(hit); //applies damage to the object
        }
    }
}
