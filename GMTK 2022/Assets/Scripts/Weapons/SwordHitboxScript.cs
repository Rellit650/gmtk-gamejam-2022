using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHitboxScript : MonoBehaviour
{
    private float m_minRange;
    private float m_maxRange;
    private float m_damageValue;
    private float hitRate = 0.25f;
    private float hitRateTimer = 0f;

    private List<Collider2D> CollisionsToResolve = new List<Collider2D>();

    private void Update()
    {
        /*
        hitRateTimer += Time.deltaTime;
        if (hitRateTimer >= hitRate) 
        {
            ResolveAllCollisions();
            hitRateTimer = 0f;
        }
        */
    }

    public void ResolveAllCollisions() 
    {
        if (CollisionsToResolve.Count <= 0) 
        {
            return;
        }
        for(int i = 0; i < CollisionsToResolve.Count; i++) 
        {
            if (CollisionsToResolve[i].gameObject.CompareTag("Enemy"))
            {
                //Hitbox logic 
                Bounds bounds = CollisionsToResolve[i].bounds;
                Vector3 closest = bounds.ClosestPoint(transform.parent.transform.position);
                Debug.DrawRay(closest, transform.parent.transform.position - closest, Color.red, 10f);
                float sqrDist = bounds.SqrDistance(transform.parent.transform.position);
                Debug.Log(sqrDist);
                if (sqrDist >= m_minRange && sqrDist < m_maxRange)
                {
                    //Debug.Log("Enemy within ranges");
                    CollisionsToResolve[i].GetComponent<BaseEnemy>().TakeDamage(m_damageValue, Vector2.zero);
                }
            }
            CollisionsToResolve[i].gameObject.GetComponent<HitTracker>().collisionAdded = false;
        }
        CollisionsToResolve.Clear();
    }

    //Update values (if no value given it wont change)
    public void UpdateHitBoxValues(float minRange = -1f, float maxRange = -1f, float damageVal = -1f) 
    {
        if(minRange >= 0f) 
        {
            m_minRange = minRange;
        }
        if (maxRange >= 0f)
        {
            m_maxRange = maxRange;
        }
        if (damageVal >= 0f)
        {
            m_damageValue = damageVal;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //Hitbox logic 
            Bounds bounds = collision.bounds;
            Vector3 closest = bounds.ClosestPoint(transform.parent.transform.position);
            Debug.DrawRay(closest, transform.parent.transform.position - closest, Color.red, 10f);
            float sqrDist = bounds.SqrDistance(transform.parent.transform.position);
            Debug.Log(sqrDist);
            if (sqrDist >= m_minRange && sqrDist < m_maxRange)
            {
                //Debug.Log("Enemy within ranges");
                collision.GetComponent<BaseEnemy>().TakeDamage(m_damageValue, Vector2.zero);
            }
        }
        /*
        if (!collision.gameObject.GetComponent<HitTracker>().collisionAdded)
        {
            collision.gameObject.GetComponent<HitTracker>().collisionAdded = true;
            CollisionsToResolve.Add(collision);
        }
        */
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        /*
        if (!collision.gameObject.GetComponent<HitTracker>().collisionAdded) 
        {
            collision.gameObject.GetComponent<HitTracker>().collisionAdded = true;
            CollisionsToResolve.Add(collision);
        }      
        */
    }
}
