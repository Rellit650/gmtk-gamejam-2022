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
    [SerializeField] protected AudioSource TakeDamageSource;
    [SerializeField] protected AudioClip TakeDamageClip;
    [SerializeField] protected float TakeDamageVolume;
    protected bool iFramesActive;
    protected AudioScript audioManager;
    private void Start()
    {
        audioManager = FindObjectOfType<AudioScript>();
        TakeDamageSource = GameObject.Find("EnemyTakeDamageSource").GetComponent<AudioSource>();
    }

    public abstract void TakeDamage(float damage,Vector2 knockback);

    public abstract void Attack();

    public abstract void Move();

    public void PlayDamageAudio() 
    {
        audioManager.PlayOneShot(TakeDamageSource, TakeDamageClip, TakeDamageVolume);
    }

}
