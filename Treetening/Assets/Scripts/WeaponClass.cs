using UnityEngine;

public class Weaponclass
{
    private string weaponName;
    private float weaponDamage;
    private float weaponAmmunition;


    public Weaponclass(string name, float damage, float ammunition)
    {
        weaponName = name;
        weaponDamage = damage;
        weaponAmmunition = ammunition;
    }

    public string WeaponName
    {
        get {return weaponName;}
        set {weaponName = value;}
    }

    public float WeaponDamage
    {
        get {return weaponDamage;}
        set {weaponDamage = value;}
    }

    public float WeaponAmmunition
    {
        get {return weaponAmmunition;}
        set {weaponAmmunition = value;}
    }
}