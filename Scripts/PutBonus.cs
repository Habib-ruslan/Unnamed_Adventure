using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutBonus : MonoBehaviour
{
    //Переменные
    public int count;
    public string type;
    public float power;
    public float time;
    public float time2;
    public GameObject Self;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(!anim.GetBool("put"))
        {
            if (col.tag == "Player")
            {
                anim.SetBool("put", true);
                Player pl = col.GetComponent<Player>();
                switch (type)
                {
                    case "money":
                        pl.money+=count;
                        pl.money_Txt.text = pl.money.ToString();
                        break;
                    default:
                        pl.Effects(type, power, time, time2);
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
