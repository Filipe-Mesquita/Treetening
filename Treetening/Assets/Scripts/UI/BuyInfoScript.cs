using UnityEngine;

public class BuyInfoScript : MonoBehaviour
{
    private string itemID;

    public void setItemID(string itemID)
    {
        this.itemID = itemID;
    }

    public string getItemID()
    {
        return itemID;
    }
}
