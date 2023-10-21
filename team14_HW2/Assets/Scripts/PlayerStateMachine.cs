using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    public CharacterState currentState;
    
    public abstract class CharacterState
    {
        public int stateIndex;
        public PlayerAction playerAction;
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
        }
        public override void DuringState()
        {
            // attack if possible
            if (playerAction.target != null) playerAction.Attack();
        }
        public override void ExitState()
        {
            print("exit idle state");
            // idk nothing currently
        }
    }
    public class MoveState : CharacterState
    {
        public override void EnterState()
        {
            stateIndex = 1;
            print("enter move state");
            // switch animation
        }
        public override void DuringState()
        {
            // move
        }
        public override void ExitState()
        {
            print("exit move state");
            // idk nothing currently
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
        currentState.playerAction = gameObject.GetComponent<PlayerAction>();
    }

    private void Start()
    {
        SwitchState(new IdleState());
    }

    private void FixedUpdate()
    {
        if(currentState != null)
        {
            currentState.DuringState();
        }
    }
}
