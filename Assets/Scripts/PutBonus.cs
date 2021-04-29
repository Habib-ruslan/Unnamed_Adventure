using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutBonus : MonoBehaviour
{
    public int count;
    public float power; //for effect
    public float time;
    public float time2;
    public GameObject Self;
    private Animator anim;
    [HideInInspector]
    public enum Type
    {
        money,
        regen,
        speed,
        stun,
        other
    };
    public Type type;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(!anim.GetBool("put"))
        {
            if (col.tag == "Player")
            {
                anim.SetBool("put", true);
                Player pl = col.GetComponent<Player>();
                switch ((int)type)
                {
                    case 0:
                        pl.money+=count;
                        pl.money_Txt.text = pl.money.ToString();
                        break;
                    default:
                        pl.Effects((int)type, power, time, time2);
                        break;
                }
                
            }
        }
    }
    public void Dstr()
    {
        Destroy(Self);
    }
}
