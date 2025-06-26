using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HudScript : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] PlayerInventory inventory;
    [SerializeField] WeaponManager weaponManager;

    [Header("Money")]
    [SerializeField] TextMeshProUGUI moneyTxt;

    [Header("Weapon")]
    [SerializeField] UnityEngine.UI.Image weaponSprite;

    [Header("Seeds")]
    [SerializeField] TextMeshProUGUI mahoganyTxt;
    [SerializeField] TextMeshProUGUI pineTxt;
    [SerializeField] GameObject seedsCanvas;

    public void UpdateMoney()
    {
        moneyTxt.text = $"{inventory.getMoney()}";
    }

    public void UpdateWeapon()
    {
        weaponSprite.sprite = weaponManager.currentWeapon.data.weaponSprite;
    }

    public void UpdateSeeds()
    {
        List<int> seedsLst = inventory.getOwnedSeeds();
        bool hasSeeds = false;
        for (int i = 0; i < seedsLst.Count; i++)
        {
            if (seedsLst[i] > 0)
            {
                hasSeeds = true;
                break;
            }

        }

        if (hasSeeds)
        {
            seedsCanvas.SetActive(true);
            mahoganyTxt.text = seedsLst[0].ToString();
            pineTxt.text = seedsLst[1].ToString();
        }
        else
        {
            seedsCanvas.SetActive(false);
        }
    }
}
