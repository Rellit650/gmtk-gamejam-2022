using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour
{
    public float MaxHealth;
    public float CurrentHealth;
    public float AttackDamage;
    public float AttackSpeed;
    public float Defense;

    public abstract void TakeDamage(float damage,Vector2 knockback);

    public abstract void Attack();

    public abstract void Move();
}
