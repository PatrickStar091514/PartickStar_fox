using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator Anim; //protected仅限在子父类中使用
    protected AudioSource deathAudio;

    // Start is called before the first frame update
    protected virtual void Start() //virtual可以让父类方法随时被子类改写
    {
        deathAudio = GetComponent<AudioSource>();
        Anim = GetComponent<Animator>();
    }

    public void Death()
    {
        deathAudio.Play();
        Anim.SetTrigger("death");
    }

    public void JumpOn()
    {
        Destroy(gameObject);
    }
}
