using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class nmy_Slot : MonoBehaviour
{
    public GameObject player;
    public nmy_Item item; //�ش� ���Կ� ����� �ִ� �������� ����
    public Image itemimage;

    //���� ������ ������Ʈ
    public void UpdateSlotUI()
    {
        itemimage.sprite = item.itemImage; //�������� �̹����� ������ �ش� ������ �̹����� ����
        itemimage.gameObject.SetActive(true);//������Ʈ�� Ȱ��ȭ
    } 
    
    //���Ծ��� ������ ������ ����
    public void RemoveSlot()
    {
        item = null;  //������ ��ü�� ����
        itemimage.gameObject.SetActive(true);//������Ʈ�� Ȱ��ȭ
    }

    //��� ������
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
                    Debug.Log("���� �������� ��� �����ؾ� �մϴ�.");
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
