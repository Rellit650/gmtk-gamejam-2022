using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleShotWeapon : BaseWeapon
{
    private bool onCD = false;
    public GameObject PlayerRef;
    public GameObject bulletPrefab;
    public int bulletAmount;
    // Start is called before the first frame update
    void Start()
    {
        UsePrimary();
    }

    // Update is called once per frame
    void Update()
    {
        if (attackCDTimer <= attackSpeed)
        {
            attackCDTimer += Time.deltaTime;
        }

        if (attackCDTimer > attackSpeed)
        {
            onCD = false;
            //Will need to remove this later 
            UsePrimary();
        }
    }

    public override void UsePrimary()
    {
        if (!onCD)
        {
            onCD = true;
            attackCDTimer = 0f;

            for (int i = 0; i < bulletAmount; i++)
            { 
                float radians = 2 * Mathf.PI / bulletAmount * i;

                /* Get the vector direction */
                float vertrical = Mathf.Sin(radians);
                float horizontal = Mathf.Cos(radians);

                Vector3 spawnDir = new Vector3(horizontal, vertrical, 0);

                /* Get the spawn position */
                Vector3 spawnPos = PlayerRef.transform.position + spawnDir; // Radius is just the distance away from the point

                /* Now spawn */
                GameObject temp = Instantiate(bulletPrefab, spawnPos, Quaternion.LookRotation(spawnDir));

                //GameObject temp = Instantiate(bulletPrefab, PlayerRef.transform.position, Quaternion.LookRotation(targetDir));
                //temp.GetComponent<BulletScript>().speed += Random.Range(-bulletSpeedUncertainty, bulletSpeedUncertainty);
                temp.GetComponent<BulletScript>().m_damage = damageValue;
            }
            //attackAnimationTimer = 0f;
        }
    }

    public override void UseSecondary()
    {
        throw new System.NotImplementedException();
    }
}


