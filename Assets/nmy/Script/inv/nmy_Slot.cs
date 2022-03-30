using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class nmy_Slot : MonoBehaviour
{
    public GameObject player;
    public nmy_Item item; //해당 슬롯에 저장되 있는 아이탬을 저장
    public Image itemimage;

    //슬롯 정보를 업데이트
    public void UpdateSlotUI()
    {
        itemimage.sprite = item.itemImage; //아이탬의 이미지를 가져와 해당 슬롯의 이미지로 설정
        itemimage.gameObject.SetActive(true);//오브젝트를 활성화
    } 
    
    //슬롯안의 아이탬 정보를 삭제
    public void RemoveSlot()
    {
        item = null;  //아이탬 객체를 지움
        itemimage.gameObject.SetActive(true);//오브젝트를 활성화
    }

    //장비를 장착함
    public void Equip()
    {
        if (item.itemType == ItemType.Equip)
        {
            if (item.isEquip)
            {
                unEquip();
            }
            else
            {
                if (player.GetComponent<Player>().equipmunt != null)
                {
                    Debug.Log("현재 장착중인 장비를 해제해야 합니다.");
                }
                else
                {
                    itemimage.color = Color.green;
                    item.itemEquip();
                    player.GetComponent<Player>().PlayerEquip(item);
                }
            }
        }
    }

    public void unEquip()
    {
        item.itemUnEquip();
        itemimage.color = Color.white;
        player.GetComponent<Player>().PlayerUnEquip(item);
    }

    // Update is called once per frame
    void Start()
    {
        player = GameObject.Find("player");
    }
}
