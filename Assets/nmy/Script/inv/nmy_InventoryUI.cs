using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class nmy_InventoryUI : MonoBehaviour
{
    public List<nmy_Item> items;

    public GameObject inventoryPanel;
    bool activateInventory = false;
    [SerializeField]
    public nmy_Slot[] slots;
    [SerializeField]
    public Transform slotParent;

    // Start is called before the first frame update
    void Start()
    {
        inventoryPanel.SetActive(activateInventory);
        slots = slotParent.GetComponentsInChildren<nmy_Slot>();
        FreshSlot();
        //potionShop.SetActive(false);
        //closePotionShop.onClick.AddListener(DeActiveShop);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            activateInventory = !activateInventory;
            inventoryPanel.SetActive(activateInventory);
        }
    }
    public void FreshSlot()
    {
        int i = 0;
        for (; i < items.Count && i < slots.Length; i++)
        {
            slots[i].item = items[i];
        }
        for (; i < slots.Length; i++)
        {
            slots[i].item = null;
        }
    }

    public void AddItem(nmy_Item _item)
    {
        if (items.Count < slots.Length)
        {
            items.Add(_item); FreshSlot();
        }
    }
    public void AcquireItem(nmy_Item _item, int _count = 1)
    {
        if (nmy_Item.ItemType.Equip != _item.itemType)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null)  // null �̶�� slots[i].item.itemName �� �� ��Ÿ�� ���� ����
                {
                    if (slots[i].item.itemName == _item.itemName)
                    {
                        slots[i].SetSlotCount(_count);
                        return;
                    }
                }
            }
        }

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].AddItem(_item, _count);
                return;
            }
        }
    }
}
