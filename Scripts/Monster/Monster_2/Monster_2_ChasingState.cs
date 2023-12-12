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
        Debug.Log("����: �߰�");
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
        if (stateMachine.Monster.PlayerCheck() > stateMachine.Monster.lostDistance && stateMachine.Monster.PlayerCheck() != 1234567f) // �÷��̾ �־����� ���� ���°� �ȴ�.
        {
            stateMachine.Monster.StopAgent();
            stateMachine.Monster.Animator.SetFloat("groundLocomotion", 0f);
            stateMachine.Monster.lostTarget();
            stateMachine.ChangeState(stateMachine.IdlingState);
            return;
        }
        else if ((stateMachine.Monster.PlayerCheck()) <= stateMachine.Monster.nmAgent.stoppingDistance && stateMachine.Monster.PlayerCheck() != 1234567f)// �÷��̾ ���ݹ����� ������, ���� ���·� ����ȴ�.
        {
            stateMachine.Monster.StopAgent();
            stateMachine.Monster.Animator.SetFloat("groundLocomotion", 0f);
            stateMachine.ChangeState(stateMachine.AttackState);
            return;
        }
        else
        {
            stateMachine.Monster.ChasingPlayer(); // �Ѵ� �ƴϸ� ��� �����Ѵ�.
        }
    }
    public void FixedUpdate()
    {

    }
}
