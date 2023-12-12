using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AilenIdleState : AilenBaseState
{
    private readonly int LocomotionHash = Animator.StringToHash("Locomotion");
    private readonly int SpeedHash = Animator.StringToHash("Speed");

    public AilenIdleState(AilenStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(LocomotionHash, 0.1f);
    }
    public override void Update()
    {
        Move(Time.deltaTime);
        if (IsInDetectionRange())
        {
            stateMachine.ChangeState(new AilenChasingState(stateMachine));
            return;
        }

        FacePlayer();

        stateMachine.Animator.SetFloat(SpeedHash, 0, 0.1f, Time.deltaTime);
    }

    public override void Exit() { }

    public override void HandleInput(){ }

    public override void PhysicsUpdate(){ }
}
