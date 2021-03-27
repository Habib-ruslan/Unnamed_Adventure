using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public virtual void TakeDamage(float damage)
    {

    }
    public virtual void TakeDamage(float damage, bool _bool)
    {

    }
    public virtual void TakeDamage(float damage, GameObject particle)
    {

    }
    public virtual void GetKnockback(float knockPow, float knockTime,float knockDamage,GameObject particale)
    {

    }
    public virtual void GetKnockback(float OtherPow, float OtherTime, float Other_Damage, bool AttackOn)
    {

    }
}
