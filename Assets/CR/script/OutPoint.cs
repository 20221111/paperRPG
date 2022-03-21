using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutPoint : MonoBehaviour
{ 
    public string outPoint;
    private Player thePlayer;

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<Player>();
        thePlayer.transform.position = this.transform.position;

    }

    // Update is called once per frame
    void Update()
    {

    }
}