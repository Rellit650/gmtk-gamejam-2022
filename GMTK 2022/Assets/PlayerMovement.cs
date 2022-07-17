using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerControls system;
    private Vector2 moveControls, aimControls;
    private float currentDashCoolDown = 0f, currentDash = 0f;
    private float health;
    private BaseWeapon currentWeapon;
    private List<BaseWeapon> activeWeapons;
    [SerializeField] private float attackSpeedBuff = 0f;
    private float attackDamageBuff = 0f;
    private float movementSpeedBuff = 0f;
    private int weaponIndex = 0;
    Vector3 mousePos;
    Plane raycastPlane;
    float animationLerpSpeed = 10f;


    public HUDBar dashBar, healthBar;

    [SerializeField]
    private float baseMovementSpeed, dashCoolDown, dashSpeed, dashLength, controller, baseHealth;

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
        SetHealth(baseHealth);
        activeWeapons = new List<BaseWeapon>();
        activeWeapons.Add(FindObjectOfType<SwordWeapon>());
        activeWeapons.Add(FindObjectOfType<LaserBeamWeapon>());
        activeWeapons.Add(FindObjectOfType<ShotgunWeapon>());

        //Plane for raycasting to make aiming work
        raycastPlane = new Plane(Vector3.forward, 0);
    }

    // Update is called once per frame
    void Update()
    {
        // Update the cool down timer
        if (currentDashCoolDown > 0)
        {
            currentDashCoolDown -= Time.deltaTime;
            currentDashCoolDown = Mathf.Max(currentDashCoolDown, 0f);
            float dashDisplayPercent = 1f - currentDashCoolDown / (dashCoolDown + dashLength);
            dashBar.SetPercent(dashDisplayPercent);
        }

        // Update the dash timer
        if (currentDash > 0)
        {
            currentDash -= Time.deltaTime;
            currentDash = Mathf.Max(currentDash, 0f);
        }

        Vector3 lookDir3D = Vector3.zero;
        Ray ray;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);    

        if (raycastPlane.Raycast(ray, out float distance))
        {
            lookDir3D = ray.GetPoint(distance);
        }

        lookDir3D -= transform.position;
        //float currentTurnTarget = Mathf.Atan2(lookDir3D.y, lookDir3D.x) * -Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, currentTurnTarget), Time.deltaTime * animationLerpSpeed);
        //transform.rotation = Quaternion.LookRotation(lookDir3D, Vector3.forward);
        // normalize the vector: this makes the x and y components numerically
        // equal to the sine and cosine of the angle:
        lookDir3D.z = 0f;
        lookDir3D.Normalize();
        // get the basic angle:
        float ang = Mathf.Asin(lookDir3D.y) * Mathf.Rad2Deg;
        // fix the angle for 2nd and 3rd quadrants:
        if (lookDir3D.x < 0)
        {
            ang = 180 - ang;
        }
        else // fix the angle for 4th quadrant:
        if (lookDir3D.y < 0)
        {
            ang = 360 + ang;
        }

        transform.rotation = Quaternion.Euler(0f,0f,ang);

        MovePlayer();
    }

    void MovePlayer()
    {
        float currentDashSpeed = dashSpeed * (1 - Mathf.Cos(currentDash / dashLength * Mathf.PI * 0.5f));
        Vector2 newPosition = moveControls * Time.deltaTime * (baseMovementSpeed + movementSpeedBuff + currentDashSpeed);
        transform.Translate(newPosition, Space.World);
    }

    void PrimaryFire()
    {
        currentWeapon.UsePrimary(attackDamageBuff,attackSpeedBuff);
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
            dashBar.SetPercent(0f);
        }
    }

    public void HealthPickUp(float heal) 
    {
        SetHealth(health + heal);
    }

    public void AttackSpeedPickUp(float buff) 
    {
        attackSpeedBuff += buff;
    }

    public void MovementSpeedPickUp(float buff) 
    {
        movementSpeedBuff += buff;
    }

    public void AttackDamagePickUp(float buff) 
    {
        attackDamageBuff += buff;
    }

    void SwitchWeapon()
    {
        Debug.Log("SWITCHING WEAPON TO RANDOM WEAPON");
        if (activeWeapons[weaponIndex].type == BaseWeapon.WeaponType.LaserBeam)
        {
            activeWeapons[weaponIndex].EndPrimary();
        }
        weaponIndex++;
        if (weaponIndex > activeWeapons.Count - 1) 
        {
            weaponIndex = 0;
        }
        currentWeapon = activeWeapons[weaponIndex];//[Mathf.FloorToInt(Random.Range(0, activeWeapons.Count) - 1)];
        if (activeWeapons[weaponIndex].type == BaseWeapon.WeaponType.LaserBeam) 
        {
            activeWeapons[weaponIndex].StartPrimary();
        }
    }

    void SetHealth(float value)
    {
        health = value;
        healthBar.SetPercent(health / baseHealth);
    }

    public void AddWeaponToPool(BaseWeapon newWeapon) 
    {
        activeWeapons.Add(newWeapon);
    }
}
