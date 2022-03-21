using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    Inventory inven;

    public GameObject inventoryPanel;
    bool activateInventory = false;

    public Slot[] slots;
    public Transform slotHolder;

    // Start is called before the first frame update
    void Start()
    {
        inventoryPanel.SetActive(activateInventory);
        inven = Inventory.instance;
        slots = slotHolder.GetComponentsInChildren<Slot>();
        inven.onChangeItem += RedrawSlotUI;
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
            Debug.Log("i≈∞ ¥©∏ß");
        }
        if (Input.GetMouseButtonUp(0))
            RayPotionShop();
    }
    private void SlotNumber()
    {
        for (int i = 0; i < slots.Length; i++)
            slots[i].slotnum = i;
    }

    void RedrawSlotUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].RemoveSlot();
        }
        for(int i = 0; i < inven.items.Count; i++)
        {
            slots[i].item = inven.items[i];
            slots[i].UpdateSlotUI();
        }
    }

    public GameObject potionShop;
    public Button closePotionShop;

    public void RayPotionShop()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = -10;
        if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(-1))
        {
            RaycastHit2D hit2D = Physics2D.Raycast(mousePos, transform.forward, 30);
            if (hit2D.collider != null)
            {
                if (hit2D.collider.CompareTag("Store"))
                {
                    ActiveShop(true);
                }
            }
        }
    }

    public void ActiveShop(bool isOpen)
    {
        potionShop.SetActive(isOpen);
    }
    public void DeActiveShop()
    {
        ActiveShop(false);
    }
}
