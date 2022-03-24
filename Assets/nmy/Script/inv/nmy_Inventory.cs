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

    public int slotCount = 20; //슬롯 갯수는 20개로 지정

    public List<nmy_Item> items = new List<nmy_Item>(); //item객체 아이탬을 저장할 리스트
    public delegate void OnChangeItem(); 
    public OnChangeItem onChangeItem;

    public void RemoveItem(int index)
    {
        items.RemoveAt(index);
        onChangeItem.Invoke();
    }


    //인벤토리에 아이탬을 추가하는 메소드
    public bool AddItem(nmy_Item _item)
    {
        if (items.Count < slotCount) //아이탬 갯수가 슬롯 갯수보다 적을 경우
        {
            items.Add(_item);
            if (onChangeItem != null)
                onChangeItem.Invoke();
            return true;
        }
        return false;
    }



}
