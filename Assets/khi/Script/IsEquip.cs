using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsEquip : Slot
{
    List<Item> EquipItem = equipedItem;
void Equipment()
    {
        Item _equipItem = EquipItem[0];
        Instantiate(_equipItem);
    }
    void UnEquipment(List<Item> _item)
    {
        Item _equipItem = EquipItem[0];
        Destroy(_equipItem);
    }
}
