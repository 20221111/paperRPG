using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class nmy_Slot : MonoBehaviour
{

    public nmy_Item item; //해당 슬롯에 저장되 있는 아이탬을 저장
    public Image itemimage;

    public void UpdateSlotUI()
    {
        itemimage.sprite = item.itemImage; //아이탬의 이미지를 가져와 해당 슬롯의 이미지로 설정
        itemimage.gameObject.SetActive(true);//오브젝트를 활성화
    }    

    public void RemoveSlot()
    {
        item = null;  //아이탬 객체를 지움
        itemimage.gameObject.SetActive(true);//오브젝트를 활성화
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
