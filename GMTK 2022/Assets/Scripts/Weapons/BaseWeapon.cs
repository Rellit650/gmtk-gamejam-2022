using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour
{
    public float damageValue;
    public float attackSpeed;
    public float attackRange;
    public float knockbackAmount;

    public abstract void UsePrimary();
    public abstract void UseSecondary();
}
