using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zones : MonoBehaviour
{
    public string Target;
    public int num;

    public GameObject mob;
    private Enemy en;


    void Start()
    {
        en = mob.GetComponent<Enemy>();
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if(col.tag==Target) en.isTrue(num, true);
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if(col.tag==Target) en.isTrue(num, false);
    }

    
}
