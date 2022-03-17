using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rigid;
    public GameManager gameManager;
    public float maxSpeed;
    public float jumpPower;
    SpriteRenderer spriteRenderer;
    Animator anim;

    private float curtime;
    public float cooltime = 0.5f; //공격 쿨타임 0.5초
    public Transform Pos;
    public Vector2 boxSize;

    public int level = 1;
    public int exp = 0;
    public int[] maxexp = {204, 454, 759, 1132, 1588, 2146, 2827, 3658, 4673, 5911, 7422, 9264, 11511, 14250, 17588, 21656, 26611, 32645, 39993, 48939, 59827, 73076, 89196, 108806, 132655 };
    public int hp = 100, maxHp = 100, maxMP = 100, attack = 10;




    void Awake() {

      rigid = GetComponent<Rigidbody2D>();
      spriteRenderer = GetComponent<SpriteRenderer>();
      anim = GetComponent<Animator>();
        for (int i = 0; i < maxexp.Length; i++)
        {
            if(maxexp[i] <= exp)
            {
                level = i + 1;
                break;
            }
        }
    }
    void Update() {


        //플레이어가 공격을 하는 매소드
        if (curtime <= 0)
        {
            //F버튼을 누르면 공격
            if (Input.GetKey(KeyCode.F))
            {
                //공격

                Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(Pos.position, boxSize, 0);
                foreach (Collider2D collider in collider2Ds)
                {
                    if (collider.tag == "Enemy")
                    {
                        collider.GetComponent<Mobs>().TakeDamage(attack);
                        if (collider.GetComponent<Mobs>().HP <= 0)
                        {
                            MobDIe(collider);
                        }
                    }
                }

                curtime = cooltime;

            }

        }
        else
        {
            curtime -= Time.deltaTime;
        }

        //Jump가 입력되고 점프중이 아닐경우 rigid를 위방향으로 jumpPower만큼 AddForce함
        if (Input.GetButtonDown("Jump") && !anim.GetBool("jumping")){
      rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
      anim.SetBool("jumping", true);
    }
      
    //수평움직임이 종료 될 경우 플레이어의 속도를 낮춤
    if(Input.GetButtonUp("Horizontal")) {
      rigid.velocity = new Vector2(rigid.velocity.normalized.x* 0.5f, rigid.velocity.y);
    }

    //수평 움직임이 감지될 경우 플레이어가 해당방향을 바라보도록 수정 
    if (Input.GetButton("Horizontal"))
        {
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
            if (spriteRenderer.flipX)
            {
                Pos.transform.localPosition = new Vector3(-0.5f, 0, 0);
            }
            else
            {
                Pos.transform.localPosition = new Vector3(0.5f, 0, 0);
            }
        }


        //이동속도가 0.3이하로 내려갈 경우 이동 애니매이션 종료
        if (Mathf.Abs(rigid.velocity.x) < 0.3)
      anim.SetBool("waking", false);
      else
      anim.SetBool("waking", true);


    }
    
    void FixedUpdate()
    {
        //수평움직임이 입력 될 경우 오브젝트의 속도만큼 오브젝트에 힘을 가함
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        //만약 현재 오브젝트의 이동속도가 최대속도 이상일 경우 오브젝트의 속도를 최대속도로 조정함
        if (rigid.velocity.x > maxSpeed)
          rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        else if (rigid.velocity.x < maxSpeed*(-1))
          rigid.velocity = new Vector2(maxSpeed*(-1), rigid.velocity.y);

      //플레이어가 착지했는지 뛰는중인지 판별
      //오브젝트의 y 속도가 음수일 경우 아래방향으로 Raycast를 측정해 "Platform"을 감지
      if(rigid.velocity.y < 0) {
      Debug.DrawRay(rigid.position, Vector3.down, new Color(0,1,0));
      RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform"));
      //측정된 물리객체와의 거리가 0.7이하일 경우 "jumping"을 false함
      if(rayHit.collider != null){
        if(rayHit.distance < 0.7f){
          anim.SetBool("jumping", false);
        }
      }
      }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //물리 객체가 부딪혔을 경우 부딪힌 오브젝트의 태그가 "적"이면 OnDamaged함수 호출
        //적의 공격력을 가져와 OnDamaged함수에 대입
        if (collision.gameObject.tag == "Enemy")
        {
            OnDamaged(collision.transform.position, collision.gameObject.GetComponent<Mobs>().attack);
        }

        //플레이어가 데미지를 입고 적 반대방향으로 밀려나게함 (데미지를 입는중에는 더 이상의 데미지를 입지 않도록 함)
        //플레이어의 채력을 damage만큼 낮춤
        void OnDamaged(Vector2 targetPos,int damage)
        {
            if (!anim.GetBool("jumping"))
            {
                anim.SetBool("jumping", true);
                int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
                rigid.AddForce(new Vector2(dirc, 1) * 7, ForceMode2D.Impulse);
            }
            gameObject.layer = 11;
            spriteRenderer.color = new Color(1, 1, 1, 0.4f);
            hp -= damage;
            anim.SetTrigger("doDamaged");

            if (hp<=0)
            {
                DIe();
            }

            Invoke("OffDamaged", 2);
        }

    }
        //플레이어가 데미지를 전부입고 다시 원래 상태로 되돌아감
        void OffDamaged()
        {
            gameObject.layer = 10;
            spriteRenderer.color = new Color(1, 1, 1, 1);
        }

    public void DIe()
    {
        spriteRenderer.color = new Color(1, 1, 1, 0.5f);
        spriteRenderer.flipY = true;
        Time.timeScale = 0;
    }

    public void MobDIe(Collider2D collider)
    {
        exp += collider.GetComponent<Mobs>().Exp;

        if (maxexp[level - 1] <= exp)
        {
            Levelup();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //동전을 먹으면 점수가 오르게 함
        if (collision.gameObject.tag == "Item")
        {
            bool isBronze = collision.gameObject.name.Contains("Bronze");
            bool isGold = collision.gameObject.name.Contains("Gold");
            bool isSilver = collision.gameObject.name.Contains("Silver");

            if (isBronze)
                gameManager.stagepoint += 10;
            else if (isSilver)
                gameManager.stagepoint += 20;
            else if (isGold)
                gameManager.stagepoint += 30;

            collision.gameObject.SetActive(false);
        }
        //종점에 도착하면 다음스테이지로 이동함
        else if (collision.gameObject.tag == "Finish")
        {
            gameManager.NextStage();
        }
    }

    void Levelup()
    {
        level++;
        Debug.Log("레벨이" + level + "로 올랐습니다");
    }

    //디버그용 메소드
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(Pos.position, boxSize);
    }
}
