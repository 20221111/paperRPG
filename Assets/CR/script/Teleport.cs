using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject targetObj;

    public GameObject toObj;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        animator.SetBool("gate", true); 
        if(collision.CompareTag("Player"))
        {
            targetObj = collision.gameObject;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")&&Input.GetKeyDown(KeyCode.UpArrow))
        {
            StartCoroutine(TeleportRountine());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        animator.SetBool("gate", false);
    }

    IEnumerator TeleportRountine()
    {
        yield return null;
        targetObj.transform.position = toObj.transform.position;
    }
    


    // Update is called once per frame
    void Update()
    {
        
    }
}
