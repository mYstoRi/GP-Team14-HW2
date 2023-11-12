using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is not a good way to implement: can use way more modern stuff.
[DisallowMultipleComponent]
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
        public EntityGeneric stats;
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
            
            playerAction.attackCD = 50 / stats.attackSpeed + 10;

        }
        public override void DuringState()
        {
            // attack if possible
            if (playerAction.target != null)
            {
                playerAction.Attack();
            }
            else animator.SetBool("shooting", false);
            animator.SetBool("jump", false);

        }
        public override void ExitState()
        {
            print("exit idle state");
            // close idle animation
            animator.SetBool("idle", false);
            animator.SetBool("shooting", false);
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
            animator.SetBool("jump", false);
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
        emptyIdleState.stats = gameObject.GetComponent<EntityGeneric>();
        emptyMoveState.stats = gameObject.GetComponent<EntityGeneric>();

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
