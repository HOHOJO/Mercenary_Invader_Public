using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_1_FlyState : Monster_1_BaseState
{
    public delegate void Fly(); // 델리게이트
    Fly fly;

    public delegate void DamageStart(float n); // 델리게이트
    DamageStart damageStart;

    public delegate void DamageExit(); // 델리게이트
    DamageExit damageExit;

    bool attack_on;

    IEnumerator enumerator;  // 코루틴 상태 저장용
    Coroutine co_my_coroutine; // 코루틴 종료용


    float AnimTime;
    string TimeName;
    float damageNum;
    int Flyturn;
    public Monster_1_FlyState(Monster_1_StateMachine monster_1_StateMachine) : base(monster_1_StateMachine)
    {
    }

    public override void Enter()
    {
        StartAnimation(stateMachine.Monster.AnimationData.AirParameterHash);
        StartAnimation(stateMachine.Monster.AnimationData.FlyOnParameterHash);
        enumerator = FlyCheck();
        AnimTime = 0f;
        TimeName = "";
        attack_on = true;

        damageNum = 0;
        Flyturn = 0;

        Debug.Log("날기1");
        StartCoroutine(Fly_StateMachine());

    }

    public override void Exit()
    {
        Debug.Log("착지");
        base.Exit();
        StopAnimation(stateMachine.Monster.AnimationData.AirParameterHash);
        StopAnimation(stateMachine.Monster.AnimationData.FlyOnParameterHash);
        StopAnimation(stateMachine.Monster.AnimationData.FlyParameterHash);
        StopAnimation(stateMachine.Monster.AnimationData.FlyIdleParameterHash);
        StopAnimation(stateMachine.Monster.AnimationData.FlyFireParameterHash);
        StopAnimation(stateMachine.Monster.AnimationData.FlyAttackParameterHash);
        StopAnimation(stateMachine.Monster.AnimationData.FlyForwardParameterHash);
        stateMachine.Monster.FlyDown();
    }

    public override void Update()
    {
        if (attack_on)
        {
            stateMachine.Monster.FollowTarget();
        }
    }

    public void FixedUpdate()
    {

    }

    public IEnumerator Fly_StateMachine()
    {
        while (stateMachine.Monster.health > 0)
        {
            if (co_my_coroutine != null)
            {
                StopCoroutine(co_my_coroutine);
            }
            yield return co_my_coroutine = StartCoroutine(enumerator);
        }
    }

    public IEnumerator FlyCheck()
    {
        if(Flyturn>=4)
        {
            if (co_my_coroutine != null)
            {
                StopCoroutine(co_my_coroutine);
            }
            AllStop();
            stateMachine.ChangeState(stateMachine.AttackState);
        }

        //if (stateMachine.Monster.Flyok)
        //{
        //    Debug.Log("나는중");
        //    stateMachine.Monster.FlyUp();
        //    yield return new WaitForSecondsRealtime(0.1f);
        //    AnimTime = stateMachine.Monster.AnimTime.AnimationTime(TimeName);
        //    yield return new WaitForSecondsRealtime(AnimTime);
        //    StopAnimation(stateMachine.Monster.AnimationData.FlyOnParameterHash);
        //}

        stateMachine.Monster.FollowCheck();
        if(stateMachine.Monster.PlayerCheck()>stateMachine.Monster.nmAgent.stoppingDistance
            && stateMachine.Monster.PlayerCheck()!=12345678)
        {
            ChangeState(FlyForward());
        }
        else
        {
            damageStart = stateMachine.Monster.monster_Attack4.attackStart;
            damageExit = stateMachine.Monster.monster_Attack4.attackExit;
            ChangeState(FlyIdle());
        }
        yield return null;
    }

    public IEnumerator FlyIdle()
    {
        Idle();
        yield return new WaitForSecondsRealtime(1f);
        ChangeState(FlyFire());
    }

    public IEnumerator FlyForward()
    {
        Debug.Log("이동");
        attack_on = false;
        Forward();
        if (stateMachine.Monster.PlayerCheck() > stateMachine.Monster.nmAgent.stoppingDistance
            && stateMachine.Monster.PlayerCheck() != 12345678)
        {
            stateMachine.Monster.ChasingPlayer();
            yield return new WaitForSecondsRealtime(2f);
        }
        attack_on = true;
        
        ChangeState(FlyCheck());
        yield return null;
    }

    public IEnumerator FlyFire()
    {
        Debug.Log("불공격"+Flyturn);
        damageNum = 20;
        attack_on = false;
        damageStart(damageNum);
        Fire();
        TimeName = "Fly Flame Attack";
        yield return new WaitForSecondsRealtime(0.1f);
        AnimTime=stateMachine.Monster.AnimTime.AnimationTime(TimeName);
        yield return new WaitForSecondsRealtime(AnimTime);
        stateMachine.Monster.DistroyFire();
        damageExit();
        Flyturn++;
        attack_on = true;
        if(Flyturn>=4)
        {
            if (co_my_coroutine != null)
            {
                StopCoroutine(co_my_coroutine);
            }
            AllStop();
            stateMachine.ChangeState(stateMachine.AttackState);
        }
        else
        {
            ChangeState(FlyCheck());
        }   
    }

    public IEnumerator FlyAttack()
    {
        yield return null;
    }

    public void ChangeState(IEnumerator s)
    {
        enumerator = s;
    }

    public void AllStop()
    {
        StopAnimation(stateMachine.Monster.AnimationData.AirParameterHash);
        StopAnimation(stateMachine.Monster.AnimationData.FlyOnParameterHash);
        StopAnimation(stateMachine.Monster.AnimationData.FlyParameterHash);
        StopAnimation(stateMachine.Monster.AnimationData.FlyIdleParameterHash);
        StopAnimation(stateMachine.Monster.AnimationData.FlyFireParameterHash);
        StopAnimation(stateMachine.Monster.AnimationData.FlyAttackParameterHash);
        StopAnimation(stateMachine.Monster.AnimationData.FlyForwardParameterHash);
    }

    public void Check()
    {
        StartAnimation(stateMachine.Monster.AnimationData.AirParameterHash);
        StartAnimation(stateMachine.Monster.AnimationData.FlyParameterHash);

        StopAnimation(stateMachine.Monster.AnimationData.FlyIdleParameterHash);
        StopAnimation(stateMachine.Monster.AnimationData.FlyFireParameterHash);
        StopAnimation(stateMachine.Monster.AnimationData.FlyAttackParameterHash);
        StopAnimation(stateMachine.Monster.AnimationData.FlyForwardParameterHash);
    }

    public void Forward()
    {
        StartAnimation(stateMachine.Monster.AnimationData.AirParameterHash);
        StartAnimation(stateMachine.Monster.AnimationData.FlyForwardParameterHash);

        StopAnimation(stateMachine.Monster.AnimationData.FlyParameterHash);
        StopAnimation(stateMachine.Monster.AnimationData.FlyIdleParameterHash);
        StopAnimation(stateMachine.Monster.AnimationData.FlyFireParameterHash);
        StopAnimation(stateMachine.Monster.AnimationData.FlyAttackParameterHash);

    }

    public void Idle()
    {
        StartAnimation(stateMachine.Monster.AnimationData.AirParameterHash);
        StartAnimation(stateMachine.Monster.AnimationData.FlyIdleParameterHash);

        StopAnimation(stateMachine.Monster.AnimationData.FlyParameterHash);
        StopAnimation(stateMachine.Monster.AnimationData.FlyFireParameterHash);
        StopAnimation(stateMachine.Monster.AnimationData.FlyAttackParameterHash);
        StopAnimation(stateMachine.Monster.AnimationData.FlyForwardParameterHash);
    }

    public void Fire()
    {
        StartAnimation(stateMachine.Monster.AnimationData.AirParameterHash);
        StartAnimation(stateMachine.Monster.AnimationData.FlyFireParameterHash);

        StopAnimation(stateMachine.Monster.AnimationData.FlyParameterHash);
        StopAnimation(stateMachine.Monster.AnimationData.FlyIdleParameterHash);
        StopAnimation(stateMachine.Monster.AnimationData.FlyAttackParameterHash);
        StopAnimation(stateMachine.Monster.AnimationData.FlyForwardParameterHash);
        stateMachine.Monster.SpawnFire();
    }

    public void Attack() 
    {
        StartAnimation(stateMachine.Monster.AnimationData.AirParameterHash);
        StartAnimation(stateMachine.Monster.AnimationData.FlyAttackParameterHash);

        StopAnimation(stateMachine.Monster.AnimationData.FlyParameterHash);
        StopAnimation(stateMachine.Monster.AnimationData.FlyIdleParameterHash);
        StopAnimation(stateMachine.Monster.AnimationData.FlyFireParameterHash);
        StopAnimation(stateMachine.Monster.AnimationData.FlyForwardParameterHash);
    }
}
