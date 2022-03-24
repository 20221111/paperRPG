using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory instance;
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

    public int slotCount = 20; //���� ������ 20���� ����

    public List<nmy_Item> items = new List<nmy_Item>(); //item��ü �������� ������ ����Ʈ
    public delegate void OnChangeItem(); 
    public OnChangeItem onChangeItem;

    public void RemoveItem(int index)
    {
        items.RemoveAt(index);
        onChangeItem.Invoke();
    }


    //�κ��丮�� �������� �߰��ϴ� �޼ҵ�
    public bool AddItem(nmy_Item _item)
    {
        if (items.Count < slotCount) //������ ������ ���� �������� ���� ���
        {
            items.Add(_item);
            if (onChangeItem != null)
                onChangeItem.Invoke();
            return true;
        }
        return false;
    }



}
