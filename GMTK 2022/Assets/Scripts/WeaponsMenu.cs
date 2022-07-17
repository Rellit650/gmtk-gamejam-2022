using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponsMenu : MonoBehaviour
{
    public GameObject container;
    public Button left, right;
    public TextMeshProUGUI weaponName;
    private float currentTimeOut = 0f;

    [SerializeField]
    private float timeOut;

    // Start is called before the first frame update
    void Start()
    {
        container.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (currentTimeOut > 0f)
        {
            currentTimeOut = Mathf.Max(currentTimeOut - Time.deltaTime, 0f);
            if (currentTimeOut == 0f)
            {
                container.SetActive(false);
            }
        }
    }

    void Display(string text, bool leftMost, bool rightMost)
    {
        left.interactable = !leftMost;
        right.interactable = !rightMost;
        weaponName.text = text;
        container.SetActive(true);
        currentTimeOut = timeOut;
    }
}
