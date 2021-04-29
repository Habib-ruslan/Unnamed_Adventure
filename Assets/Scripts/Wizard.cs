using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : Player
{
    public GameObject[] MagicBullet;
    public Transform AttackPoint; 
    public Quaternion Offset;
    private Animation an;
    public Sprite[] skins;
    public GameObject stick;

    private int EquipWeapon;
    private string UnlockWeapons;
    private short k;

    private delegate void Skill();
    Skill first;
    Skill second;

    private void Awake()
    {
        Find();
        first = magicBulletShot;
        second = heavyBulletShot;
        PlayerPrefs.SetString("UnlockWeapons","11");
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
        anim.SetFloat("speed", 1f);
        hp = Maxhp;
        hd.EditTime(2, 1f);
        hd.EditTime(0, Skill_Time[0]);
        hd.EditTime(1, Skill_Time[1]);
    }

    private void Update()
    {
        if (!Stun)
        {
            Jump();
            Move();
            if ((Input.GetButton("Fire1") || Input.GetButton("Fire2") || Input.GetButton("Fire3")) && hd.isReady(0))
            {
                first();
            }
            else if(Input.GetButton("Fire4") && hd.isReady(1))
            {
                second();
            }

            else if(Input.GetKeyDown(KeyCode.R))
            {
                PlayerPrefs.SetString("UnlockWeapons", "10");
            }
            else if(Input.GetButtonDown("Inventory"))
            {
                Inventory.GetComponent<GUI>().Active();
            }
        }
        else
        {
            anim.SetBool("run", false);
            anim.SetBool("Attack", false);
            //anim.SetBool("OnAir", false);
        }
        
    }
    private void FixedUpdate()
    {
        OnGround = Physics2D.OverlapBox(groundCheck.position, checkSize, 0f, whatIsGround);
    }
    private void magicBulletShot()
    {
        anim.SetBool("Attack", true);
        if(Input.GetButton("Fire1"))
        {
            k = 0;
            anim.SetFloat("speed", 1f);
            hd.EditTime(0, 1f);
        }
        else if (Input.GetButton("Fire2"))
        {
            k = 1;
            anim.SetFloat("speed", 2f);
            hd.EditTime(0, 0.5f); 
        }
        else
        {
            k = 2;
            anim.SetFloat("speed", 0.75f);
            hd.EditTime(0, 1.35f);
        }
    }
    private void heavyBulletShot()
    {
        anim.SetBool("HeavyAttack", true);
    }
    public void AnimAttack()
    {
        hd.SetZero(0);
        if(transform.localScale.x<0)
        {
            Offset.z +=180f;
            Instantiate(MagicBullet[k], AttackPoint.position, Offset);
            Offset.z-=180f;
        }
        else
        {
            Instantiate(MagicBullet[k], AttackPoint.position, Offset);
        }
        anim.SetBool("Attack", false);
    }
    public void AnimHeavyAttack()
    {
        hd.SetZero(1);
        if(transform.localScale.x<0)
        {
            Offset.z +=180f;
            Instantiate(MagicBullet[3], AttackPoint.position, Offset);
            Offset.z-=180f;
        }
        else
        {
            Instantiate(MagicBullet[3], AttackPoint.position, Offset);
        }
        anim.SetBool("HeavyAttack", false);
    }
    public override void Equip(int num)
    {
        int h = (num !=0)? 6 * num : 0;
        stick.GetComponent<SpriteRenderer>().sprite = skins[h];
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(groundCheck.position, checkSize);
    }
}
