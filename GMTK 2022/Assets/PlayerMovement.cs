using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerControls system;

    private void Awake()
    {
        system = new PlayerControls();
        system.InGame.Move.performed += ctx => MovePlayer(ctx.ReadValue<Vector2>());
        system.InGame.PrimaryFire.performed += ctx => PrimaryFire();
        system.InGame.SecondaryFire.performed += ctx => SecondaryFire();
        system.InGame.Dash.performed += ctx => Dash();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void MovePlayer(Vector2 moveControls)
    {

    }

    void PrimaryFire()
    {

    }

    void SecondaryFire()
    {

    }

    void Dash()
    {

    }
}
