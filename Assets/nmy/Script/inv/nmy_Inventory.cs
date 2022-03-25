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

    public delegate void OnChangItem(); // 대리자 선언
    public OnChangItem onChangItem; //대리자 인스턴스 생성

    public nmy_Item test_Item; //테스트용 아이탬 객체 생성

    public List<nmy_Item> items = new List<nmy_Item>(); //인벤토리에 저장된 아이탬을 저장할 리스트

    public int slotCnt = 20; //슬롯 갯수는 20개로 지정

    private void Start()
    {
        AddItem(test_Item);
        AddItem(test_Item);
        AddItem(test_Item);
        AddItem(test_Item);
        AddItem(test_Item);
        AddItem(test_Item);
        AddItem(test_Item);
        AddItem(test_Item);
        AddItem(test_Item);
        AddItem(test_Item);

    }

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
