using System.Collections;
using UnityEngine;

public class AilenAttackingState : AilenBaseState
{
    private readonly int[] AttackHashes = new int[]
    {
        Animator.StringToHash("Attack"),
        Animator.StringToHash("Attack2"),
        Animator.StringToHash("Attack3"),
        Animator.StringToHash("Kick"),
        Animator.StringToHash("Backflip"),
    };

    public AilenAttackingState(AilenStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        int randomAttackIndex = Random.Range(0, AttackHashes.Length);
        int randomAttackHash = AttackHashes[randomAttackIndex];

        foreach (var weapon in stateMachine.Weapons)
        {
            weapon.SetAttack(stateMachine.AttackDamage);
        }

        stateMachine.Animator.CrossFadeInFixedTime(randomAttackHash, 0.1f);
    }

    public override void Update()
    {
        if (GetNormalizedTime(stateMachine.Animator) >= 1)
        {
            stateMachine.ChangeState(new AilenChasingState(stateMachine));
        }
        if (stateMachine.Health.health <= 200)
        {
            stateMachine.Animator.speed = 1.7f;
        }
    }

    private float GetNormalizedTime(Animator animator)
    {
        AnimatorStateInfo currentInfo = animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = animator.GetNextAnimatorStateInfo(0);

        if (animator.IsInTransition(0) && nextInfo.IsTag("Attack"))
        {
            return nextInfo.normalizedTime;
        }
        else if (!animator.IsInTransition(0) && currentInfo.IsTag("Attack"))
        {
            return currentInfo.normalizedTime;
        }
        else
        {
            return 0;
        }
    }

    public override void Exit() { }

    public override void HandleInput() { }

    public override void PhysicsUpdate() { }
}
