using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AilenShootingState : AilenBaseState
{
    private readonly int ShootingHash = Animator.StringToHash("Praying");

    private AlienShot rangeAttack;

    public AilenShootingState(AilenStateMachine stateMachine) : base(stateMachine)
    {
        rangeAttack = stateMachine.GetComponentInChildren<AlienShot>();
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(ShootingHash, 0.1f);
    }

    public override void Update()
    {
        rangeAttack.enabled = true;
        FacePlayer();

        if (stateMachine.Health.health <= 200)
        {
            stateMachine.ChangeState(new AilenIdleState(stateMachine));
        }
    }

    public override void Exit()
    {
        rangeAttack.enabled = false;
    }

    public override void HandleInput() { }

    public override void PhysicsUpdate() { }
}