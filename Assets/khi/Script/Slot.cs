using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;



public class Slot : MonoBehaviour
{
    public int itemCount;
    public Item item;
    [SerializeField]
    public Image itemIcon;
    public bool isEquip = false;
    public static List<Item> equipedItem;
    [SerializeField]
    private Text ItemTextCount;
    [SerializeField]
    private GameObject ItemCountVisible;

    private void SetColor(float _alpha)
    {
        Color color = itemIcon.color;
        color.a = _alpha;
        itemIcon.color = color;
    }

    public void AddItem(Item _item, int _count = 1)
    {
        item = _item;
        itemCount = _count;
        itemIcon.sprite = item.itemImage;

        if (item.itemType != Item.ItemType.Equip)
        {
            ItemCountVisible.SetActive(true);
            ItemTextCount.text = itemCount.ToString();
        }
        else
        {
            ItemTextCount.text = "0";
            ItemCountVisible.SetActive(false);
        }
        SetColor(1);
    }

    public void SetSlotCount(int _count)
    {
        itemCount += _count;
        ItemTextCount.text = itemCount.ToString();

        if (itemCount <= 0)
            ClearSlot();
    }

    private void ClearSlot()
    {
        item = null;
        itemCount = 0;
        itemIcon.sprite = null;
        SetColor(0);

        ItemTextCount.text = "0";
        ItemCountVisible.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
       
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (item != null)
            {
                if (item.itemType == Item.ItemType.Equip)
                {
                    if(!isEquip)
                    {
                        equipedItem.Add(item);
                        Equipment();
                        itemIcon.color = Color.green;
                    }
                    else
                    {
                        SetColor(0);
                        UnEquipment();
                        equipedItem.RemoveAt(0);
                        equipedItem.Add(item);
                        itemIcon.color = Color.green;
                    }
                }
                else
                {
                    Debug.Log(item.itemName + " 을 사용했습니다.");
                    SetSlotCount(-1);
                }
            }
        }
    }
    void Equipment()
    {
        Item _equipItem = equipedItem[0];
        Instantiate(_equipItem);
    }
    void UnEquipment()
    {
        Item _equipItem = equipedItem[0];
        Destroy(_equipItem);
    }
}
