using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime;

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

    public List<nmy_Item> items = new List<nmy_Item>(); //인벤토리에 저장된 아이탬을 저장할 리스트

    public int slotCnt = 20; //슬롯 갯수는 20개로 지정


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

    //인벤토리에 아이탬을 삭제하는 메소드
    public bool RemoveItem(nmy_Item _Item)
    {
        int _index;

        if (items.Exists(x => x = _Item))
        {
            _index = items.IndexOf(_Item);
            if (items[_index].itemCount > 1)
            {
                items[_index].itemCount -= 1;
            }
            else
            {
                items.RemoveAt(_index);
            }
            onChangItem.Invoke();
            return true;
        }
        else
        {
            return false;
        }

    }

}
