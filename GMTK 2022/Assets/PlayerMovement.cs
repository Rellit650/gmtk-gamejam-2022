using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerControls system;
    private Rigidbody rb;
    private Vector2 moveVec;
    private int dashCoolDown = 0;

    [SerializeField]
    private float moveSpeed;

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
        system.InGame.Move.performed += ctx => moveVec = ctx.ReadValue<Vector2>();
        system.InGame.Move.canceled += ctx => moveVec = Vector2.zero;
        system.InGame.PrimaryFire.performed += ctx => PrimaryFire();
        system.InGame.SecondaryFire.performed += ctx => SecondaryFire();
        system.InGame.Dash.performed += ctx => Dash();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        dashCoolDown -= dashCoolDown > 0 ? 1 : 0;
        MovePlayer(moveVec * moveSpeed);
    }

    void MovePlayer(Vector2 moveControls)
    {
        Vector2 locationVec;
        locationVec = transform.position;
        locationVec += moveControls;
        transform.position = locationVec;
    }

    void PrimaryFire()
    {

    }

    void SecondaryFire()
    {

    }

    void Dash()
    {
        if (dashCoolDown == 0)
        {
            MovePlayer(moveVec * 5);
            dashCoolDown = 1200;
        }
    }
}
