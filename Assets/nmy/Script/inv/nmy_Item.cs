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
    //아이탬의 기본적인 정보
    public ItemType itemType;
    public string itemName;
    public string itemDescription;
    public Sprite itemImage;
    public int itemCount = 0;
    public GameObject itemPrefab;
    public int weaponDamage;

    public bool isEquip;

    public bool Use()
    {
        return false;
    }

    public void itemEquip()
    {
        isEquip = true;
    }

    public void itemUnEquip()
    {
        isEquip = false;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}

