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
    public LayerMask target;
    public GameObject[] damageParticale;

    private float DownMove;              //Начальная гравитация
    public float TimeMoveSkill;  
    public float GravityPower;           //Конечная гравитация
    public float knockDamage;
    public float knockPow;
    public float knockRadius;
    public float knockTime;

    private int EquipWeapon;
    private string UnlockWeapons;

    private delegate void Skill();
    Skill first;
    Skill second;
    private delegate void holdingSkill(bool b);
    holdingSkill third;


    [Header("Передвижение (танк)")]
    public float slow_boost;

    private void Awake()
    {
        Find();
        first = normalDamage;
        second = moveSkill;
        third = Block;
        PlayerPrefs.SetString("UnlockWeapons","10");
        PlayerPrefs.SetInt("Equip", 0);
        UnlockWeapons = PlayerPrefs.GetString("UnlockWeapons");
        EquipWeapon = PlayerPrefs.GetInt("Equip");
        print(UnlockWeapons);
        print("Оружие "+ EquipWeapon + " Экипировано");
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        DownMove = rb.gravityScale;
        hp = Maxhp;
        hd.EditTime(2, TimeMoveSkill);
        hd.EditTime(0, Skill_Time[0]);
        hd.EditTime(1, Skill_Time[1]);
    }

    private void Update()
    {
        if (!Stun)
        {
            Jump();
            Move();
            if (CanJump)
            {
                if (Input.GetButton("Fire1"))
                {
                    first();
                }
                else if(Input.GetButtonDown("Jump") && !OnGround)
                {
                    second();
                }
                else if(Input.GetKeyDown(KeyCode.LeftShift))    {third(true);}

                else if(Input.GetKeyDown(KeyCode.R))
                {
                    PlayerPrefs.SetString("UnlockWeapons", "10");
                }
                else if(Input.GetButtonDown("Inventory"))
                {
                    Inventory.GetComponent<GUI>().Active();
                }
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift)) {third(false);}
        }
        else
        {
            anim.SetBool("run", false);
            anim.SetBool("Attack", false);
            //anim.SetBool("OnAir", false);
        }
    }
    protected override void Jump()
    {
        if (OnGround) JumpCount = MaxJumps;
        if (CanJump)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (JumpCount > 0)
                {
                    rb.velocity = Vector2.up * JumpPow;
                    JumpCount--;
                }
            }
        }
    }
    private void FixedUpdate()
    {
        OnGround = Physics2D.OverlapBox(groundCheck.position, checkSize, 0f, whatIsGround);
    }
    private void normalDamage()
    {
        anim.SetBool("Attack", true);
    }
    private void moveSkill()
    {
        StartCoroutine(Gravity());
    }
    protected override void Attack()
    {
        hd.SetZero(0);
        Damaging();
        anim.SetBool("Attack", false);
    }
    private void Block(bool On)
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
            hd.SetZero(1);
            BlockChance /=10;
            speed *=slow_boost;
            CanJump = true;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos[0].position , attackRadius);
        Gizmos.DrawWireSphere(attackPos[1].position , knockRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(groundCheck.position, checkSize);
    }
    private IEnumerator Gravity()
    {
        anim.SetBool("Gravity", true);
        hd.SetZero(2);
        rb.gravityScale = GravityPower;
        Collider2D[] obj = Physics2D.OverlapCircleAll(attackPos[0].position, attackRadius, target);
        for (int i=0; i < obj.Length; i++)
        {
            obj[i].GetComponent<Entity>().GetKnockback(knockPow,knockTime,knockDamage, damageParticale[0]);
        }
        yield return new WaitForSeconds(TimeMoveSkill);
        Instantiate(damageParticale[1], groundCheck.position, Quaternion.identity);
        rb.gravityScale = DownMove;
        anim.SetBool("Gravity", false);
    }
    private void Damaging()
    {
        if (Random.Range(0, 100) <= CriticalChance) { damage = Damage * AttackBooster; }
        else damage = Damage;
        Collider2D[] obj = Physics2D.OverlapCircleAll(attackPos[0].position, attackRadius, target);

        for (int i = 0; i < obj.Length; i++)
        {
            obj[i].GetComponent<Entity>().TakeDamage(damage, damageParticale[0]);
        }
    }
}
