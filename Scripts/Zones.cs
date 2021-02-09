using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zones : MonoBehaviour
{
    public bool Right;
    public bool Agr;
    public GameObject otherZone;
    private Zones zn;
    public GameObject enemy;
    private Mob mob;
    void Start()
    {
        mob = enemy.GetComponent<Mob>();
        zn = otherZone.GetComponent<Zones>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag =="Player")
        {
            mob.Agr = Agr = true;
            zn.Agr = false; 
            if (mob.Right !=Right)
            { 
                Right=!Right;
                zn.Right = !zn.Right;
                mob.Flip();
            }
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag =="Player")
        {
            Agr = false;
            if (!zn.Agr) {mob.Agr = false;}
        }
    }
}
