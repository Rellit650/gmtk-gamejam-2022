using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    float timeElapsed;
    public float lifeSpan = 4f;
    Vector3 _direction;
    float _speed;

    private float attackDmg = 1f;

    private Rigidbody2D rb;
    public void SetDirection(Vector3 dir) { _direction = dir; }

    public void SetSpeed(float spd) { _speed = spd; }

    public void SetDmg(float dmg) { attackDmg = dmg; }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnDisable()
    {
        timeElapsed = 0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timeElapsed += Time.deltaTime;
        rb.AddForce(_direction * _speed);
        gameObject.SetActive(timeElapsed >= lifeSpan ? false : true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerMovement>().TakeDamage(attackDmg);
            gameObject.SetActive(false);
        }
    }
}
