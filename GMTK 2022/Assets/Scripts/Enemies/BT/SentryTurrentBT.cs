using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class SentryTurrentBT : BT
{
    public float attRange = 10f;
    public float fovRange = 15f;
    public float bulletSpd = 5f;
    public List<GameObject> bulletPool;
    public GameObject bulletPrefab;
    public Transform barrelPos;

    public int bulletNum = 3;

    private void Awake()
    {
        bulletPool = new List<GameObject>();
        for (int i = 0; i < 5; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.SetActive(false);
            bullet.GetComponent<EnemyBullet>().SetSpeed(bulletSpd);
            bullet.GetComponent<EnemyBullet>().SetDmg(GetComponent<TestEnemy>().GetAttackDmg());
            bulletPool.Add(bullet);

        }
    }
    protected override Node SetupTree()
    {
       
        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new TaskEnemyInFOV(transform,fovRange),
                new TaskEnemyInAttackRange(transform,attRange),
                new TaskShoot(transform,
                GetComponent<TestEnemy>().GetAttacKSpd(),
                GetComponent<TestEnemy>().GetAttackDmg()),
            }),
        });

        return root;
    }

    public GameObject GetBullet()
    {
        foreach (GameObject bullet in bulletPool)
        {
            if(bullet.activeSelf == false)
            {
                return bullet;
            }
        }
        //make new bullet if we need to
        GameObject newBullet = Instantiate(bulletPrefab, transform.position,
            Quaternion.identity);
        newBullet.SetActive(false);
        bulletPool.Add(newBullet);
        return newBullet;
    }
}
