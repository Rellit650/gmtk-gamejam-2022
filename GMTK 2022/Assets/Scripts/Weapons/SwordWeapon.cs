using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordWeapon : BaseWeapon
{
    private float animationTime = 0.15f;
    private float attackAnimationTimer = 0f;
    private bool onCD = false;
    private GameObject playerRef;
    public GameObject WeaponColliderObject;
    public GameObject SwordSwingVFX;
    public Transform startPoint;
    public Transform midPoint;
    public Transform endPoint;

    // Start is called before the first frame update
    void Start()
    {
        type = WeaponType.Sword;
        playerRef = FindObjectOfType<PlayerMovement>().gameObject;
        WeaponColliderObject = GetComponentInChildren<CircleCollider2D>().gameObject;
        //GetComponentInChildren<SwordHitboxScript>().UpdateHitBoxValues(minRange, maxRange, damageValue);

        //animation time (0.35 max) = half attackspeed

        animationTime = Mathf.Min(0.15f, (attackSpeed - attackSpeedFromPlayer) * 0.5f);

        TrailRenderer[] trails = SwordSwingVFX.GetComponentsInChildren<TrailRenderer>();
        for (int i = 0; i < trails.Length; i++)
        {
            trails[i].time = animationTime;
        }
        
        WeaponColliderObject.GetComponent<HitboxScript>().UpdateHitBoxValues(damageValue);
        
        //UsePrimary();
    }

    private void OnValidate()
    {
        //GetComponentInChildren<SwordHitboxScript>(true).UpdateHitBoxValues(minRange, maxRange, damageValue);
        animationTime = Mathf.Min(0.15f, (attackSpeed - attackSpeedFromPlayer) * 0.5f);
        TrailRenderer[] trails = SwordSwingVFX.GetComponentsInChildren<TrailRenderer>();
        for (int i = 0; i < trails.Length; i++)
        {
            trails[i].time = animationTime;
        }
        WeaponColliderObject.GetComponent<HitboxScript>().UpdateHitBoxValues(damageValue);
    }



    // Update is called once per frame
    void Update()
    {
        if (attackCDTimer <= (attackSpeed - attackSpeedFromPlayer)) 
        {
            attackCDTimer += Time.deltaTime;       
        }

        if (attackCDTimer > (attackSpeed - attackSpeedFromPlayer))
        {
            onCD = false;
            //Will need to remove this later 
            //UsePrimary();
        }

        if (onCD) 
        {
            //Add to timer
            attackAnimationTimer += Time.deltaTime;

            //Calculate our lerp factor
            float interFactor = attackAnimationTimer / animationTime;

            //apply 3 point lerp
            SwordSwingVFX.transform.position = Vector3.Lerp(Vector3.Lerp(startPoint.position, midPoint.position, interFactor), Vector3.Lerp(midPoint.position, endPoint.position, interFactor), interFactor);

            //Debug.LogWarning("Later: " + Time.deltaTime);
            if (attackAnimationTimer - Time.deltaTime == 0f)
            {
                TrailRenderer[] trails = SwordSwingVFX.GetComponentsInChildren<TrailRenderer>();
                for (int i = 0; i < trails.Length; i++)
                {
                    trails[i].Clear();
                }
            }
        }
        transform.position = playerRef.transform.position;
 
        //transform.rotation = Quaternion.LookRotation(-playerRef.transform.right);
    }

    public override void UsePrimary(float damageBuff, float ASBuff)
    {
        attackSpeedFromPlayer = ASBuff;
        if (!onCD) 
        {
            transform.rotation = Quaternion.Euler(0f, 0f, playerRef.transform.rotation.eulerAngles.z);
            onCD = true;
            attackCDTimer = 0f;
            attackAnimationTimer = 0f;
            StartCoroutine(PrimaryAttack());
        } 
    }

    public override void StartPrimary()
    {
        throw new System.NotImplementedException();
    }

    public override void EndPrimary()
    {
        throw new System.NotImplementedException();
    }

    public override void UseSecondary()
    {

    }

    IEnumerator PrimaryAttack() 
    {
        WeaponColliderObject.SetActive(true);
        yield return new WaitForSeconds(animationTime);
        WeaponColliderObject.SetActive(false);
    }
}
