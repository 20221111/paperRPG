using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;


public class Mobs : MonoBehaviour
{
    Rigidbody2D rigid;
    CapsuleCollider2D mobscollider;
    SpriteRenderer spriteRenderer;
    Animator anim;
    string mobData;
    public string mobName;
    public int nextMove;
    public float HP;
    public float attack;
    public int Exp;
    public float Money;


    //초기화
    public virtual void Start() 
    {
      rigid = GetComponent<Rigidbody2D>();
      spriteRenderer = GetComponent<SpriteRenderer>();
      mobscollider = GetComponent<CapsuleCollider2D>();

        mobName = "mob";
        HP = 100;
        attack = 50;
        Exp = 100;
        Money = 10;

    Think();
        Invoke("Think", 5);
    }

    public void FixedUpdate()
    {
        //Move
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        //Platform check
        Vector2 FrontVec = new Vector2(rigid.position.x + nextMove*0.2f, rigid.position.y-0.5f);
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
        if (nextMove != 0)
            spriteRenderer.flipX = nextMove == 1;
        CancelInvoke();
        Invoke("Think",5);

    }

    //몬스터의 진행방향을 바꿈
    public void turn()
    {
        nextMove *= -1;
        spriteRenderer.flipX = nextMove == 1;
        Invoke("Think", 5);

    }

    //몬스터를 사망처리함
    public void DIe()
    {
        SpawnManager._instance.isSpawn[int.Parse(transform.parent.name) -1] = false;
        SpawnManager._instance.maxMob -= 1;
        spriteRenderer.color = new Color(1, 1, 1, 0.5f);
        spriteRenderer.flipY = true;
        mobscollider.enabled = false;
        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        Destroy(gameObject, 3f);

    }


    //플레이어에게 피격시 자신의 채력을 낮춤
    public void TakeDamage(float damage)
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
