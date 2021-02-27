using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : Player
{
    [Header("Атака")]
    protected float damage;
    public float AttackBooster;
    public float CriticalChance;
    public Transform[] attackPos;
    public float attackRadius;
    public LayerMask[] Target;
    public GameObject[] damageParticale;

    private float DownMove;              //Начальная гравитация
    public float TimeMoveSkill;  
    public float GravityPower;           //Конечная гравитация
    public float knockDamage;
    public float knockPow;
    public float knockRadius;
    public float knockTime;

    int EquipWeapon;
    string UnlockWeapons;


    [Header("Передвижение (танк)")]
    public float slow_boost;

    void Awake()
    {
        Find();

        PlayerPrefs.SetString("UnlockWeapons","10");
        PlayerPrefs.SetInt("Equip", 0);
        UnlockWeapons = PlayerPrefs.GetString("UnlockWeapons");
        EquipWeapon = PlayerPrefs.GetInt("Equip");
        print(UnlockWeapons);
        print("Оружие "+ EquipWeapon + " Экипировано");
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        DownMove = rb.gravityScale;
        hp = Maxhp;
        hd.EditTime(2, TimeMoveSkill);
        hd.EditTime(0, Skill_Time[0]);
        hd.EditTime(1, Skill_Time[1]);
    }

    void Update()
    {
        if (!Stun)
        {
            Jump();
            Run();
            if (CanJump)
            {
                if (Input.GetKey(KeyCode.X))
                {
                    anim.SetBool("Attack", true);
                }
                else if(Input.GetKeyDown(KeyCode.C) || ((Input.GetKeyDown(KeyCode.Space) && !OnGround)))
                {
                    StartCoroutine(Gravity());
                }
                else if(Input.GetKeyDown(KeyCode.LeftShift))    {Block(true);}

                else if(Input.GetKeyDown(KeyCode.R))
                {
                    PlayerPrefs.SetString("UnlockWeapons", "10");
                }
                else if(Input.GetKeyDown(KeyCode.I))
                {
                    Inventory.GetComponent<GUI>().Active();
                }
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift)) {Block(false);}
        }
        else
        {
            anim.SetBool("run", false);
            anim.SetBool("Attack", false);
            //anim.SetBool("OnAir", false);
        }
    }
    void Attack()
    {
        hd.Zero(0);
        if (Random.Range(0,100) <= CriticalChance)  {damage = Damage * AttackBooster;}
        else damage = Damage;
        Collider2D[] en = Physics2D.OverlapCircleAll(attackPos[0].position, attackRadius, Target[0]);
        Collider2D[] box = Physics2D.OverlapCircleAll(attackPos[0].position, attackRadius, Target[1]);
        Collider2D[] bomb = Physics2D.OverlapCircleAll(attackPos[0].position, attackRadius, Target[2]);
        for (int i=0; i<en.Length; i++)
        {
            en[i].GetComponent<Mob>().TakeDamage(damage, damageParticale[0]);
        }
        for (int i = 0; i < box.Length; i++)
        {
            box[i].GetComponent<Boxes>().DestroyOther();
        }
        for (int i = 0; i < bomb.Length; i++)
        {
            bomb[i].GetComponent<Bombs>().Boom();
        }
        anim.SetBool("Attack", false);
    }
    void Block(bool On)
    {
        if (On)
        {
            hd.EditColor(1,new Color (1f,1f,1f,0f));
            BlockChance *= 10;
            speed /= slow_boost;
            CanJump = false;
        }
        else
        {
            hd.EditColor(1,new Color (1f,1f,1f,1f));
            hd.Zero(1);
            BlockChance /=10;
            speed *=slow_boost;
            CanJump = true;
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos[0].position , attackRadius);
        Gizmos.DrawWireSphere(attackPos[1].position , knockRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
    }
    IEnumerator Gravity()
    {
        anim.SetBool("Gravity", true);
        hd.Zero(2);
        rb.gravityScale = GravityPower;
        Collider2D[] en = Physics2D.OverlapCircleAll(attackPos[1].position, knockRadius, Target[0]);
        Collider2D[] box = Physics2D.OverlapCircleAll(attackPos[1].position, attackRadius, Target[1]);
        Collider2D[] bomb = Physics2D.OverlapCircleAll(attackPos[1].position, attackRadius, Target[2]);
        for (int i=0; i < en.Length; i++)
        {
            en[i].GetComponent<Mob>().KnockBack(knockPow,knockTime,knockDamage, damageParticale[0]);
        }
        for (int i=0; i<box.Length; i++)
        {
            box[i].GetComponent<Boxes>().DestroyOther();
        }
        for (int i=0; i<bomb.Length; i++)
        {
            bomb[i].GetComponent<Bombs>().Boom();
        }
        yield return new WaitForSeconds(TimeMoveSkill);
        Instantiate(damageParticale[1], groundCheck.position, Quaternion.identity);
        rb.gravityScale = DownMove;
        anim.SetBool("Gravity", false);
    }
}
