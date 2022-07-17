using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    private float weaponSwapCDTimer = 0f;
    private bool usingPrimary = false;
    private bool usingSecondary = false;
    private bool onSwapCD = false;
    Vector3 mousePos;
    Plane raycastPlane;
    float animationLerpSpeed = 10f;


    public TextMeshProUGUI weaponText;
    public Image radialCooldown;
    public HUDBar dashBar, healthBar;

    [SerializeField]
    private float baseMovementSpeed, dashCoolDown, dashSpeed, dashLength, controller, baseHealth, swapCooldown;

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
        system.InGame.PrimaryFire.canceled += ctx => EndPrimary();
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
        activeWeapons.Add(FindObjectOfType<CircleShotWeapon>());
        activeWeapons.Add(FindObjectOfType<MinePlacerWeapon>());
        currentWeapon = activeWeapons[0];

        //Plane for raycasting to make aiming work
        raycastPlane = new Plane(Vector3.forward, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (onSwapCD) 
        {
            weaponSwapCDTimer += Time.deltaTime;
            radialCooldown.fillAmount = weaponSwapCDTimer / swapCooldown;
            if (weaponSwapCDTimer >= swapCooldown) 
            {
                onSwapCD = false;
                weaponSwapCDTimer = 0f;
                radialCooldown.color = Color.green;
            }
        }

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

        if(usingPrimary)
            currentWeapon.UsePrimary(attackDamageBuff, attackSpeedBuff);
    }

    void MovePlayer()
    {
        float currentDashSpeed = dashSpeed * (1 - Mathf.Cos(currentDash / dashLength * Mathf.PI * 0.5f));
        Vector2 newPosition = moveControls * Time.deltaTime * (baseMovementSpeed + movementSpeedBuff + currentDashSpeed);
        transform.Translate(newPosition, Space.World);
    }

    void PrimaryFire()
    {
        currentWeapon.StartPrimary();
        usingPrimary = true;     
    }

    void EndPrimary() 
    {
        currentWeapon.EndPrimary();
        usingPrimary = false;
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
        int activeWeaponsCount = activeWeapons.Count;

        if (!onSwapCD) 
        {
            onSwapCD = true;
            currentWeapon.EndPrimary();
            /* //Cycle Weapons
            weaponIndex++;
            if (weaponIndex > activeWeapons.Count - 1) 
            {
                weaponIndex = 0;
            }
            */
            //Cant get same Weapon
            int newWeapon = Random.Range(0, activeWeaponsCount);
            if (newWeapon == weaponIndex)
            {
                newWeapon++;
                if (newWeapon >= activeWeaponsCount)
                {
                    
                    newWeapon %= activeWeaponsCount;
                }
            }

            currentWeapon = activeWeapons[newWeapon];
            weaponIndex = newWeapon;
            SetWeaponText();
            radialCooldown.color = Color.red;
        }     
    }

    void SetWeaponText() 
    {
        switch (currentWeapon.type) 
        {
            case BaseWeapon.WeaponType.LaserBeam: 
                {
                    weaponText.text = "Laser Beam";
                    break;
                }
            case BaseWeapon.WeaponType.Shotgun:
                {
                    weaponText.text = "Shotgun";
                    break;
                }
            case BaseWeapon.WeaponType.Sword:
                {
                    weaponText.text = "Sword";
                    break;
                }
            case BaseWeapon.WeaponType.Circle:
                {
                    weaponText.text = "Circle Shot";
                    break;
                }
            case BaseWeapon.WeaponType.MinePlacer:
                {
                    weaponText.text = "Mine Placer";
                    break;
                }
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
