using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerControls system;
    private Vector2 moveControls, aimControls;
    private float currentDashCoolDown = 0f, currentDash = 0f;
    private BaseWeapon currentWeapon;
    private List<BaseWeapon> activeWeapons;
    Vector3 mousePos;

    [SerializeField]
    private float baseMovementSpeed, dashCoolDown, dashSpeed, dashLength, controller;

    private void OnEnable()
    {
        system.Enable();
    }

    private void OnDisable()
    {
        system.Disable();
    }

    private void Awake()
    {
        system = new PlayerControls();
        system.InGame.Move.performed += ctx => moveControls = ctx.ReadValue<Vector2>();
        system.InGame.Move.canceled += ctx => moveControls = Vector2.zero;
        system.InGame.Aim.performed += ctx => aimControls = ctx.ReadValue<Vector2>();
        system.InGame.PrimaryFire.performed += ctx => PrimaryFire();
        system.InGame.SecondaryFire.performed += ctx => SecondaryFire();
        system.InGame.Dash.performed += ctx => Dash();
        system.InGame.SwitchWeapon.performed += ctx => SwitchWeapon();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Update the cool down timer
        if (currentDashCoolDown > 0)
        {
            currentDashCoolDown -= Time.deltaTime;
            currentDashCoolDown = Mathf.Max(currentDashCoolDown, 0f);
        }

        // Update the dash timer
        if (currentDash > 0)
        {
            currentDash -= Time.deltaTime;
            currentDash = Mathf.Max(currentDash, 0f);
        }

        if (aimControls.sqrMagnitude > 0.0f)
        {
            Cursor.visible = false;
            transform.LookAt(aimControls + (Vector2)transform.position);
        }
        else
        {
            if (Input.GetAxis("Mouse X") != 0)
            {
                Cursor.visible = true;
            }
            if(Cursor.visible)
            {
                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                transform.LookAt(new Vector3(mousePos.x, mousePos.y, transform.position.z));
            }
        }
        

        MovePlayer();
    }

    void MovePlayer()
    {
        float currentDashSpeed = dashSpeed * (1 - Mathf.Cos(currentDash / dashLength * Mathf.PI * 0.5f));
        Vector2 newPosition = moveControls * Time.deltaTime * (baseMovementSpeed + currentDashSpeed);
        transform.Translate(newPosition, Space.World);
    }

    void PrimaryFire()
    {
        currentWeapon.UsePrimary();
    }

    void SecondaryFire()
    {
        currentWeapon.UseSecondary();
    }

    void Dash()
    {
        if (currentDashCoolDown == 0 && moveControls != Vector2.zero)
        {
            currentDash = dashLength;
            currentDashCoolDown = dashCoolDown + dashLength;
        }
    }

    void SwitchWeapon()
    {
        Debug.Log("SWITCHING WEAPON TO RANDOM WEAPON");
        currentWeapon = activeWeapons[Mathf.CeilToInt(Random.Range(0, activeWeapons.Count) - 1)];
    }
}
