using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_2_ChasingState : Monster_2_BaseState
{
    bool Fly = false;
    public Monster_2_ChasingState(Monster_2_StateMachine monster_2_StateMachine) : base(monster_2_StateMachine)
    {
    }
    public override void Enter()
    {
        base.Enter();
        Debug.Log("상태: 추격");
        if(!Fly)
        {
            stateMachine.Monster.Animator.SetFloat("groundLocomotion", 2f);
        }
        else
        {
            stateMachine.Monster.Animator.SetFloat("airLocomotion", 2f);
        }

    }
    public override void Exit()
    {
    }
    public override void Update()
    {
        if (stateMachine.Monster.PlayerCheck() > stateMachine.Monster.lostDistance && stateMachine.Monster.PlayerCheck() != 1234567f) // 플레이어가 멀어지면 정지 상태가 된다.
        {
            stateMachine.Monster.StopAgent();
            stateMachine.Monster.Animator.SetFloat("groundLocomotion", 0f);
            stateMachine.Monster.lostTarget();
            stateMachine.ChangeState(stateMachine.IdlingState);
            return;
        }
        else if ((stateMachine.Monster.PlayerCheck()) <= stateMachine.Monster.nmAgent.stoppingDistance && stateMachine.Monster.PlayerCheck() != 1234567f)// 플레이어가 공격범위에 들어오면, 공격 상태로 변경된다.
        {
            stateMachine.Monster.StopAgent();
            stateMachine.Monster.Animator.SetFloat("groundLocomotion", 0f);
            stateMachine.ChangeState(stateMachine.AttackState);
            return;
        }
        else
        {
            stateMachine.Monster.ChasingPlayer(); // 둘다 아니면 계속 추적한다.
        }
    }
    public void FixedUpdate()
    {

    }
}
