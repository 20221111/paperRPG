using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    public string startPoint;
    private Player thePlayer;

    // Start is called before the first frame update
    void Start()
    {
        //플레이어 객체 찾기    
        thePlayer = FindObjectOfType<Player>();
        //플레이어를 startpoint에 갖다놓기
        thePlayer.transform.position = this.transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
