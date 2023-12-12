using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AilenChasingState : AilenBaseState
{
    private readonly int LocomotionHash = Animator.StringToHash("Locomotion");
    private readonly int SpeedHash = Animator.StringToHash("Speed");

    public AilenChasingState(AilenStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(LocomotionHash, 0.1f);
    }
    public override void Update()
    {
        if (!IsInDetectionRange())
        {
            stateMachine.ChangeState(new AilenIdleState(stateMachine));
            return;
        }
        else if(IsInAttackRange())
        {
            stateMachine.ChangeState(new AilenAttackingState(stateMachine));
        }

        MoveToPlayer(Time.deltaTime);
        FacePlayer();

        stateMachine.Animator.SetFloat(SpeedHash, 1f, 0.1f, Time.deltaTime);
    }

    private void MoveToPlayer(float deltaTime)
    {
        if (stateMachine.NavAgent.isOnNavMesh)
        {
            stateMachine.NavAgent.destination = stateMachine.Player.transform.position;
            Vector3 movementSpeed = stateMachine.NavAgent.desiredVelocity.normalized * stateMachine.MoveSpeed;
            Move(movementSpeed, deltaTime);
        }

        stateMachine.NavAgent.velocity = stateMachine.Controller.velocity;
    }

    private bool IsInAttackRange()
    {
        Vector3 inPlayer = stateMachine.Player.transform.position - stateMachine.transform.position;

        return inPlayer.magnitude <= stateMachine.PlayerAttackRange;
    }

    public override void Exit()
    {
        stateMachine.NavAgent.ResetPath();
        stateMachine.NavAgent.velocity = Vector3.zero;
    }

    public override void HandleInput() { }

    public override void PhysicsUpdate() { }
}
