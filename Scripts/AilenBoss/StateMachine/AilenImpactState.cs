using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AilenImpactState : AilenBaseState
{
    private readonly int ImpactHash = Animator.StringToHash("Impact");

    private float duration = 0.5f;

    public AilenImpactState(AilenStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(ImpactHash, 0.1f);
    }
    public override void Update()
    {
        Move(Time.deltaTime);

        duration -= Time.deltaTime;

        if (duration <= 0f)
        {
            stateMachine.ChangeState(new AilenIdleState(stateMachine));
        }
    }

    public override void Exit() { }

    public override void HandleInput() { }

    public override void PhysicsUpdate() { }
}
