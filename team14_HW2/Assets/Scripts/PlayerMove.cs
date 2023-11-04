using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    public float motionSmooth;
    public float jumpHeight;

    private PlayerStateMachine state;
    private Rigidbody rb;
    private PlayerAction playerAction;
    public Animator anim;

    private Vector2 inputDirection;
    private Vector3 direction;
    private Vector3 lookAt;
    private Vector3 velocity;
    private float jumpForce;
    private float speed;

    //public GameObject debugLookAt;

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

    public void Jump(InputAction.CallbackContext context)
    {
        if (Physics.Raycast(transform.position, Vector3.down, 0.2f) && context.performed)
        {
            anim.SetBool("jump", true);
            jumpForce = jumpHeight;
        }

    }

    private void Start()
    {
        state = gameObject.GetComponent<PlayerStateMachine>();
        rb = gameObject.GetComponent<Rigidbody>();
        playerAction = gameObject.GetComponent<PlayerAction>();
        anim = gameObject.GetComponent<Animator>();
        lookAt = Vector3.zero;
        velocity = Vector3.zero;
        speed = gameObject.GetComponent<EntityGeneric>().speed;
    }

    private void FixedUpdate()
    {
        direction = new Vector3(inputDirection.x, 0, inputDirection.y);
        velocity = direction * speed * (1 - motionSmooth) + velocity * motionSmooth;
        if (velocity.magnitude > 0.5f) lookAt = (transform.position + velocity) * motionSmooth + lookAt * (1 - motionSmooth);
        else if (state.currentState.stateIndex == 0 && playerAction.target != null)
        {
            lookAt = playerAction.target.transform.position * (1 - motionSmooth) + lookAt * motionSmooth;
            lookAt.y = transform.position.y;
        }
        if (playerAction.target != null || state.currentState.stateIndex == 1) transform.LookAt(lookAt);

        // debug ball, disable this line in actual game (or have some effect is cool too)
        //debugLookAt.transform.position = lookAt;

        if (jumpForce != 0)
        {
            velocity.y = jumpForce;
            rb.velocity = velocity;
            velocity.y = 0;
            jumpForce = 0;
        }
        else rb.velocity = new Vector3(velocity.x, rb.velocity.y, velocity.z);

    }
}
