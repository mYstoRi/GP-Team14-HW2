using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
