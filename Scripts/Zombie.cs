using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Enemy
{
    [Header("Передвижение")]
    public float speed;
    private int X;
    public float KPow, KTime, KDamage;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();  
        Find();
        Right = false;
        hp = MaxHp;
    }
    private void Update()
    {
        Move();  
        Alive();
        if (AttackOn)   anim.SetBool("attack", true);
        else anim.SetBool("attack", false);
    }

    protected override void Move()
    {
        if (isAgr) 
        {
            anim.SetBool("run", true);
            if (!anim.GetBool("attack"))
            {
                if (Right) X = -1;
                else X = 1;
                rb.velocity = new Vector2(speed *X * Time.deltaTime,rb.velocity.y);
            }    
        } 
        else
        {
            anim.SetBool("run", false);
        }
    }
    public override void Attack()
    {
        if (AttackOn)
            pl.TakeDamage(damage, AttackOn);
    }
    public void ToKnockback()
    {
        pl.GetKnockback(KPow, KTime, KDamage, AttackOn);
    }
    public override void isTrue(int num, bool isBool)
    {
        isAgr = isBool;
        if(pl.transform.position.x <= transform.position.x)
        {
            if(!Right)
            {
                Flip();
            }
        }
        else
        {
            if (Right)
            {
                Flip();
            }
        } 
    }
}
