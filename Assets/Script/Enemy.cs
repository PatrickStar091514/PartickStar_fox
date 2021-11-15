using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator Anim; //protected仅限在子父类中使用

    // Start is called before the first frame update
    protected virtual void Start() //virtual可以让父类方法随时被子类改写
    {
        Anim = GetComponent<Animator>();
    }

    public void Death()
    {
        Anim.SetTrigger("death");
    }

    public void JumpOn()
    {
        Destroy(gameObject);
    }
}
