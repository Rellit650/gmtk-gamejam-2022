using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeamWeapon : BaseWeapon
{
    private bool firingInternal = false;
    private bool firing = true;
    private BoxCollider2D beamCollider;
    private GameObject beamVFX;
    // Start is called before the first frame update
    void Start()
    {
        beamCollider = GetComponentInChildren<BoxCollider2D>();
        beamVFX = GetComponentInChildren<LineRenderer>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (firing) 
        {
            UsePrimary();
        }    
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

    public override void UsePrimary()
    {
        if (!firingInternal) 
        {
            StartCoroutine(FireBeam());
        }
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
