using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bombs : MonoBehaviour
{
    public GameObject self;
    public GameObject damageParticale;
    public float radius;
    public float radius2;
    public float damage;
    public float damage2;

    public LayerMask[] target; 

    public void Boom()
    {
        float damage3;
        Collider2D[] en = Physics2D.OverlapCircleAll(transform.position, radius, target[0]);
        for (int i =0; i < en.Length; i++)
        {
            damage3 = Random.Range(damage, damage2); 
            en[i].GetComponent<Mob>().TakeDamage(damage3, damageParticale);
        }
        Collider2D pl = Physics2D.OverlapCircle(transform.position, radius, target[1]);
        damage3 = Random.Range(damage, damage2);
        if (pl !=null)
            pl.GetComponent<Player>().OtherDamage(damage3, true);

        Collider2D[] en2 = Physics2D.OverlapCircleAll(transform.position, radius2, target[0]);
        for (int i =0; i < en2.Length; i++)
        {
            damage3 = Random.Range(damage, damage2);
            en2[i].GetComponent<Mob>().TakeDamage(damage3, damageParticale);
        }
        pl = Physics2D.OverlapCircle(transform.position, radius2, target[1]);
        damage3 = Random.Range(damage, damage2);
        if (pl !=null)
            pl.GetComponent<Player>().OtherDamage(damage3, true);
        Destroy(self);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position ,radius);
        Gizmos.DrawWireSphere(transform.position ,radius2);
    }
}
