using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrapnel : MonoBehaviour
{
    public GameObject self;
    public GameObject[] splinters;
    public GameObject damageParticale;
    private Animator anim;

    public float speed;
    public float speed_Y;

    public bool isEnemyBullet;
    public LayerMask target;
    public float radius;
    float damage;
    public float Damage;
    public int crit;

    public float ChildDamage;
    public float ChiclDeathTime;

    public float deathTime;

    private void Start()
    {
        anim = GetComponent<Animator>();
        Destroy(self, deathTime);
    }
    
    private void FixedUpdate()
    {
        transform.Translate(speed*Time.deltaTime,speed_Y,0f);
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(isEnemyBullet)
        {
            if(col.tag =="Player")
            {
                col.GetComponent<Player>().TakeDamage(SetRandomDamage(), true);
            }
            Split();
        }
        else if(col.tag =="Enemy" || col.tag=="Wall" || col.tag=="Ground"||col.tag=="Boxes"||col.tag=="Bomb")
        {
            Collider2D[] obj = Physics2D.OverlapCircleAll(transform.position, radius, target);
            for (int i = 0; i< obj.Length; i++)
            {
                obj[i].GetComponent<Entity>().TakeDamage(SetRandomDamage(), damageParticale);
            }
            Split();
        }
        
    }
    private float SetRandomDamage()
    {
        damage = (int) Random.Range(Damage-crit, Damage+crit);
        return damage;
    }
    public void _Destroy()
    {
        Destroy(self);
    }
    private void Split()
    {
        anim.SetBool("isDestroy", true);
        speed_Y = speed = 0;
        foreach (GameObject splinter in splinters)
            {
                Bullets bullet =splinter.AddComponent<Bullets>();
                bullet.speed = 0;
                bullet.damage = ChildDamage;
                bullet.deathTime = ChiclDeathTime;
                bullet.self = splinter;
                bullet.damageParticale = damageParticale;
                bullet.target = target;
                bullet.isEnemyBullet = isEnemyBullet;
            }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color =Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
