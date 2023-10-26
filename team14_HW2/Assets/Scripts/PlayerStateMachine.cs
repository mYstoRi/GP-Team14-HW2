using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    public CharacterState currentState;

    public IdleState emptyIdleState;
    public MoveState emptyMoveState;

    private Animator anim;

    public abstract class CharacterState
    {
        public int stateIndex;
        public PlayerAction playerAction;
        public Animator animator;
        public abstract void EnterState();
        public abstract void DuringState();
        public abstract void ExitState();
    }

    public class IdleState : CharacterState
    {
        public override void EnterState()
        {
            stateIndex = 0;
            print("enter idle state");
            // switch animation
            animator.SetBool("idle", true);

        }
        public override void DuringState()
        {
            // attack if possible
            if (playerAction.target != null) playerAction.Attack();
        }
        public override void ExitState()
        {
            print("exit idle state");
            // close idle animation
            animator.SetBool("idle", false);
        }
    }
    public class MoveState : CharacterState
    {
        public override void EnterState()
        {
            stateIndex = 1;
            print("enter move state");
            // switch animation
            animator.SetBool("move", true);
        }
        public override void DuringState()
        {
            // move
        }
        public override void ExitState()
        {
            print("exit move state");
            // close move animation
            animator.SetBool("move", false);
        }
    }

    public void SwitchState(CharacterState state)
    {
        if (currentState != null)
        {
            currentState.ExitState();
        }
        currentState = state;
        currentState.EnterState();
    }

    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();

        emptyIdleState = new IdleState();
        emptyMoveState = new MoveState();
        emptyIdleState.playerAction = gameObject.GetComponent<PlayerAction>();
        emptyMoveState.playerAction = gameObject.GetComponent<PlayerAction>();
        emptyIdleState.animator = anim;
        emptyMoveState.animator = anim;

        SwitchState(emptyIdleState);
    }

    private void FixedUpdate()
    {
        if(currentState != null)
        {
            currentState.DuringState();
        }
    }
}
