using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSpeedPickUp : MonoBehaviour
{
    [SerializeField]
    float buffAmount = 1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerMovement>().MovementSpeedPickUp(buffAmount);
        }
    }
}