using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Use,
    Equip,
    Quest,
    ETC
}

[System.Serializable]
public class Item
{
    public ItemType itemType;
    public string itemName;
    public string itemDescription;
    public Sprite itemImage;
    public int itemCount;
    public List<ItemEffect> efts;

    // Start is called before the first frame update
    public bool Use()
    {
        bool isUsed = false;
        foreach(ItemEffect eft in efts)
        {
            isUsed = eft.ExcuteRole();
        }
        return false;
    }
}
