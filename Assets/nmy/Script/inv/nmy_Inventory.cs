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

    public delegate void OnChangItem(); // 아이탬이 변경되었을 경우 실행할 대리자 선언
    public OnChangItem onChangItem; //대리자 인스턴스 생성

    public nmy_Item WC; //테스트용 아이탬 객체 생성
    public nmy_Item WE; //테스트용 아이탬 객체 생성
    public nmy_Item WR; //테스트용 아이탬 객체 생성

    public List<nmy_Item> items = new List<nmy_Item>(); //인벤토리에 저장된 아이탬을 저장할 리스트

    public int slotCnt = 20; //슬롯 갯수는 20개로 지정

    private void Start()
    {
        AddItem(WC);
        AddItem(WE);
        AddItem(WR);

    }

    //인벤토리에 아이탬을 더하는 메소드
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
