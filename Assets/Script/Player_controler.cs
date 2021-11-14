using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_controler : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Rigidbody2D rb;//private声明私有变量，无法在窗口中看到，[SerializeField] 则是将私有变量显示出来
    [SerializeField] private Animator anim;
    public Collider2D coll;
    public float speed = 3;
    public float jump_force;
    public LayerMask ground;
    public LayerMask up_block;
    public int cherry_number = 0;
    [SerializeField] private int jump_time = 0;
    private bool isHurt;

    public Text CherryNum;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();//自动获取
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!anim.GetBool("isHurt"))
        {
            PlayerMove();
        }
        SwitchAnim();
    }

    void PlayerMove()
    {
        float Horizontal_move = Input.GetAxis("Horizontal");//水平方向-1/0/1
        float face_direction = Input.GetAxisRaw("Horizontal");//垂直方向-1/0/1

        if(Horizontal_move != 0)//角色移动
        {
            rb.velocity = new Vector2(Horizontal_move*speed * Time.fixedDeltaTime, rb.velocity.y);//velocity是速度
        }
        anim.SetFloat("running", Mathf.Abs(face_direction));
        if (face_direction != 0)
        {
            rb.transform.localScale = new Vector3(face_direction, 1, 1);
        }

        if(Input.GetButtonDown("Jump"))//角色跳动
        {
            if (jump_time < 2)
            {
                rb.velocity = new Vector2(rb.velocity.x, jump_force * Time.fixedDeltaTime);
                anim.SetBool("jumping", true);
                jump_time++;
            }
        }
        crouch();
    }

    void SwitchAnim()
    {
        if (anim.GetBool("jumping"))
        {
            if(rb.velocity.y  < 0)
            {
                anim.SetBool("jumping", false);
                anim.SetBool("falling", true);
            }
        }
        else if (coll.IsTouchingLayers(ground))
        {
            anim.SetBool("falling", false);
            anim.SetBool("idel", true);
            jump_time = 0;
        }

        if (anim.GetBool("isHurt"))
        {
            if (Mathf.Abs(rb.velocity.x) < 0.1f)
            {
                anim.SetBool("isHurt", false);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)//与敌人碰撞
    {
        if(collision.gameObject.tag == "Enemy")
        {
            if(anim.GetBool("falling") && !anim.GetBool("jumping"))
            {
                Destroy(collision.gameObject);
                rb.velocity = new Vector2(rb.velocity.x, jump_force * Time.fixedDeltaTime);
                anim.SetBool("jumping", true);
                anim.SetBool("falling", false);
                jump_time = 1;
            }
            else
            {
                anim.SetBool("isHurt", true);
                if (transform.position.x < collision.gameObject.transform.position.x)
                {
                    rb.velocity = new Vector2(-5, 2);
                }
                else if (transform.position.x > collision.gameObject.transform.position.x)
                {
                    rb.velocity = new Vector2(5, 2);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)//采集
    {
        if (collision.tag == "Collection") 
        {
            Destroy(collision.gameObject);
            cherry_number += 1;
            CherryNum.text = cherry_number.ToString();
        }
    }

        /*public void crouch()
        {
            if (coll.IsTouchingLayers(up_block))//切换成蹲下姿态
            {
                anim.SetBool("crouching", true);
            }
        }*/
    public void crouch()
    {
        if (Input.GetButtonDown("Crouch"))
        {
            anim.SetBool("crouching", true);
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            anim.SetBool("crouching", false);
        }
    }
}
