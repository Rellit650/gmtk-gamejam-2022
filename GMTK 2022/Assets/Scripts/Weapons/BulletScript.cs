using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed;
    [HideInInspector]
    public float m_damage;

    public float lifeSpan;
    private float lifeTime;

    // Update is called once per frame
    void Update()
    {
        transform.position += speed * Time.deltaTime * transform.forward;

        lifeTime += Time.deltaTime;

        if (lifeTime >= lifeSpan) 
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")) 
        {
            collision.gameObject.GetComponent<BaseEnemy>().TakeDamage(m_damage, Vector2.zero);
            Destroy(gameObject);
        }

        if (collision.CompareTag("Wall")) 
        {
            Destroy(gameObject);
        }
    }
}
