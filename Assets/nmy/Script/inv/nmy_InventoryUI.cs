using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class nmy_InventoryUI : MonoBehaviour
{
    public nmy_Inventory nmy_inven; //�κ��丮 �ν��Ͻ��� ������ ����

    public GameObject inventoryPanel;//�κ��丮UI������Ʈ�� ������
    bool activateInventory = false; 


    [SerializeField]
    public nmy_Slot[] slots; //�κ��丮�� �� ������ ������ �迭
    [SerializeField]
    public Transform slotHolder; //������ ���� ������Ʈ�� ������ �ִ� ����Ȧ�� �ν��Ͻ��� ������ ����

    // Start is called before the first frame update
    void Start()
    {
        nmy_inven = nmy_Inventory.instance;
        slots = slotHolder.GetComponentsInChildren<nmy_Slot>(); //slotHolder���� ������Ʈ�� ������ slots����Ʈ�� ����
        nmy_inven.onChangItem += RedrawSlotUI;
        inventoryPanel.SetActive(activateInventory);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))//i��ư�� ���� ��� �κ��丮�� ����
        {
            activateInventory = !activateInventory; //activateInventory������ ������Ŵ
            inventoryPanel.SetActive(activateInventory); //activateInventory������ ���� inventoryPanel�� ���� �״���
        }
    }

    //�κ��丮 ������ �ʱ�ȭ�ϰ� �κ��丮�� �������� �о���̴� �޼ҵ�
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
