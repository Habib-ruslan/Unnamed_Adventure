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

    int EquipWeapon;
    string UnlockWeapons;
    short k;

    void Awake()
    {
        Find();
        PlayerPrefs.SetString("UnlockWeapons","11");
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
        hp = Maxhp;
        hd.EditTime(2, 1f);
        hd.EditTime(0, Skill_Time[0]);
        hd.EditTime(1, Skill_Time[1]);
    }

    void Update()
    {
        if (!Stun)
        {
            Jump();
            Run();
            if (Input.GetKey(KeyCode.X) || Input.GetKey(KeyCode.C) || Input.GetKey(KeyCode.V))
            {

                anim.SetBool("Attack", true);
                if (Input.GetKey(KeyCode.X))
                {
                    k = 0;
                    anim.SetFloat("speed", 1f);
                    hd.EditTime(0, 1f);
                }
                else if(Input.GetKey(KeyCode.C))
                {
                    k = 1;
                    anim.SetFloat("speed", 2f);
                    hd.EditTime(0, 0.5f);
                }
                else if(Input.GetKey(KeyCode.V))
                {
                    k = 2;
                    anim.SetFloat("speed", 0.75f);
                    hd.EditTime(2, 1.35f);
                }
            }

            else if(Input.GetKeyDown(KeyCode.R))
                {
                    PlayerPrefs.SetString("UnlockWeapons", "10");
                }
            else if(Input.GetKeyDown(KeyCode.I))
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
    public void Attack()
    {
        hd.Zero(0);
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
    public override void Equip(int num)
    {
        int h = (num !=0)? 6 * num : 0;
        stick.GetComponent<SpriteRenderer>().sprite = skins[h];
    }
}
