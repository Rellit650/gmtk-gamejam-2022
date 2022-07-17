using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TestEnemy : BaseEnemy
{
    public override void Attack()
    {
        throw new System.NotImplementedException();
    }

    public override void TakeDamage(float damage, Vector2 knockback)
    {
        if (iFramesActive)
        {
            Debug.Log("iFrames!");
            //CurrentHealth -= Mathf.Max(damage - Defense, 1f);
            transform.position += new Vector3(knockback.x * 0.5f, knockback.y * 0.5f);
        }
        else 
        {
            Debug.Log("Taking Damage!");

            float totalDamage = Mathf.Max(damage - defense, 1f);
            currentHealth -= totalDamage;
            GameObject temp = Instantiate(DamageNumberPrefab, gameObject.transform.position, Quaternion.identity);
            temp.GetComponentInChildren<TextMeshPro>().text = totalDamage.ToString();
            Vector3 newPos = temp.transform.position;
            newPos.z -= 7f;
            temp.transform.position = newPos;
            transform.position += new Vector3(knockback.x, knockback.y);
            PlayDamageAudio();
            StartCoroutine(IFrames());
        }
        if (currentHealth <= 0f) 
        {
            Destroy(gameObject);
        }
        //throw new System.NotImplementedException();
    }

    public override void Move()
    {
        throw new System.NotImplementedException();
    }

    IEnumerator IFrames() 
    {
        iFramesActive = true;
        yield return new WaitForSeconds(0.05f);
        iFramesActive = false;
    }


}
