using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Item", menuName ="New Item/item")]
public class Item :ScriptableObject
{
    public enum ItemType
    {
        Use,
        Equip,
        Quest,
        ETC
    }

    public ItemType itemType;
    public string itemName;
    public string itemDescription;
    public Sprite itemImage;
    public int itemCount;
    public GameObject itemPrefab;

    public string weaponType;
    public bool isEquip;
}
