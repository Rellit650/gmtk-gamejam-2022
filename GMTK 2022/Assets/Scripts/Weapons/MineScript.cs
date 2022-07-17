using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineScript : MonoBehaviour
{
    public float range;
    [HideInInspector]
    public float m_damage;

    public float lifeSpan;
    private float lifeTime;

    // Update is called once per frame
    void Update()
    {
        //transform.position += speed * Time.deltaTime * transform.forward;

        lifeTime += Time.deltaTime;

        if (lifeTime >= lifeSpan)
        {
            Collider2D[] explosionHits = Physics2D.OverlapCircleAll(transform.position, range);

            for (int i = 0; i < explosionHits.Length; i++)
            {
                if (explosionHits[i].CompareTag("Enemy"))
                    explosionHits[i].GetComponent<BaseEnemy>().TakeDamage(m_damage, Vector2.zero);
            }
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Collider2D[] explosionHits = Physics2D.OverlapCircleAll(transform.position, range);

            for (int i = 0; i < explosionHits.Length; i++)
            {
                if (explosionHits[i].CompareTag("Enemy"))
                    explosionHits[i].GetComponent<BaseEnemy>().TakeDamage(m_damage, Vector2.zero);
            }
            Destroy(gameObject);
        }
    }
}
