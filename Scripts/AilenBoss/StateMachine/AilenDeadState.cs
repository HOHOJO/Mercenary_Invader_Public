using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AilenDeadState : AilenBaseState
{
    public AilenDeadState(AilenStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.Ragdoll.ToggleRagdolll(true);
        foreach(var weapon in stateMachine.Weapons)
        {
            weapon.gameObject.SetActive(false);
        }
    }

    public override void Update() { }

    public override void Exit() { }

    public override void HandleInput() { }

    public override void PhysicsUpdate() { }

}
