using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour
{

    [SerializeField] protected float damageValue;
    [SerializeField] protected float attackSpeed;
    [SerializeField] protected float attackCDTimer = 0f;
    [SerializeField] protected float attackRange;
    [SerializeField] protected float knockbackAmount;
    public abstract void UsePrimary();
    public abstract void UseSecondary();
}
