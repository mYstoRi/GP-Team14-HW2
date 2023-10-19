using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    public float speed;

    private PlayerStateMachine state;

    private Vector2 inputDirection;
    private Vector3 direction;

    public void Movement(InputAction.CallbackContext context)
    {
        // read input
        inputDirection = context.ReadValue<Vector2>();

        // switch state
        if (inputDirection.Equals(Vector2.zero)) 
        {
            if (state.currentState.stateIndex != 0) state.SwitchState(new PlayerStateMachine.IdleState());
        }
        else
        {
            if (state.currentState.stateIndex != 1) state.SwitchState(new PlayerStateMachine.MoveState());
        }

        // rotate (haven't implement)
    }

    private void Start()
    {
        state = gameObject.GetComponent<PlayerStateMachine>();
    }

    private void FixedUpdate()
    {
        direction = new Vector3(inputDirection.x, 0, inputDirection.y);
        gameObject.GetComponent<Rigidbody>().velocity = direction * speed;
    }
}
