using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordWeapon : BaseWeapon
{
    public float minRange;
    public float maxRange;
    private float attackCDTimer = 0f;
    private bool onCD = false;
    // Start is called before the first frame update
    void Start()
    {
        WeaponColliderObject = GetComponentInChildren<BoxCollider2D>().gameObject;
        GetComponentInChildren<SwordHitboxScript>().UpdateHitBoxValues(minRange, maxRange, damageValue);
        UsePrimary();
    }

    private void OnValidate()
    {
        GetComponentInChildren<SwordHitboxScript>().UpdateHitBoxValues(minRange, maxRange, damageValue);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(attackCDTimer);
        if (attackCDTimer <= attackSpeed) 
        {
            attackCDTimer += Time.deltaTime;
            if (attackCDTimer > attackSpeed) 
            {
                onCD = false;
                //Will need to remove this later 
                UsePrimary();
            }
        }
    }

    public override void UsePrimary()
    {
        if (!onCD) 
        {
            onCD = true;
            attackCDTimer = 0f;
            StartCoroutine(PrimaryAttack());
        } 
    }

    public override void UseSecondary()
    {

    }

    IEnumerator PrimaryAttack() 
    {
        Debug.Log("Attacking");
        WeaponColliderObject.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        //WeaponColliderObject.GetComponent<MeleeHitboxScript>().ResolveAllCollisions();
        WeaponColliderObject.SetActive(false);
    }
}
