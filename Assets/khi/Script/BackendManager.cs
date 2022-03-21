using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;

public class BackendManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var bro = Backend.Initialize(true);
        if(bro.IsSuccess())
        {
            Debug.Log("����");
        }
        else
        {
            Debug.LogError("����");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Backend.AsyncPoll();        
    }
}
