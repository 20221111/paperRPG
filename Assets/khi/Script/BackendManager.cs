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
            Debug.Log("성공");
        }
        else
        {
            Debug.LogError("실패");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Backend.AsyncPoll();        
    }
}
