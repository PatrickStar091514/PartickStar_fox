
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frog : MonoBehaviour
{
    private Rigidbody2D rb;
    public Transform leftpoint, rightpoint;
    public float speed;
    public float jumpSpeed;
    [SerializeField] private Animator anim;
    public LayerMask ground;
    public Collider2D coll;

    private bool Faceleft = true;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        transform.DetachChildren();//解除子物体与父物体的关系，防止子物体对父物体状态的改变而改变
    }

    private void Update()
    {
        // 添加事件后，因为事件的方法是跟随动画的进行而被调用的，所以不用在update中再次调用
        FrogSwitch();
    }

    void FrogMove()
    {
        if (coll.IsTouchingLayers(ground))//添加事件后，程序在执行玩idle的动画后，只会执行FrogMove()一次，所以不能将动作切换的方法写在这里面
        {
            anim.SetBool("jumping", true);
            if (Faceleft)
            {
                rb.velocity = new Vector2(-speed, jumpSpeed);
                if (transform.position.x < leftpoint.position.x)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                    Faceleft = false;
                }
            }
            else
            {
                rb.velocity = new Vector2(speed, jumpSpeed);
                if (transform.position.x > rightpoint.position.x)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                    Faceleft = true;
                }
            }
        }
/*        else
        {
            if (anim.GetBool("jumping"))
            {
                if (rb.velocity.y < 0.1)
                {
                    anim.SetBool("jumping", false);
                    anim.SetBool("falling", true);
                }
            }
            else if (coll.IsTouchingLayers(ground))
            {
                anim.SetBool("falling", false);
            }
        }*/
    }
        void FrogSwitch()
        {
            if (anim.GetBool("jumping"))
            {
                if (rb.velocity.y < 0.1)
                {
                    anim.SetBool("jumping", false);
                    anim.SetBool("falling", true);
                }
            }
            else if (coll.IsTouchingLayers(ground))
            {
                anim.SetBool("falling", false);
            }
        }
}
