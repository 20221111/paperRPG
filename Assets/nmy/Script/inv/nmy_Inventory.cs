using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nmy_Inventory : MonoBehaviour
{
    #region Singleton
    public static nmy_Inventory instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    #endregion

    public delegate void OnChangItem(); // �������� ����Ǿ��� ��� ������ �븮�� ����
    public OnChangItem onChangItem; //�븮�� �ν��Ͻ� ����

    public nmy_Item WC; //�׽�Ʈ�� ������ ��ü ����
    public nmy_Item WE; //�׽�Ʈ�� ������ ��ü ����
    public nmy_Item WR; //�׽�Ʈ�� ������ ��ü ����

    public List<nmy_Item> items = new List<nmy_Item>(); //�κ��丮�� ����� �������� ������ ����Ʈ

    public int slotCnt = 20; //���� ������ 20���� ����

    private void Start()
    {
        AddItem(WC);
        AddItem(WE);
        AddItem(WR);

    }

    //�κ��丮�� �������� ���ϴ� �޼ҵ�
    public bool AddItem(nmy_Item _Item)
    {
        if (items.Count < slotCnt)
        {
            items.Add(_Item);
            onChangItem.Invoke();
            return true;
        }
        return false;
    }


}
