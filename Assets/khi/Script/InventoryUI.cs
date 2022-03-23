using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public List<Item> items;

    public GameObject inventoryPanel;
    bool activateInventory = false;
    [SerializeField]
    public Slot[] slots;
    [SerializeField]
    public Transform slotParent;

    // Start is called before the first frame update
    void Start()
    {
        inventoryPanel.SetActive(activateInventory);
        slots = slotParent.GetComponentsInChildren<Slot>();
        FreshSlot();
        //potionShop.SetActive(false);
        //closePotionShop.onClick.AddListener(DeActiveShop);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            activateInventory = !activateInventory;
            inventoryPanel.SetActive(activateInventory);
        }
    }
    public void FreshSlot()
    {
        int i = 0;
        for(; i < items.Count && i < slots.Length; i++)
        {
            slots[i].item = items[i];
        }
        for(; i < slots.Length; i++)
        {
            slots[i].item = null;
        }
    }

    public void AddItem(Item _item)
    {
        if (items.Count < slots.Length)
        {
            items.Add(_item); FreshSlot();
        }
    }
    public void AcquireItem(Item _item, int _count = 1)
    {
        if (Item.ItemType.Equip != _item.itemType)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null)  // null 이라면 slots[i].item.itemName 할 때 런타임 에러 나서
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
