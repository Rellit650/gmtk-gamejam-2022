using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpeedPickUp : MonoBehaviour
{
    [SerializeField]
    float buffAmount = 1f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerMovement>().AttackSpeedPickUp(buffAmount);
            Destroy(gameObject);
        }
    }
}
