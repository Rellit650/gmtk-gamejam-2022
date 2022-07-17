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
    protected float attackSpeedFromPlayer = 0f;
    public WeaponType type;

    public enum WeaponType 
    {
        Sword = 0,
        LaserBeam,
        Shotgun,
        Circle,
        MinePlacer
    }

    public abstract void UsePrimary(float attackDamageBuff, float attackSpeedBuff);
    public abstract void StartPrimary();
    public abstract void EndPrimary();
    public abstract void UseSecondary();
}
