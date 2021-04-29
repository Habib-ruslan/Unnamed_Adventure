using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boxes : Entity
{
    public GameObject[] loot;
    public float[] ChanceLoot;
    public GameObject self;
    public GameObject particle;
    private bool isBroken=false;

    public override void TakeDamage(float damage, GameObject particle) => Broken();
    public override void GetKnockback(float knockPow, float knockTime, float knockDamage, GameObject particale) => Broken();

    public void Broken()
    {
        if (isBroken) return;
        isBroken = true;
        float f = 0;
        float r = Random.Range(0, 100f);
        for (int i = 0; i < loot.Length; i++)
        {
            if ((r > f) && (r <= f + ChanceLoot[i]))
            {
                Instantiate(loot[i], transform.position, Quaternion.identity);
                Instantiate(particle, transform.position, Quaternion.identity);
            }
            f+=ChanceLoot[i]; 
        }
        Destroy(self);
    }
}
