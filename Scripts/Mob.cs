using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mob : MonoBehaviour
{
    [Header("Основное")]
    public GameObject self;
    protected Rigidbody2D rb;
    protected Animator anim;
    protected Player pl;
    public Transform center;

    public bool Agr;
    public bool Right;

    public float MaxHp;
    protected float hp;
    public bool AttackOn;
    public float damage;
    public Transform AttackPos;
    public float timeAttack;
    public float radiusAttack;
    protected bool readyAttack = true;

    public float Knock_Resist = 1;  //Сопротивление откидыванию

    public int Exp;
    public float range_Y;

    [Header("UI")]
    public GameObject UI_damage;
    public GameObject UI_Exp;
    public GameObject UI_Message;

    public virtual void TakeDamage(float Damage, GameObject particle)
    {
        Vector2 Pos_UI = new Vector2(transform.position.x + Random.Range(-2f,2f), transform.position.y +range_Y);
        if (Knock_Resist < 20f || UI_Message == null)
            anim.SetBool("hurt", true);
        else    Instantiate(UI_Message, Pos_UI, Quaternion.identity);
        Instantiate(UI_damage, Pos_UI, Quaternion.identity);
        UI_damage.GetComponentInChildren<UI_Damage>().count = Damage;
        if (Damage > pl.Damage)
            {UI_damage.GetComponentInChildren<UI_Damage>().color = new Color (147/255f, 59/255f, 16/255f);}
        else {UI_damage.GetComponentInChildren<UI_Damage>().color = new Color (210/255f, 217/255f, 40/255f);}
        hp -=Damage;
        Instantiate(particle, center.position, Quaternion.identity);
    }
    protected virtual void Alive()
    {
        if (hp <=0)
        {
            hp=100000;
            Destroy(self, 0.5f);
            UI_Exp.GetComponentInChildren<UI_Damage>().count = (float) Exp;
            Vector2 Pos_UI = new Vector2(transform.position.x + Random.Range(-2f,2f), transform.position.y +range_Y);
            Instantiate(UI_Exp, Pos_UI, Quaternion.identity);
        }
    }
    public virtual void Flip()
    {
        Right = !Right;
        Vector3 Scale = transform.localScale;
        Scale.x *= -1;
        transform.localScale = Scale;
    }
    protected virtual void Attack()
    {

    }
    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color =Color.red;
        Gizmos.DrawWireSphere(AttackPos.position, radiusAttack);
    }
    protected virtual void Cor_takeDamage()
    {
        anim.SetBool("hurt", false);
        anim.SetFloat("speed", 1f);
    }
    public virtual void KnockBack(float knockPow, float knockTime, float knockDamage, GameObject damageParticale)
    {
        Agr = false;
        if (Knock_Resist < 10f)
            rb.velocity = Vector2.up*knockPow/Knock_Resist;
        TakeDamage(knockDamage, damageParticale);
        Agr = true;
    }
    protected virtual void Move()
    {

    }
    protected void Find()
    {
        pl = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    public void OtherStun(float slow)
    {
        anim.SetFloat("speed", slow);
    }
}
