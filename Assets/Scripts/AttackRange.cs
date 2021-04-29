using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    public GameObject enemy;
    private Enemy mb;
    private void Start()
    {
        mb = enemy.GetComponent<Enemy>();
    }
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            mb.AttackOn = true;
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            mb.AttackOn = false;
        }
    }
}
