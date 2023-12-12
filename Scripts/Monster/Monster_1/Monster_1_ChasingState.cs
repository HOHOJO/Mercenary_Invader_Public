using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_1_ChasingState : Monster_1_BaseState
{
    public Monster_1_ChasingState(Monster_1_StateMachine monster_1_StateMachine) : base(monster_1_StateMachine)
    {
    }
    public override void Enter()
    {
        base.Enter();
        stateMachine.Monster.StopAgent();
        stateMachine.MovementSpeedModifier = 1;

        if (stateMachine.Monster.soundManager != null)
        {
            stateMachine.Monster.soundManager.SFXPlay("Chasing", stateMachine.Monster.monsterSound.audioClips[2]);
        }

        StopAnimation(stateMachine.Monster.AnimationData.ScreamParameterHash);
        StartAnimation(stateMachine.Monster.AnimationData.GroundParameterHash);
        StartAnimation(stateMachine.Monster.AnimationData.RunParameterHash);
        StopAnimation(stateMachine.Monster.AnimationData.AttackParameterHash);
        StopAnimation(stateMachine.Monster.AnimationData.Attack_1ParameterHash);
        StopAnimation(stateMachine.Monster.AnimationData.Attack_2ParameterHash);
        StopAnimation(stateMachine.Monster.AnimationData.Attack_3ParameterHash);
        StopAnimation(stateMachine.Monster.AnimationData.IdleParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Monster.AnimationData.GroundParameterHash);
        StopAnimation(stateMachine.Monster.AnimationData.RunParameterHash);
    }

    public override void Update()
    {

        if (stateMachine.Monster.PlayerCheck() <= (stateMachine.Monster.lostDistance - 15f)
                && stateMachine.Monster.PlayerCheck() != 1234567f && stateMachine.Monster.IsAttacking2())
        {
            stateMachine.Monster.StopAgent();
            //stateMachine.Monster.FollowTarget();
            stateMachine.ChangeState(stateMachine.AttackState);
            return;
        }
        if(stateMachine.Monster.PlayerCheck() > stateMachine.Monster.lostDistance && stateMachine.Monster.PlayerCheck() != 1234567f) // 플레이어가 멀어지면 정지 상태가 된다.
        {
            stateMachine.Monster.StopAgent();
            stateMachine.Monster.lostTarget();
            stateMachine.ChangeState(stateMachine.IdlingState);
            return;
        }
        else if ((stateMachine.Monster.PlayerCheck()-0.3f)<=stateMachine.Monster.nmAgent.stoppingDistance && stateMachine.Monster.PlayerCheck() != 1234567f)// 플레이어가 공격범위에 들어오면, 공격 상태로 변경된다.
        {
            stateMachine.Monster.StopAgent();
            stateMachine.ChangeState(stateMachine.AttackState);
            return;
        }
        else
        {
            stateMachine.Monster.ChasingPlayer(); // 둘다 아니면 계속 추적한다.
        }
    }
}
