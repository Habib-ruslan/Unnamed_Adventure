using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public abstract class Player : Entity
{
    //Основные переменные
    [Header("Передвижение")] 
    public float speed;            //Скорость
    public float JumpPow;         //Скорость прыжка
    public LayerMask whatIsGround;      //Слой "Земля"
    protected short JumpCount;               //Текущее кол-во прыжков
    protected Rigidbody2D rb;             //RigidBody2D 
    protected bool Right = true;    //Поворот игрока
    public bool OnGround;             //Игрок на земле?
    public Transform groundCheck;       //Проверка слоя земли
    public short MaxJumps;           //Максимальное кол-во прыжков
    protected bool CanJump = true;        //Доступны ли прыжки сейчас?
    public Vector2 checkSize;

    [Header("Боевая система")]
    protected float hp;
    public float Damage;
    public float Maxhp;
    public GameObject self;
    protected bool Invuln; 
    public float timer;
    public bool Stun;
    public float[] Skill_Time;
    [Header("Система эффектов")]
    public Sprite[] eff_ico;
    public Image[] eff_img;
    public int[] _effects = {0, 0, 0, 0, 0};

    public float BlockChance;   //Блокирование/Уклонение

    [Header("Другое")]
    public int money;
    protected Animator anim;
    public Animation _attack; 
    public Sprite _sprite;
    public string MyClass;

    [Header("UI")]
    public Text money_Txt;
    protected Image hp_Im;
    protected GameObject cam;
    public GameObject[] UI_Message;
    protected GameObject Inventory;
    protected GameObject Hud;
    protected HUD hd;

    //Функция поворота персонажа
    protected void Flip() {
        Right = !Right;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    //Прыжки
    protected virtual void Jump()
    {
        if (OnGround) JumpCount = MaxJumps;
        if (CanJump)
        {
            if (Input.GetButtonDown("Jump"))
            {
                if (JumpCount > 0)
                {
                    rb.velocity = Vector2.up*JumpPow;
                    JumpCount--;
                }
            }
        }
    }
    //Передвижение по х
    protected virtual void Move()
    {
        float moveX=Input.GetAxis ("Horizontal");
        if (moveX !=0)
        {
            anim.SetBool("run", true);
            rb.velocity = new Vector2(moveX * speed, rb.velocity.y);
            if ((!Right && moveX > 0) || (Right && moveX < 0)) Flip();
        }
        else anim.SetBool("run", false);
        cam.transform.position = new Vector3(transform.position.x, transform.position.y + 5f, -20f);
    }
    protected virtual void Attack()
    {

    }
    public override void TakeDamage(float damage, bool AttackOn)
    {
        short block =(short) Random.Range(1, 100);
        if (AttackOn)
        {
            if (block > BlockChance)
            {
                hp -= damage;
                hp_Im.fillAmount =hp/Maxhp;
                anim.SetBool("Hurt", true);
                StartCoroutine(_Invuln());
            }
            else
            {
                Instantiate(UI_Message[1], new Vector3(transform.position.x, transform.position.y + 2, -1f), Quaternion.identity);
            }
        }
        else Instantiate(UI_Message[0], new Vector3(transform.position.x, transform.position.y + 2, -1f), Quaternion.identity);
        if (hp <=0)
        {
            Destroy(self);
        }

    }
    public override void TakeDamage(float damage, GameObject particle)
    {
        TakeDamage(damage, true);
    }

    protected IEnumerator _Invuln()
    {
        Invuln = true;
        yield return new WaitForSeconds(timer);
        Invuln = false;
    }
    public void Anim_Hurt()
    {
        anim.SetBool("Hurt", false);
    }
    protected void Find()
    {
        //Для режима разработки
        PlayerPrefs.SetString("Hero_Class", MyClass);
        print("Ваш класс: " + PlayerPrefs.GetString("Hero_Class"));
        
        Inventory = GameObject.Find("Canvas/Инвентарь");
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        Hud = GameObject.Find("Canvas/HUD");
        hd = Hud.GetComponent<HUD>();

        eff_img[0] = GameObject.Find("Canvas/Эффекты/Effect").GetComponent<Image>();
        eff_img[1] = GameObject.Find("Canvas/Эффекты/Effect 2").GetComponent<Image>();
        eff_img[2] = GameObject.Find("Canvas/Эффекты/Effect 3").GetComponent<Image>();
        eff_img[3] = GameObject.Find("Canvas/Эффекты/Effect 4").GetComponent<Image>();
        eff_img[4] = GameObject.Find("Canvas/Эффекты/Effect 5").GetComponent<Image>();

        money_Txt = GameObject.FindGameObjectWithTag("UI_Mon").GetComponent<Text>();
        hp_Im = GameObject.FindGameObjectWithTag("UI_HP").GetComponent<Image>();

    }
    public override void GetKnockback(float OtherPow, float OtherTime, float Other_Damage, bool AttackOn)
    {
        rb.velocity = Vector2.up*OtherPow;
        TakeDamage(Other_Damage, AttackOn);
    }
    public override void GetKnockback(float OtherPow, float OtherKnockTime, float OtherDamage, GameObject particale)
    {
        GetKnockback(OtherPow, OtherKnockTime, OtherDamage, true);
    }
    private void OnTriggerStay2D(Collider2D col)
    {
        if(col.tag == "NPC")
        {
            if (Input.GetKey(KeyCode.T))
            {
                col.GetComponent<NPCs>().Dialog();
            }
            else if(Input.GetKey(KeyCode.Backspace))
            {
                col.GetComponent<NPCs>().ExitDialog();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if(col.tag == "NPC")
        {
            col.GetComponent<NPCs>().ExitDialog();
            col.GetComponent<NPCs>().ShopExit();
        }
    }

    public virtual void Equip(int num)
    {

    }
    //Система эффектов
    #region EffectSystem
    public virtual void Effects(int type, float power,float time_, float _time)
    {
        StartCoroutine(Effects_Cor(type, power, time_, _time));
    }

    private int Eff_FreeSlot()
    {
        int maxFree = 0;
        for (int i=0; i< 5; i++)
        {
            if (_effects[i] > maxFree) maxFree = _effects[i];
        }
        return maxFree+1;
    }
    private void Eff_ReloadOrFind(int num, float time_)
    {
        if (_effects[num] !=0)
        {
            eff_img[_effects[num]-1].fillAmount = 1; 
        }
        else
        {
            _effects[num] = Eff_FreeSlot();
            eff_img[_effects[num]-1].sprite = eff_ico[num];
            eff_img[_effects[num]-1].gameObject.GetComponent<UI_Eff>().Timer = time_;
            eff_img[_effects[num]-1].gameObject.SetActive(true);
        }
    }
    private void Eff_Null(int num)
    {
        if (eff_img[_effects[num]-1].fillAmount <= 0)
        {
            for (int i=0; i<5; i++)
            {
                if (_effects[i] > _effects[num])
                {
                    float Amount = eff_img[_effects[i]-1].fillAmount;
                    eff_img[_effects[i]-1].gameObject.SetActive(false);
                    _effects[i]--;
                    eff_img[_effects[i]-1].sprite = eff_ico[i];
                    eff_img[_effects[i]-1].fillAmount = Amount;
                    eff_img[_effects[i]-1].gameObject.SetActive(true);
                }
            }
            eff_img[_effects[num]-1].gameObject.SetActive(false);
            _effects[num] = 0;
        }
    }

    private IEnumerator Effects_Cor(int type, float power,float _time, float time_)
    {
        switch(type)
        {
            case 1:
                int num = power < 0 ?  0 : 3;
                Eff_ReloadOrFind(num, time_);
                for (int i=0; i<time_/_time; i++)
                {
                    hp+=power;
                    hp_Im.fillAmount =hp/Maxhp;
                    yield return new WaitForSeconds(_time);
                }
                Eff_Null(num);
                break;

            case 3:
                num = 1;
                Eff_ReloadOrFind(num, time_);
                Stun = true;
                yield return new WaitForSeconds(time_);
                Stun = false;
                Eff_Null(num);
                break;

            case 2:
                num = power > 1 ?  2: 4;
                speed *= power;
                Eff_ReloadOrFind(num, time_);
                yield return new WaitForSeconds(time_);
                speed /=power; 
                Eff_Null(num);
                break;
        }
    }
    #endregion
}
