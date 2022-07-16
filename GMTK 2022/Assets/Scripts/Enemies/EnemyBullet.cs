using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    float timeElapsed;
    public float lifeSpan = 4f;

    private void OnDisable()
    {
        timeElapsed = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        gameObject.SetActive(timeElapsed >= lifeSpan ? false : true);
    }
}
