using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    public GameObject enemy;
    private Mob mb;
    void Start()
    {
        mb = enemy.GetComponent<Mob>();
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            mb.AttackOn = true;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            mb.AttackOn = false;
        }
    }
}
