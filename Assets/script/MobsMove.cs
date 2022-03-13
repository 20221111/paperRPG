using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobsMove : MonoBehaviour
{
    Rigidbody2D rigid;
    public int nextMove;
    
    SpriteRenderer spriteRenderer;
    Animator anim;
    CapsuleCollider2D collider;

    //초기화
    void Awake() 
    {
      rigid = GetComponent<Rigidbody2D>();
      anim = GetComponent<Animator>();
      spriteRenderer = GetComponent<SpriteRenderer>();
      collider = GetComponent<CapsuleCollider2D>();
      Think();

      Invoke("Think", 5);
    }

    void FixedUpdate()
    {
        //Move
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        //Platform check
        Vector2 FrontVec = new Vector2(rigid.position.x + nextMove*0.2f, rigid.position.y);
        Debug.DrawRay(FrontVec, Vector3.down, new Color(0,1,0));
        RaycastHit2D rayHit = Physics2D.Raycast(FrontVec, Vector3.down, 1, LayerMask.GetMask("Platform"));
            if(rayHit.collider == null){
                turn();
            }
    }

    //몬스터 AI
    void Think()
    {
        nextMove = Random.Range(-1, 2);
      Invoke("Think",5);

      anim.SetInteger("warkSpeed",nextMove);

      if(nextMove !=0)
      spriteRenderer.flipX = nextMove == 1;

    }

        void turn()
    {
        nextMove = nextMove* -1;
        spriteRenderer.flipX = nextMove == 1;
        CancelInvoke();
        Invoke("Think", 5);

    }
}
