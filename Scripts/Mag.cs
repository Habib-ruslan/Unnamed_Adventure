using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mag : Player
{
    public GameObject[] MagicBullet;
    public Transform AttackPoint; 
    public Quaternion Offset;
    private Animation an;
    //public Sprite[] skins;
    //public GameObject stick;
    short k;

    void Awake()
    {
        Find();
        skill[2].Timer = 1f;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        hp = Maxhp;
    }

    void Update()
    {
        if (!Stun)
        {
            Jump();
            Run();
            if (Input.GetKey(KeyCode.X) || Input.GetKey(KeyCode.C) || Input.GetKey(KeyCode.V))
            {

                anim.SetBool("Attack", true);
                if (Input.GetKey(KeyCode.X))
                {
                    k = 0;
                    anim.SetFloat("speed", 1f);
                    skill[0].Timer = 1f;
                }
                else if(Input.GetKey(KeyCode.C))
                {
                    k = 1;
                    anim.SetFloat("speed", 2f);
                    skill[0].Timer = 0.5f;
                }
                else if(Input.GetKey(KeyCode.V))
                {
                    k = 2;
                    anim.SetFloat("speed", 0.75f);
                    skill[0].Timer = 1.35f;
                }
            }
        }
        else
        {
            anim.SetBool("run", false);
            anim.SetBool("Attack", false);
            //anim.SetBool("OnAir", false);
        }
        /*
        else if(Input.GetKey(KeyCode.G))
        {
            stick.GetComponent<SpriteRenderer>().sprite = skins[1];
        }
        */
    }
    public void Attack()
    {
        skill[0].image.fillAmount = 0;
        if(transform.localScale.x<0)
        {
            Offset.z +=180f;
            Instantiate(MagicBullet[k], AttackPoint.position, Offset);
            Offset.z-=180f;
        }
        else
        {
            Instantiate(MagicBullet[k], AttackPoint.position, Offset);
        }
        anim.SetBool("Attack", false);
    }
}
