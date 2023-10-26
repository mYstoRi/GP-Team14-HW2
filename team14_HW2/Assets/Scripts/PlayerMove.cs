using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    public float speed;
    public float motionSmooth;

    private PlayerStateMachine state;
    private Rigidbody rb;
    private PlayerAction playerAction;

    private Vector2 inputDirection;
    private Vector3 direction;
    private Vector3 lookAt;

    public GameObject debugLookAt;

    public void Movement(InputAction.CallbackContext context)
    {
        // read input
        inputDirection = context.ReadValue<Vector2>();

        // switch state
        if (inputDirection.Equals(Vector2.zero)) 
        {
            if (state.currentState.stateIndex != 0) state.SwitchState(state.emptyIdleState);
        }
        else
        {
            if (state.currentState.stateIndex != 1) state.SwitchState(state.emptyMoveState);
        }

        // rotate (haven't implement)

    }

    private void Start()
    {
        state = gameObject.GetComponent<PlayerStateMachine>();
        rb = gameObject.GetComponent<Rigidbody>();
        playerAction = gameObject.GetComponent<PlayerAction>();
        lookAt = Vector3.zero;
    }

    private void FixedUpdate()
    {
        direction = new Vector3(inputDirection.x, 0, inputDirection.y);
        rb.velocity = direction * speed * (1 - motionSmooth) + rb.velocity * motionSmooth;
        if (rb.velocity.magnitude > 0.5f) lookAt = (transform.position + rb.velocity) * motionSmooth + lookAt * (1 - motionSmooth);
        else if (state.currentState.stateIndex == 0 && playerAction.target != null)
        {
            lookAt = playerAction.target.transform.position * (1 - motionSmooth) + lookAt * motionSmooth;
            lookAt.y = 0;
        }
        if (playerAction.target != null || state.currentState.stateIndex == 1) transform.LookAt(lookAt);
        debugLookAt.transform.position = lookAt;
    }
}
