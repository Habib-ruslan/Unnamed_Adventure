using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    public float speed;
    public bool isEnemyBullet;
    public float damage;
    public int damage_crit;
    public GameObject self;
    public float deathTime;
    public float stun;
    [Range(0, Mathf.Infinity)]
    public float radius;
    public LayerMask target;
    public GameObject damageParticale;
    public float rayDistance;


    private void Start()
    {
        Destroy(self, deathTime);
    }

    private void FixedUpdate()
    {
        transform.Translate(transform.right*Time.fixedDeltaTime*speed, Space.World);
        RaycastHit2D ray = Physics2D.Raycast(transform.position, transform.right, rayDistance, target); 
        if (ray)
        {
            Damage();
            ray.collider.GetComponent<Entity>().TakeDamage(damage, damageParticale);
            if (radius > 0) Exploution();
            Destroy(self);
        }
    }
    private void Exploution()
    {
        Collider2D[] obj = Physics2D.OverlapCircleAll(transform.position, radius, target);
        for (int i = 0; i < obj.Length; i++)
        {
            obj[i].GetComponent<Entity>().TakeDamage(damage, damageParticale);
        }
        Destroy(self);
    }
    /*
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (isEnemyBullet)
        {

            if (col.tag =="Player")
            {
                Damage();
                col.GetComponent<Player>().TakeDamage(damage, true);
                Destroy(self);
            }
            else if (col.tag == "Bomb")
            {
                col.GetComponent<Bombs>().Boom();
            }
            else if (col.tag == "Boxes")
            {
                col.GetComponent<Boxes>().DestroyOther();
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
                    col.GetComponent<Enemy>().OtherStun(stun);
                col.GetComponent<Enemy>().TakeDamage(damage, damageParticale);
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
    */
    private void Damage()
    {
        damage += Random.Range(0, damage_crit);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
