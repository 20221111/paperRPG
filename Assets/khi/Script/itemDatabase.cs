using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemDatabase : MonoBehaviour
{
    public static itemDatabase instance;
  
    private void Awake()
    {
        instance = this;
    }

    public List<Item> ItemDB = new List<Item>();

    void Start()
    {
    
    }
}
