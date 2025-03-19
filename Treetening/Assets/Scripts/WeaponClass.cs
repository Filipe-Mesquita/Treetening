using UnityEngine;

public class Weapon
{
    private int weaponId;
    private string weaponName;
    private float weaponDamage;
    private float weaponAmmunition;


    public Weapon(int id, string name, float damage, float ammunition)
    {
        weaponId = id;
        weaponName = name;
        weaponDamage = damage;
        weaponAmmunition = ammunition;
    }

    public int WeaponId
    {
        get {return weaponId;}
        set {weaponId = value;}
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
    
    public void DisplayInfo()
    {
        UnityEngine.Debug.Log($"ID: {weaponId}, Weapon: {weaponName}, Damage: {weaponDamage}, Ammunition: {weaponAmmunition}");
    }
}