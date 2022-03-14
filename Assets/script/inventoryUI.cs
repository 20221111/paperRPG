using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventoryUI : MonoBehaviour
{
    public GameObject inventoryPanel;
    bool activeInventory = false;

    public void Start()
    {
        inventoryPanel.SetActive(activeInventory);

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            activeInventory = !activeInventory;
            inventoryPanel.SetActive(activeInventory);
        }

    }
}
