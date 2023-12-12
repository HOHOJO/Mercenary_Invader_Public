using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AilenBaseState : IState
{
    protected AilenStateMachine stateMachine;

    public AilenBaseState(AilenStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    protected void Move(float deltaTime)
    {
        Move(Vector3.zero, deltaTime);
    }

    protected void Move(Vector3 motion, float deltaTime)
    {
        stateMachine.Controller.Move(motion* deltaTime);
    }

    protected void FacePlayer()
    {
        if(stateMachine.Player == null) { return; }

        Vector3 lookPos = stateMachine.Player.transform.position - stateMachine.transform.position;
        lookPos.y = 0f;

        stateMachine.transform.rotation = Quaternion.LookRotation(lookPos);
    }

    protected bool IsInDetectionRange()
    {
        Vector3 toPlayer = stateMachine.Player.transform.position - stateMachine.transform.position;

        return toPlayer.magnitude <= stateMachine.PlayerDetectionRange;
    }

    public abstract void Enter();
    public abstract void Exit();
    public abstract void HandleInput();
    public abstract void Update();
    public abstract void PhysicsUpdate();
}
