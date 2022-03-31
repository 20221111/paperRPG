using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    Rigidbody2D rigid;
    public GameManager gameManager;
    public float maxSpeed;
    public float jumpPower;
    SpriteRenderer spriteRenderer;
    Animator anim;
    Animator attackAnim;
    public string currentMapName; //TransferMap 스크립트에 있는 transferMapName 변수의 값을 저장
    static public Player instance; //TransferMap 할때 플레이어 무한 생성 막기 위해

    private float curtime;
    public float cooltime = 0.5f; //공격 쿨타임 0.5초
    public Transform Pos;
    public Vector2 boxSize;

    //플레이어 능력치
    public int level = 1;
    public int exp = 0;
    public int[] maxexp = { 2040, 4536, 7589, 11321, 15883, 21457, 28265, 36578, 46726, 59109, 74217, 92644, 115112, 142502, 175884, 216559, 266108, 326454, 399934, 489389, 598268, 730762, 891964, 1088056, 1326547 };
    public float hp = 1000, maxHp = 1000, mp = 850, maxMp = 850, attackDamage = 100;
    public float stress = 1000, MaxStress = 1000;

    public nmy_Item equipmunt; //플레이어가 장착하고 있는 아이탬

    public Slider[] infoBar;
    public Text TextLV;
    public Text TextStrees;
    public Text Textdamage;

    //맵 이동시 플레이어 이동제한
    public bool isControl;


    void Awake() {

        if(instance == null)
        {
            DontDestroyOnLoad(this.gameObject); //맵 이동할때 플레이어 사라지지않게
            instance = this;

        }
        else
        { 
            Destroy(this.gameObject);
        }

        isControl = true; //플레이어의 움직임 허용

        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        attackAnim = transform.GetChild(0).GetComponent<Animator>();

        //플레이어 레벨 초기화
        for (int i = 0; i < maxexp.Length; i++)
        {
            if (maxexp[i] <= exp)
            {
                level = i + 1;
                break;
            }
        }
        //플레이어 레벨에 맞춰 능력치 조정
        maxHp += (100 * (level - 1));
        hp = maxHp;
        maxMp += (85 * (level - 1));
        mp = maxMp;
        attackDamage += (10 * (level - 1));

        //플레이어 UI 초기화
        UItextController();
        UIBarController();



    }
    void Update() {

        //플레이어의 채력을 재생
        playerHPRegen();
        //플레이어의 정신력을 관리
        PlayerStressManager();




        //플레이어가 공격을 하는 매소드
        if (curtime <= 0)
        {
            //F버튼을 누르면 공격
            if (Input.GetButtonDown("Fire1"))
            {
                //공격
                playerAttack();

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
        //플레이어의 움직임이 허용된(true일) 경우
        if (isControl)
        {
            //수평움직임이 입력 될 경우 오브젝트의 속도만큼 오브젝트에 힘을 가함
            float h = Input.GetAxisRaw("Horizontal");
            rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

            //만약 현재 오브젝트의 이동속도가 최대속도 이상일 경우 오브젝트의 속도를 최대속도로 조정함
            if (rigid.velocity.x > maxSpeed)
                rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
            else if (rigid.velocity.x < maxSpeed * (-1))
                rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);

            //플레이어가 착지했는지 뛰는중인지 판별
            //오브젝트의 y 속도가 음수일 경우 아래방향으로 Raycast를 측정해 "Platform"을 감지
            if (rigid.velocity.y < 0)
            {
                Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
                RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform"));
                //측정된 물리객체와의 거리가 0.7이하일 경우 "jumping"을 false함
                if (rayHit.collider != null)
                {
                    if (rayHit.distance < 0.7f)
                    {
                        anim.SetBool("jumping", false);
                    }
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

    }


    /*--------------------플레이어의 전투를 담당하는 메소드--------------------*/

    //플레이어가 데미지를 입고 적 반대방향으로 밀려나게함 (데미지를 입는중에는 더 이상의 데미지를 입지 않도록 함)
    //플레이어의 채력을 damage만큼 낮춤
    void OnDamaged(Vector2 targetPos, float damage)
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
        if (stress>0)
        {
            stress -= 100;
        }
        else
        {
            stress = 0;
        }
        UIBarController();
        anim.SetTrigger("doDamaged");

        if (hp <= 0)
        {
            DIe();
        }

        Invoke("OffDamaged", 2);
    }
    //플레이어가 데미지를 전부입고 다시 원래 상태로 되돌아감
    void OffDamaged()
        {
            gameObject.layer = 10;
            spriteRenderer.color = new Color(1, 1, 1, 1);
        }

    //플레이어가 죽으면 시간을 멈춤
    public void DIe()
    {
        spriteRenderer.color = new Color(1, 1, 1, 0.5f);
        spriteRenderer.flipY = true;
        Time.timeScale = 0;
    }

    //플레이어의 현재 공격력을 계산하는 메소드(플레이어 공격력 + 정신력 보정 + 무기공격력)
    public float PlayerAtackDamage(float playerDamage, float stress)
    {
        float temp = playerDamage;

        if (equipmunt != null)
        {
            temp += equipmunt.weaponDamage;
        }

        if (stress <= 400)
        {
            temp = temp / 2;
        }
        return temp;
    }

    //플레이어가 공격 버튼을 누를경우 실행되는 메소드
    public void playerAttack()
    {
        attackAnim.SetTrigger("Attack");
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(Pos.position, boxSize, 0);
        foreach (Collider2D collider in collider2Ds)
        {
            if (collider.tag == "Enemy")
            {
                collider.GetComponent<Mobs>().TakeDamage(PlayerAtackDamage(attackDamage, stress));
                Debug.Log(PlayerAtackDamage(attackDamage, stress));
                if (collider.GetComponent<Mobs>().HP <= 0)
                {
                    MobDIe(collider);
                }
                UItextController();
            }
        }

        curtime = cooltime;
    }

    //몬스터가 죽을경우 호출되는 함수
    public void MobDIe(Collider2D collider)
    {
        exp += collider.GetComponent<Mobs>().Exp; //몬스터의 경험치를 가져와 플레이어에게 지급

        if (maxexp[level - 1] <= exp)//경험치량에 맞춰서 레벨업 해줌
        {
            Levelup();
        }

        UIBarController(); //barUI를 업데이트함
    }


    /*--------------------플레이어의 리소스(체력, 레벨, 마나, 장비)를 관리하는 메소드--------------------*/
    void Levelup()
    {
        for (int i = 0; i < maxexp.Length; i++)
        {
            if (maxexp[i] > exp)
            {
                level = i + 1;
                maxHp += (100 * (level - 1));
                hp = maxHp;
                maxMp += (85 * (level - 1));
                mp = maxMp;
                attackDamage += (10 * (level - 1));
                Debug.Log("레벨이" + level + "로 올랐습니다");
                UItextController();
                break;
            }
        }
    }

    //플레이어 하위 오브젝트로 장착된 아이탬을 생성
    public void PlayerEquip(nmy_Item equipmunt)
    {
        Instantiate(equipmunt.itemPrefab, this.transform);
        this.equipmunt = equipmunt;
        Debug.Log(equipmunt.name);
    }

    //장비를 해제 할 경우 플레이어 하위 오브젝트 장비를 삭제함 
    public void PlayerUnEquip(nmy_Item equipmunt)
    {
        gameObject.transform.Find(equipmunt.name + "(Clone)").GetComponent<nmy_Item>().Destroy();
        this.equipmunt = null;

    }

    //플레이어 채력이 재생되는 매소드
    public void playerHPRegen()
    {
        if (hp != maxHp)
        {
            //초당 최대 채력의 1/500 회복
            hp += Time.deltaTime * (float)(maxHp / 500);//실행시 회복량이 1을 넘지 못해 값이 전부 버려져 회복이 되지 않음

            //채력을 회복했는데 최대 채력보다 크다면 현재채력을 최대채력으로 변경
            if (hp > maxHp)
            {
                hp = maxHp;
            }
            UIBarController();

        }
    }

    //플레이어 정신력 관리 매소드
    public void PlayerStressManager()
    {

        if (currentMapName == "Town")
        {
            if (stress != MaxStress)
            {
                //초당 최대 정신력의 1/200 회복
                stress += Time.deltaTime * (float)(MaxStress / 200);

                //정신력을 회복했는데 최대 정신력보다 크다면 현재정신력을 최대정신력으로 변경
                if (stress > MaxStress)
                {
                    stress = MaxStress;
                }
                UItextController();

            }
        }
        else
        {
            if (stress >= 0)
            {
                //초당 최대 정신력의 1/600 씩 감소
                stress -= Time.deltaTime * (float)(MaxStress / 600);

                //정신력이 감소했는데 0보다 작다면 현재정신력을 0으로 변경
                if (stress < 0)
                {
                    Debug.Log("정신력 최하치 경고");
                    stress = 0;
                }
                UItextController();

            }
        }
    }



    /*--------------------플레이어의 UI를 담당하는 메소드--------------------*/
    void UIBarController()
    {

        infoBar[0].value = ((float)hp / (float)maxHp);
        //현재 경험치량에 맞게 경험치 바를 조정
        if (level == 1)
        {
            infoBar[2].value = (float)exp / maxexp[0];
        }
        else
        {
            infoBar[2].value = ((float)exp - (maxexp[level - 2])) / ((float)(maxexp[level - 1] - maxexp[level - 2]));
        }
    }

    void UItextController()
    {
        TextLV.text = "LV." + level;
        TextStrees.text = (int)(stress / MaxStress * 100) + " / 100";
        Textdamage.text = "" + (int)PlayerAtackDamage(attackDamage, stress);
    }



    /*--------------------플레이어 디버그용 메소드 (없어도 실행에 문제가 없음)--------------------*/
    //디버그용 메소드 (Sceen 뷰에서 플레이어의 평타 공격 범위를 보여줌)
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(Pos.position, boxSize);
    }

}
