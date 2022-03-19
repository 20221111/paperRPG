using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mobs : MonoBehaviour
{
    Rigidbody2D rigid;
    CapsuleCollider2D mobscollider;
    SpriteRenderer spriteRenderer;
    Animator anim;
    public int nextMove;
    public int HP;
    public int attack;
    public int Exp;
    public int Money;

    //초기화
    public virtual void Start() 
    {
      rigid = GetComponent<Rigidbody2D>();
      anim = GetComponent<Animator>();
      spriteRenderer = GetComponent<SpriteRenderer>();
      mobscollider = GetComponent<CapsuleCollider2D>();

        HP = 10;
        attack = 10;
        Exp = 1000;
        Money = 100;

    Think();

      Invoke("Think", 5);
    }

    public void FixedUpdate()
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
    public void Think()
    {
        nextMove = Random.Range(-1, 2);
      Invoke("Think",5);

      anim.SetInteger("warkSpeed",nextMove);

      if(nextMove !=0)
      spriteRenderer.flipX = nextMove == 1;

    }

    //몬스터의 진행방향을 바꿈
    public void turn()
    {
        nextMove = nextMove* -1;
        spriteRenderer.flipX = nextMove == 1;
        CancelInvoke();
        Invoke("Think", 5);

    }

    //몬스터를 사망처리함
    public void DIe()
    {
        SpawnManager._instance.isSpawn[int.Parse(transform.parent.name) -1] = false;
        spriteRenderer.color = new Color(1, 1, 1, 0.5f);
        spriteRenderer.flipY = true;
        mobscollider.enabled = false;
        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        Invoke("DeActive", 5);

    }

    //몬스터 객체를 비활성화함
    public void DeActive()
    {
        gameObject.SetActive(false);

    }

    //플레이어에게 피격시 자신의 채력을 낮춤
    public void TakeDamage(int damage)
    {
        HP -= damage;
        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);

        //자신의 채력이 0이하가 될 경우 사망처리함
        if (HP <= 0)
        {
            DIe();
        }
    }
}
