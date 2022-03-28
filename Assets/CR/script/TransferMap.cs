using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferMap : MonoBehaviour
{
    public string transferMapName;

    private Player thePlayer;

    public GameObject targetObj;

    public GameObject toObj;

    // Start is called before the first frame update
    void Start()
    {
        //플레이어 객체 찾기
        thePlayer = FindObjectOfType<Player>();
    }

 
    private void OnTriggerStay2D(Collider2D collision)
    {
        //위쪽 방향키를 누르면 이동할 맵으로 이동 +지정된 위치로 이동
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            //포탈에 부딪힌 객체가 player일때
            if (collision.gameObject.name == "player")
            {
                //플레이어의 현재 맵 = 이동할 맵
                thePlayer.currentMapName = transferMapName;

                SceneManager.LoadScene(transferMapName);
                thePlayer.transform.position = new Vector3(0, 0, 0);
            }

        }
    }

    public IEnumerator Routine() //맵 이동시 플레이어 이동 제한
    {
        yield return null;
        targetObj.GetComponent<Player>().isControl = false;

        yield return new WaitForSeconds(0.5f);

        targetObj.transform.position = toObj.transform.position;

        yield return new WaitForSeconds(0.5f);

        targetObj.GetComponent<Player>().isControl = true;
    }
}