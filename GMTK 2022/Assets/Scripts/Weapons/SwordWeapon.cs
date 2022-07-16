using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordWeapon : BaseWeapon
{
    public float minRange;
    public float maxRange;
    private float attackCDTimer = 0f;
    private float attackAnimationTimer = 0f;
    private bool onCD = false;
    public GameObject SwordSwingVFX;
    public Transform startPoint;
    public Transform midPoint;
    public Transform endPoint;
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

        if (onCD) 
        {
            
            //Add to timer
            attackAnimationTimer += Time.deltaTime;
            //Debug.LogWarning("First: " + Time.deltaTime);
            //Calculate our lerp factor
            float interFactor = attackAnimationTimer / 0.15f;   
            
            //apply 3 point lerp
            SwordSwingVFX.transform.position = Vector3.Lerp(Vector3.Lerp(startPoint.position, midPoint.position, interFactor), Vector3.Lerp(midPoint.position, endPoint.position, interFactor), interFactor);

            //Debug.LogWarning("Later: " + Time.deltaTime);
            if (attackAnimationTimer - Time.deltaTime == 0f)
            {
                SwordSwingVFX.GetComponentInChildren<TrailRenderer>().Clear();
            }
        }  
    }

    public override void UsePrimary()
    {
        if (!onCD) 
        {       
            onCD = true;
            attackCDTimer = 0f;
            attackAnimationTimer = 0f;
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
        yield return new WaitForSeconds(0.15f);
        //WeaponColliderObject.GetComponent<MeleeHitboxScript>().ResolveAllCollisions();
        WeaponColliderObject.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        SwordSwingVFX.GetComponentInChildren<TrailRenderer>().Clear();
    }
}
