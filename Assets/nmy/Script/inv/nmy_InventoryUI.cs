using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class nmy_InventoryUI : MonoBehaviour
{
    public nmy_Inventory nmy_inven; //인벤토리 인스턴스를 저장할 공간

    public GameObject inventoryPanel;//인벤토리UI오브젝트를 가져옴
    bool activateInventory = false; 


    [SerializeField]
    public nmy_Slot[] slots; //인벤토리의 각 슬롯을 저장할 배열
    [SerializeField]
    public Transform slotHolder; //슬롯을 하위 오브젝트로 가지고 있는 슬롯홀더 인스턴스를 저장할 공간

    // Start is called before the first frame update
    void Start()
    {
        nmy_inven = nmy_Inventory.instance;
        slots = slotHolder.GetComponentsInChildren<nmy_Slot>(); //slotHolder하위 오브젝트를 가져와 slots리스트에 저장
        nmy_inven.onChangItem += RedrawSlotUI;
        inventoryPanel.SetActive(activateInventory);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))//i버튼을 누를 경우 인벤토리를 열음
        {
            activateInventory = !activateInventory; //activateInventory변수를 반전시킴
            inventoryPanel.SetActive(activateInventory); //activateInventory변수에 맞춰 inventoryPanel을 껏다 켰다함
        }
    }

    //인벤토리 슬롯을 초기화하고 인벤토리에 아이탬을 읽어들이는 메소드
    void RedrawSlotUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].RemoveSlot();
        }
        for (int i = 0; i < nmy_inven.items.Count; i++)
        {
            slots[i].item = nmy_inven.items[i];
            slots[i].UpdateSlotUI();
        }
    }

}
