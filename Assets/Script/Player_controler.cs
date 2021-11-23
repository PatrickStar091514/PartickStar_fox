using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player_controler : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Rigidbody2D rb;//private声明私有变量，无法在窗口中看到，[SerializeField] 则是将私有变量显示出来
    [SerializeField] private Animator anim;
    public Collider2D coll;
    public Collider2D headColl;
    public AudioSource jumpAudio;
    public Transform HeadCheck;

    public float speed = 3;
    public float jump_force;
    [SerializeField] private int jump_time = 0;
    private bool isHurt;
    private float rbGravity;

    public LayerMask ground;
    public LayerMask up_block;
    public int cherry_number = 0;
    public Text CherryNum;

    public GameObject EnterSign;

    private bool IsClimb = false;
    public float ClimbSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();//自动获取
        anim = GetComponent<Animator>();
        rbGravity = rb.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if(!anim.GetBool("isHurt"))
        {
            PlayerMove();
        }
        SwitchAnim();
    }

    //角色动作
    void PlayerMove()
    {
        float Horizontal_move = Input.GetAxis("Horizontal");//水平方向-1/0/1
        float face_direction = Input.GetAxisRaw("Horizontal");//垂直方向-1/0/1

        //角色移动
        if(Horizontal_move != 0)
        {
            rb.velocity = new Vector2(Horizontal_move*speed * Time.deltaTime, rb.velocity.y);//velocity是速度
        }
        anim.SetFloat("running", Mathf.Abs(face_direction));
        if (face_direction != 0)
        {
            rb.transform.localScale = new Vector3(face_direction, 1, 1);
        }

        //角色跳动
        if(Input.GetButtonDown("Jump") && !IsClimb)
        {
            if (jump_time < 2)
            {
                jumpAudio.Play();
                rb.velocity = new Vector2(rb.velocity.x, jump_force * Time.deltaTime);
                anim.SetBool("jumping", true);
                jump_time++;
            }
        }
        crouch();
        //Climb();
    }

    //动画切换
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

    //与敌人碰撞
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
             // frog frog_enemy = collision.gameObject.GetComponent<frog>(); //声明frig这个类作为局部变量，可以做到调用frog类中的方法
            Enemy enemy = collision.gameObject.GetComponent<Enemy >();
            if (anim.GetBool("falling") && !anim.GetBool("jumping"))
            {
                 //frog_enemy.Death();
                enemy.Death();
                rb.velocity = new Vector2(rb.velocity.x, jump_force * Time.deltaTime);
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

    //碰撞触发器
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //采集
        if (collision.tag == "Collection") 
        {
            Destroy(collision.gameObject);
            cherry_number += 1;
            CherryNum.text = cherry_number.ToString();
        }

        //进门
        else if (collision.tag == "Door")
        {
            EnterSign.SetActive(true);
        }
        else if(collision.tag == "Ladder")
        {
            IsClimb = true;
        }

        //死亡 
        else if (collision.tag == "DeadLine")
        {
            GetComponent<AudioSource>().enabled = false;
            Invoke("Restar", 1f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Door")
        {
            EnterSign.SetActive(false);
        }
        else if (collision.tag == "Ladder")
        {
            IsClimb = false;
            rb.gravityScale = rbGravity; 
        }
    }

    //下蹲
    public void crouch()
    {
        if (!Physics2D.OverlapCircle(HeadCheck.position, 0.2f, up_block))
        {
            if (Input.GetButton("Crouch"))
            {
                anim.SetBool("crouching", true);
                headColl.enabled = false;
            }
            else
            {
                anim.SetBool("crouching", false);
                headColl.enabled = true;
            }
        }
    }

    void Climb()
    {
        if (IsClimb)
        {
            float climbForce = Input.GetAxis("Vertical");
            if (climbForce > 0.5f || climbForce < -0.5f)
            {
                rb.gravityScale = 0.0f;
                rb.velocity = new Vector2(rb.velocity.x, climbForce*ClimbSpeed);
            }
        }
    }

    void Restar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }
}
