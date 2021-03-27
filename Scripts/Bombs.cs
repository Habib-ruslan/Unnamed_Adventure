using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bombs : Entity
{
    public GameObject self;
    public GameObject damageParticale;
    public float radius;
    public float radius2;
    public float damage;
    public float scatter;
    private bool isBoom = false;


    public LayerMask target; 

    public void Boom()
    {
        if (isBoom) return;
        isBoom = true;
        Collider2D[] obj = Physics2D.OverlapCircleAll(transform.position, radius, target);
        Collider2D[] obj2 = Physics2D.OverlapCircleAll(transform.position, radius2, target);
        damages(obj);
        damages(obj2);
        Destroy(self);

    }
    public override void TakeDamage(float damage, GameObject particle) => Boom();
    public override void GetKnockback(float knockPow, float knockTime, float knockDamage, GameObject particale) =>Boom();
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position ,radius);
        Gizmos.DrawWireSphere(transform.position ,radius2);
    }
    private void damages(Collider2D[] cols)
    {
        float damage3;
        for (int i = 0; i < cols.Length; i++)
        {
            damage3 = Random.Range(damage - scatter, damage + scatter);
            cols[i].GetComponent<Entity>().TakeDamage(damage3, damageParticale);
        }
    }
}
