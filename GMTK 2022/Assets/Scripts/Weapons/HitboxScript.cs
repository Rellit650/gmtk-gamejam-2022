using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxScript : MonoBehaviour
{
    private float m_damageValue;

    //Update values (if no value given it wont change)
    public void UpdateHitBoxValues(float damageVal = -1f) 
    {
        /*
        if(minRange >= 0f) 
        {
            m_minRange = minRange;
        }
        if (maxRange >= 0f)
        {
            m_maxRange = maxRange;
        }
        */
        if (damageVal >= 0f)
        {
            m_damageValue = damageVal;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")) 
        {
            collision.GetComponent<BaseEnemy>().TakeDamage(m_damageValue, Vector2.zero);
        }    
        /*
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //Hitbox logic 
            Bounds bounds = collision.bounds;
            Vector3 closest = bounds.ClosestPoint(transform.parent.transform.position);
            Debug.DrawRay(closest, transform.parent.transform.position - closest, Color.red, 10f);
            float sqrDist = bounds.SqrDistance(transform.parent.transform.position);
            //Debug.Log(sqrDist);
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
}
