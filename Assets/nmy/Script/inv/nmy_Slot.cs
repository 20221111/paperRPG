using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class nmy_Slot : MonoBehaviour
{

    public nmy_Item item; //�ش� ���Կ� ����� �ִ� �������� ����
    public Image itemimage;

    public void UpdateSlotUI()
    {
        itemimage.sprite = item.itemImage; //�������� �̹����� ������ �ش� ������ �̹����� ����
        itemimage.gameObject.SetActive(true);//������Ʈ�� Ȱ��ȭ
    }    

    public void RemoveSlot()
    {
        item = null;  //������ ��ü�� ����
        itemimage.gameObject.SetActive(true);//������Ʈ�� Ȱ��ȭ
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
