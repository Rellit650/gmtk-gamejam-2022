using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunWeapon : BaseWeapon
{
    private bool onCD = false;
    public float inaccuracy;
    public float bulletSpeedUncertainty;
    public GameObject PlayerRef;
    public GameObject bulletPrefab;
    public int bulletAmount;
    // Start is called before the first frame update
    void Start()
    {
        type = WeaponType.Shotgun;
        //UsePrimary(0f,0f);
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
            //UsePrimary(0f,0f);
        }
    }

    public override void UsePrimary(float damageBuff, float ASBuff)
    {
        attackSpeedFromPlayer = ASBuff;
        if (!onCD)
        {
            onCD = true;
            attackCDTimer = 0f;

            for (int i = 0; i < bulletAmount; i++) 
            {
                Vector3 targetDir = PlayerRef.transform.right;
                targetDir.x += Random.Range(-inaccuracy, inaccuracy);
                targetDir.y += Random.Range(-inaccuracy, inaccuracy);
                //targetDir.z += Random.Range(-inaccuracy, inaccuracy);

                GameObject temp = Instantiate(bulletPrefab, PlayerRef.transform.position, Quaternion.LookRotation(targetDir));
                temp.GetComponent<BulletScript>().speed += Random.Range(-bulletSpeedUncertainty, bulletSpeedUncertainty);
                temp.GetComponent<BulletScript>().m_damage = damageValue;
            }
            //attackAnimationTimer = 0f;
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
}
