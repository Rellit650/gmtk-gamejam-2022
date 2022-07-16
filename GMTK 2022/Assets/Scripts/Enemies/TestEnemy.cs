using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : BaseEnemy
{
    public override void Attack()
    {
        throw new System.NotImplementedException();
    }

    public override void TakeDamage(float damage, Vector2 knockback)
    {
        CurrentHealth -= Mathf.Max(damage - Defense, 1f);
        transform.position += new Vector3(knockback.x,knockback.y);
        Debug.Log("Took Damage!");
        if (CurrentHealth <= 0f) 
        {
            Destroy(gameObject);
        }
        //throw new System.NotImplementedException();
    }

    public override void Move()
    {
        throw new System.NotImplementedException();
    }
}
