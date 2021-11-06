using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frog : MonoBehaviour
{
    private Rigidbody2D rb;
    public Transform leftpoint, rightpoint;
    public float speed;

    private bool Faceleft = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.DetachChildren();//解除子物体与父物体的关系，防止子物体对父物体状态的改变而改变
    }

    private void Update()
    {
        FrogMove();
    }

    void FrogMove()
    {
        if (Faceleft)
        {
            rb.velocity = new Vector2(-speed * Time.deltaTime, 0);
            if (transform.position.x < leftpoint.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                Faceleft = false;
            }
        }
        else
        {
            rb.velocity = new Vector2(speed * Time.deltaTime, 0);
            if (transform.position.x > rightpoint.position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
                Faceleft = true;
            }
        }
    }

    /* [SerializeField] private Rigidbody2D rb;
     [SerializeField]  private Animator anim;
     public Collider2D coll;
     public LayerMask ground;
     public float speed;

     // Start is called before the first frame update
     void Start()
     {
         anim = GetComponent<Animator>();
         rb = GetComponent<Rigidbody2D>();
     }

     // Update is called once per frame
     void Update()
     {
         //FrogSwitch();
         FrogMove();
     }

     void FrogMove()
     {
         if (coll.IsTouchingLayers(ground))
         {
             if (transform.position.x > -22 && transform.position.x < -5)
             {
                 rb.velocity = new Vector2(speed * Time.deltaTime, 0);
             }
             else if (transform.position.x < -22 || transform.position.x > -5)
             {
                 speed *= -1;
                 rb.velocity = new Vector2(speed * Time.deltaTime, 0);
             }
         }
     }

    void FrogSwitch()
     {
         if (!coll.IsTouchingLayers(ground))
         {
             anim.SetBool("jumping", true);
         }
         else
         {
             anim.SetBool("jumping", false);
         }
     }*///我的代码
}
