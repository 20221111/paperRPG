using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory instance;
    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    #endregion

    public int slotCount;

    public List<Item> items = new List<Item>();
    public delegate void OnChangeItem();
    public OnChangeItem onChangeItem;

    // Start is called before the first frame update
    void Start()
    {
        slotCount = 20;
    }

    public void RemoveItem(int index)
    {
        items.RemoveAt(index);
        onChangeItem.Invoke();
    }


    public bool AddItem(Item _item)
    {
        if(items.Count < slotCount)
        {
            items.Add(_item);
            if(onChangeItem != null)
                onChangeItem.Invoke();
            return true;
        }
        return false;
    }



}
