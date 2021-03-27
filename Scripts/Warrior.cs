using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct weapons
{
    public float damage, Radius, CriticalChance, CriticalDamage, AttackSpeed;
    public int MaxDamage;
    public weapons(float damage, float Radius, float CriticalChance, float CriticalDamage, float AttackSpeed, int MaxDamage)
    {
        this.damage = damage;
        this.Radius = Radius;
        this.CriticalChance = CriticalChance;
        this.CriticalDamage = CriticalDamage;
        this.AttackSpeed = AttackSpeed;
        this.MaxDamage = MaxDamage;

    }
}
public class Warrior : Player
{
    private string UnlockWeapons;
    private int EquipWeapon;
    public LayerMask target;
    public Transform[] attackPos;
    protected float damage;
    public Sprite[] skins;
    public GameObject sword;
    public GameObject damageParticale;
    public float AttackRadius;
    short n = 0;
    public float regen; 
    bool stay;
    bool Spurt_Ready = true;
    bool spurt_;
    public float spurt_forward;

    private delegate void skill();
    private skill moveSkill;

    private weapons[] Weapons;

    private void Awake()
    {
        moveSkill = Shift;
        PlayerPrefs.SetString("UnlockWeapons","10000");
        PlayerPrefs.SetInt("Equip", 0);
        UnlockWeapons = PlayerPrefs.GetString("UnlockWeapons");
        EquipWeapon = PlayerPrefs.GetInt("Equip");
        print(UnlockWeapons);
        print("Оружие "+ EquipWeapon + " Экипировано");
        Find();
        Weapons = new weapons[5]{
            new weapons(4f, 1.7f, 5f, 1.5f, 1f, 2),
            new weapons(6f, 2.3f, 10f, 1.5f, 0.75f, 3),
            new weapons(10f, 2.3f, 1f, 1.3f, 0.75f, 1),
            new weapons(3f, 2f, 2.0f, 3f, 3.0f, 5),
            new weapons(3f, 3.2f, 8f, 2.2f, 1.4f, 6)
        };
    } 
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        hp = Maxhp;
        hd.EditTime(2, 1f);
        hd.EditTime(0, Skill_Time[0]);
        hd.EditTime(1, Skill_Time[1]);
    }

    private void Update()
    {
        if(!Stun)
        {
            Jump();
            Move();
            if (Input.GetKey(KeyCode.X))
            {
                anim.SetBool("Attack", true);
            }
            else if(Input.GetKeyDown(KeyCode.R))
            {
                PlayerPrefs.SetString("UnlockWeapons", "10000");
            }
            else if(Input.GetButtonDown("Inventory"))
            {
                Inventory.GetComponent<GUI>().Active();
            }
            if (!OnGround) anim.SetBool("OnAir", true);
            else anim.SetBool("OnAir", false);
        }
        else
        {
            anim.SetBool("run", false);
            anim.SetBool("Attack", false);
            anim.SetBool("OnAir", false);
        }
    }
    private void FixedUpdate()
    {
        OnGround = Physics2D.OverlapBox(groundCheck.position, checkSize, 0f,whatIsGround);
        if (anim.GetBool("run") && !stay)
        {
            if (hp+regen <= Maxhp)
            {
                hp+=regen;
                hp_Im.fillAmount =hp/Maxhp;
            }
        }
    }
    private void Shift()
    {
        hd.SetZero(1);
        rb.velocity = new Vector2(spurt_forward *Input.GetAxis("Horizontal"), rb.velocity.y);
        StartCoroutine(Spurt_Forward());
    } 
    protected override void Move()
    {
        float moveX=Input.GetAxis ("Horizontal");
        if (moveX !=0)
        {
            if(Input.GetKey(KeyCode.LeftShift) && Spurt_Ready)
            {
                moveSkill();
            }
            else
            {
                anim.SetBool("run", true);
                rb.velocity = new Vector2(moveX * speed, rb.velocity.y);
            }
            if ((!Right && moveX > 0) || (Right && moveX < 0)) Flip();
        }
        else anim.SetBool("run", false);
        cam.transform.position = new Vector3(transform.position.x, transform.position.y, -20f);
    }
    protected override void Attack()
    {
        hd.SetZero(0);
        if (Random.Range(0,100) <= Weapons[n].CriticalChance)  {damage = Weapons[n].damage * Weapons[n].CriticalDamage +Random.Range(0,Weapons[n].MaxDamage);}
        else damage = Weapons[n].damage + Random.Range(0, Weapons[n].MaxDamage);
        Collider2D[] obj = Physics2D.OverlapCircleAll(attackPos[0].position, Weapons[n].Radius, target);
        for (int i=0; i<obj.Length; i++)
        {
            obj[i].GetComponent<Entity>().TakeDamage(damage, damageParticale);
        }
        anim.SetBool("Attack", false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos[0].position , AttackRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(groundCheck.position, checkSize);
    }
    #region Collision/Collider
    private void OnCollisionStay2D(Collision2D col)
    {
        if(col.gameObject.tag == "Wall" || col.gameObject.tag =="Enemy" || col.gameObject.tag == "Boxes")
        {
            stay = true;
        }
    }
    private void OnCollisionExit2D(Collision2D col)
    {
        if(col.gameObject.tag == "Wall" || col.gameObject.tag =="Enemy" || col.gameObject.tag == "Boxes")
        {
            stay = false;
        }
    }
    #endregion
    public override void Equip(int num)
    {
        n =(short) num;
        int h = (n!=0)? h=2*n : h=0;;
        sword.GetComponent<SpriteRenderer>().sprite = skins[h];
    }
    

    IEnumerator Spurt_Forward()
    {
        yield return new WaitForSeconds(0.25f);
        Spurt_Ready = false;
        yield return new WaitForSeconds(1f);
        Spurt_Ready = true;
    }
}
