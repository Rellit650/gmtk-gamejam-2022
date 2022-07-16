using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDBar : MonoBehaviour
{

    public Slider bar;

    // Start is called before the first frame update
    void Start()
    {
        bar.value = 1f;
    }

    public void SetPercent(float percentage)
    {
        bar.value = percentage;
    }
}
