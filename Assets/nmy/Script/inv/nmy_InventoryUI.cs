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

    public nmy_Item WC; //�׽�Ʈ�� ������ ��ü ����
    public nmy_Item WE; //�׽�Ʈ�� ������ ��ü ����
    public nmy_Item WR; //�׽�Ʈ�� ������ ��ü ����

    // Start is called before the first frame update
    void Start()
    {
        nmy_inven = nmy_Inventory.instance;
        slots = slotHolder.GetComponentsInChildren<nmy_Slot>(); //slotHolder���� ������Ʈ�� ������ slots����Ʈ�� ����
        nmy_inven.onChangItem += RedrawSlotUI;
        inventoryPanel.SetActive(activateInventory);


        nmy_inven.AddItem(WC);
        nmy_inven.AddItem(WE);
        nmy_inven.AddItem(WR);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))//i��ư�� ���� ��� �κ��丮�� ����
        {
            activateInventory = !activateInventory; //activateInventory������ ������Ŵ
            inventoryPanel.SetActive(activateInventory); //activateInventory������ ���� inventoryPanel�� ���� �״���
        }

        //�׽�Ʈ �ڵ�
        if (Input.GetKeyDown(KeyCode.P))
        {
            nmy_inven.RemoveItem(WR);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            nmy_inven.AddItem(WR);
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
