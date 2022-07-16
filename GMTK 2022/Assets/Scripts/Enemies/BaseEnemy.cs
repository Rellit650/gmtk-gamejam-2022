using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour
{

    [SerializeField] protected float maxHealth;
    [SerializeField] protected float currentHealth;
    [SerializeField] protected float attackDamage;
    [SerializeField] protected float attackSpeed;
    [SerializeField] protected float defense;
    [SerializeField] protected GameObject DamageNumberPrefab;
    protected bool iFramesActive;

    public abstract void TakeDamage(float damage,Vector2 knockback);

    public abstract void Attack();

    public abstract void Move();

    public float GetHealth() { return currentHealth; }
    public float GetAttacKSpd() { return attackSpeed; }
    public float GetAttackDmg() { return attackDamage; }
}
