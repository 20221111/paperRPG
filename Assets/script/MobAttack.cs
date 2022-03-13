using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobAttack : MonoBehaviour
{

    SpriteRenderer spriteRenderer;
    CapsuleCollider2D collider;
    Rigidbody2D rigid;

    public int HP = 10;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //몬스터가 죽을 경우
    public void DIe()
    {
        spriteRenderer.color = new Color(1, 1, 1, 0.5f);

        spriteRenderer.flipY = true;

        collider.enabled = false;

        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);

        Invoke("DeActive", 5);

    }

    void DeActive()
    {
        gameObject.SetActive(false);
    }

    public void TakeDamage(int damage)
    {
        HP -= damage;
        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);

        if (HP <= 0)
        {
            DIe();
        }
    }
}
