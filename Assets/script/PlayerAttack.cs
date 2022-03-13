using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // Start is called before the first frame update

    Rigidbody2D myrigidbody;
    Animator animator;


    private float curtime;
    public float cooltime = 0.5f; //���� ��Ÿ�� 0.5��

    public Transform Pos;
    public Vector2 boxSize;
    void Start()
    {
        myrigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
            if (curtime <= 0)
            {
                    //F��ư�� ������ ����
                if (Input.GetKey(KeyCode.F))
                {
                //����

                Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(Pos.position, boxSize, 0);
                foreach (Collider2D collider in collider2Ds)
                {
                    Debug.Log(collider.tag);
                    if (collider.tag == "Enemy")
                    {
                        collider.GetComponent<MobAttack>().TakeDamage(1);
                    }
                }

                //animator.SetTrigger("atk");
                curtime = cooltime;

                }

            }
            else
            {
                curtime -= Time.deltaTime;
            }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(Pos.position, boxSize);
    }
 
}
