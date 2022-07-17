using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeamWeapon : BaseWeapon
{
    private bool firingInternal = false;
    private bool firing = true;
    private BoxCollider2D beamCollider;
    private GameObject beamVFX;
    private GameObject playerRef;
    // Start is called before the first frame update
    void Start()
    {
        type = WeaponType.LaserBeam;
        playerRef = FindObjectOfType<PlayerMovement>().gameObject;
        beamCollider = GetComponentInChildren<BoxCollider2D>();
        beamVFX = GetComponentInChildren<LineRenderer>().gameObject;
        beamVFX.SetActive(false);
        beamCollider.GetComponent<HitboxScript>().UpdateHitBoxValues(damageValue);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (firing) 
        {
            UsePrimary(0f,0f);
        }
        */

        transform.position = playerRef.transform.position;
        transform.rotation = Quaternion.Euler(0f, 0f, playerRef.transform.rotation.eulerAngles.z);
    }

    public void InitialFireCommand() 
    {
        beamVFX.SetActive(true);
        firing = true;
    }

    public void ReleaseFireCommand() 
    {
        beamVFX.SetActive(false);
        firing = false;
    }

    public override void UsePrimary(float damageBuff, float ASBuff)
    {
        if (!firingInternal) 
        {
            StartCoroutine(FireBeam());
        }
    }

    public override void StartPrimary()
    {
        InitialFireCommand();
    }

    public override void EndPrimary()
    {
        ReleaseFireCommand();
    }

    public override void UseSecondary()
    {
        
    }

    IEnumerator FireBeam() 
    {
        Debug.Log("Laser Beam Firing!");
        firingInternal = true;
        beamCollider.enabled = true;
        yield return new WaitForSeconds(attackSpeed);
        beamCollider.enabled = false;
        firingInternal = false;
    }

}
