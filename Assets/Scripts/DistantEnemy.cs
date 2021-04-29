using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistantEnemy : Enemy
{
    [Header("Передвижение")]
    public bool isDanger;
    public bool isNormalDistance; 

    [Range(0,50f)]
    public float xDeviation;
    [Range(0,50f)]
    public float yDeviation;
    public float speed;
    private float X,Y;
    private bool Ready=true;

    [Header("Прицел")]
    public GameObject directionObj;
    private Rigidbody2D rb_directionObj;
    public float Offset;

    public GameObject[] bullets;
    public Transform attackPoint;
    public float scatter;



    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();  
        rb_directionObj = directionObj.GetComponent<Rigidbody2D>();
        Find();
        hp = MaxHp;
    }

    private void Update()
    {
        Move();  
        Alive();
        if(isNormalDistance) anim?.SetBool("attack", true);
        else anim?.SetBool("attack", false);
    }
    public override void TakeDamage(float Damage, GameObject particle)
    {

    }
    
    protected override void Move()
    {
        if (isAgr)
        {
            FindTarget(Offset);
            if(Ready) StartCoroutine(RandMove());
            if(isDanger)
            {
                directionObj.transform.Translate((-1)*transform.up*Time.deltaTime*speed*3); 
                rb_directionObj.velocity = Vector2.zero; 
            }
            else if (isNormalDistance)
            {
                rb_directionObj.velocity = new Vector2( X*speed, Y*speed);
            }
            else
            {
                directionObj.transform.Translate(transform.up*Time.deltaTime*speed); 
                rb_directionObj.velocity = Vector2.zero;
            }
        }
        else
        {
            rb_directionObj.velocity = new Vector2( X*speed, Y*speed);
        }
        transform.position = directionObj.transform.position;
    }
    
    private void FindTarget(float _Offset)
    {
        Vector3 dir = pl.gameObject.transform.position - directionObj.transform.position;
        float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
        directionObj.transform.rotation = Quaternion.AngleAxis(angle+_Offset, Vector3.forward);
    }

    public override void isTrue(int num, bool isBool)
    {
        switch(num)
        {
            case 0: isAgr = isBool; break;
            case 1: isNormalDistance = isBool; break;
            case 2: if(!isDanger && isBool) {Ready=true;StopCoroutine(RandMove()); }
            isDanger = isBool; break;
        }
    }
    
    private IEnumerator RandMove()
    {
        Ready = false;
        X = Random.Range(-xDeviation,xDeviation);
        Y = Random.Range(-yDeviation,yDeviation);
        yield return new WaitForSeconds(Random.Range(2f,3f));
        Ready = true;
    }
    public override void Attack()
    {
        int k = Random.Range(0,(int) bullets.Length);
        //Instantiate(bullets[k], attackPoint.position, rb_directionObj.transform.rotation);
        Quaternion rotation = rb_directionObj.transform.rotation;
        rotation.z += Random.Range(-scatter, scatter);
        Instantiate(bullets[k], attackPoint.position, rotation);
    }

}
