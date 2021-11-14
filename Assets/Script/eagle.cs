using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eagle : MonoBehaviour
{
    private Rigidbody2D rb;
    public Transform uppoint, downpoint;
    public Collider2D coll;
    public int flySpeed;
    [SerializeField] private Animator anim;

    bool upFly = true;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        transform.DetachChildren();
    }

    // Update is called once per frame
    void Update()
    {
        EagleFly();
    }

    void EagleFly()
    {
        if (upFly)
        {
            rb.velocity = new Vector2(rb.velocity.x, flySpeed);
            if(transform.position.y >= uppoint.position.y)
            {
                upFly = false;
            }
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, -flySpeed);
            if (transform.position.y <= downpoint.position.y)
            {
                upFly = true;
            }
        }
    }
}
