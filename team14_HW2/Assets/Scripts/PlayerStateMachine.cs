using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    public abstract class CharacterState
    {
        public int stateIndex;
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
            // attack
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

    public CharacterState currentState;

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
        SwitchState(new IdleState());
    }

    private void Update()
    {
        if(currentState != null)
        {
            currentState.DuringState();
        }
    }
}
