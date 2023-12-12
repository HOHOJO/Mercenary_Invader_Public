using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_2_IdleState : Monster_2_BaseState
{
    public Monster_2_IdleState(Monster_2_StateMachine monster_2_StateMachine) : base(monster_2_StateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateMachine.Monster.Animator.SetFloat("groundLocomotion", 0f);
        Debug.Log("상태: 정지");
    }
    public override void Exit() 
    {
    }
    public override void Update() 
    {
        if (stateMachine.Monster.PlayerCheck() <= stateMachine.Monster.lostDistance && stateMachine.Monster.PlayerCheck() != 1234567f)
        {
            if (stateMachine.Monster.FindTarget()) // 플레이어를 찾으면 추적상태로 변경한다.
            {
                Debug.Log("상태: 찾는중");
                stateMachine.ChangeState(stateMachine.ChasingState);
                return;
            }
        }
    }
    public void FixedUpdate()
    {

    }
}
