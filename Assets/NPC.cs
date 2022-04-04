using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    private Player thePlayer;

    public GameObject targetObj;

    public GameObject toObj;

    GameObject scanObject;

    public Transform Pos;

    public Vector2 boxSize;

    public DialogManager manager;

    public bool isAction;

    // Start is called before the first frame update
    void Start()
    {
        //플레이어 객체 찾기
        thePlayer = FindObjectOfType<Player>();
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        //위쪽 방향키를 누르면 이동할 맵으로 이동 +지정된 위치로 이동
        if (Input.GetKey(KeyCode.UpArrow))
        {
            scan(collision);

        }
    }

    public void scan(Collider2D collision)
    {    
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(Pos.position, boxSize, 0);
        foreach (Collider2D collider in collider2Ds)
        {
            if (collider.tag == "NPC")
            {
                scanObject = collider.gameObject;
                manager.Action(scanObject);
                targetObj.GetComponent<Player>().isControl = false;
            }
            else
            {
                scanObject = null;
                targetObj.GetComponent<Player>().isControl = true;
            }
        }    
    }
}
