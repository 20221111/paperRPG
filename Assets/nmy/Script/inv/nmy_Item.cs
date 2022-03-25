using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum nmy_ItemType
{
    Use,
    Equip,
    Quest,
    ETC
}

[System.Serializable]
public class nmy_Item : MonoBehaviour
{

    public ItemType itemType;
    public string itemName;
    public string itemDescription;
    public Sprite itemImage;
    public int itemCount;
    public GameObject itemPrefab;

    public string weaponType;
    public bool isEquip;

    public bool Use()
    {
        return false;
    }
}

