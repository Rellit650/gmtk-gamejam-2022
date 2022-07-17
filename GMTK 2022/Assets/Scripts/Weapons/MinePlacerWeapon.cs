using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinePlacerWeapon : BaseWeapon
{
    private bool onCD = false;
    private bool released = true;
    public GameObject playerRef;
    public GameObject minePrefab;
    public int bulletAmount;
    // Start is called before the first frame update
    void Start()
    {
        type = WeaponType.MinePlacer;
        //UsePrimary(0f, 0f);
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
            //UsePrimary(0f, 0f);
        }
        transform.rotation = Quaternion.Euler(0f, 0f, playerRef.transform.rotation.eulerAngles.z);
    }

    public override void UsePrimary(float damageBuff, float ASBuff)
    {
        attackSpeedFromPlayer = ASBuff;
        if (!onCD && released)
        {
            released = false;
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
                Vector3 spawnPos = playerRef.transform.position + spawnDir; // Radius is just the distance away from the point          

                /* Now spawn */
                GameObject temp = Instantiate(minePrefab, spawnPos, Quaternion.LookRotation(spawnDir));

                //GameObject temp = Instantiate(bulletPrefab, PlayerRef.transform.position, Quaternion.LookRotation(targetDir));
                //temp.GetComponent<BulletScript>().speed += Random.Range(-bulletSpeedUncertainty, bulletSpeedUncertainty);
                temp.GetComponent<MineScript>().m_damage = damageValue;
            }
            //attackAnimationTimer = 0f;
        }
    }
    public override void StartPrimary()
    {
        //throw new System.NotImplementedException();
        released = true;
    }

    public override void EndPrimary()
    {
        //throw new System.NotImplementedException();
        //released = true;
    }

    public override void UseSecondary()
    {
        //throw new System.NotImplementedException();
    }
}
