using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    public float speed;
    public bool Enemy_sBullet;
    public float damage;
    public int damage_crit;
    public GameObject self;
    public float deathTime;
    public float stun;
    public float radius;
    public LayerMask target;
    public GameObject damageParticale;

    void Start()
    {
        Destroy(self, deathTime);
    }

    void Update()
    {
        transform.Translate(speed*Time.deltaTime,0f,0f);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (Enemy_sBullet)
        {

            if (col.tag =="Player")
            {
                Damage();
                col.GetComponent<Player>().OtherDamage(damage, true);
                Destroy(self);
            }
            else if (col.tag == "Bomb")
            {
                col.GetComponent<Bombs>().Boom();
            }
        }
        else
        {
            if(col.tag =="Enemy")
            {
                if (radius > 0)
                {
                    Radius_Damage();
                }
                Damage();
                if (stun!=0)
                    col.GetComponent<Mob>().OtherStun(stun);
                col.GetComponent<Mob>().TakeDamage(damage, damageParticale);
                Destroy(self);
            }
            else if (col.tag == "Ground" && radius > 0)
            {
                Radius_Damage();
            }
            else if (col.tag == "Boxes")
            {
                col.GetComponent<Boxes>().DestroyOther();
            }
            else if (col.tag == "Bomb")
            {
                col.GetComponent<Bombs>().Boom();
            }
        }
    }

    void Damage()
    {
        damage += Random.Range(0, damage_crit);
    }
    void Radius_Damage()
    {
        Collider2D[] mb = Physics2D.OverlapCircleAll(transform.position, radius, target);
        for (int i = 0; i< mb.Length; i++)
        {
            mb[i].GetComponent<Mob>().TakeDamage(damage, damageParticale);
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
