using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalZombie : Mob
{
    [Header("Передвижение")]
    public float speed;
    int X;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Find();
        anim = GetComponent<Animator>(); 
        hp = MaxHp;
        if (Knock_Resist == 0)
            Knock_Resist = 1;
    }
    void Update()
    {
        Move();  
        Alive();
        if (AttackOn)   anim.SetBool("fight", true);
        else anim.SetBool("fight", false);
    }
    private void Inverse(ref bool a) {a = !a;}
    public void Anim_attack()
    {
        if (AttackOn)
            pl.OtherDamage(damage, AttackOn);
    }
    protected override void Move()
    {
        if (Agr) 
        {
            anim.SetBool("run", true);
            if (Right) X = -1;
            else X = 1;
            rb.velocity = new Vector2(speed *X * Time.deltaTime,rb.velocity.y);
        } 
        else
        {
            anim.SetBool("run", false);
        }
    }
}
