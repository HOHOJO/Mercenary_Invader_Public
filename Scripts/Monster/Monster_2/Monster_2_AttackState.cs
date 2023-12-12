using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_2_AttackState : Monster_2_BaseState
{
    private bool Attack_on = true;

    public delegate void DamageStart(float n); // 델리게이트
    DamageStart damageStart;

    public delegate void DamageExit(); // 델리게이트
    DamageExit damageExit;

    IEnumerator enumerator;  // 코루틴 상태 저장용
    Coroutine co_my_coroutine; // 코루틴 종료용

    public Monster_2_AttackState(Monster_2_StateMachine monster_2_StateMachine) : base(monster_2_StateMachine)
    {
    }
    public override void Enter()
    {
        base.Enter();
        stateMachine.Monster.Animator.SetFloat("groundLocomotion", 0f);
        Debug.Log("상태: 공격");
        enumerator = Check();
        StartCoroutine(Attack_StateMachine());
    }
    public override void Exit()
    {
        stateMachine.Monster.Animator.SetFloat("groundLocomotion", 0f);
        if (co_my_coroutine != null)
        {
            StopCoroutine(co_my_coroutine);
        }
    }
    public override void Update()
    {
        if(Attack_on) 
        {
            stateMachine.Monster.FollowTarget();
        }
    }
    public void FixedUpdate()
    {

    }

    public IEnumerator Attack_StateMachine()
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

    public IEnumerator Deley()
    {
        stateMachine.Monster.Animator.SetFloat("groundLocomotion", -1f);
        yield return new WaitForSecondsRealtime(1f);
        ChangeState(Check());
    }

    public IEnumerator  Check()
    {
        Attack_on = false;
        stateMachine.Monster.Animator.SetFloat("groundLocomotion", 0f);
        if (stateMachine.Monster.PlayerCheck() > stateMachine.Monster.nmAgent.stoppingDistance
    && stateMachine.Monster.PlayerCheck() != 12345678)
        {
             stateMachine.ChangeState(stateMachine.ChasingState);
        }
        else
        {
            ChangeState(Idle());
        }
        Attack_on = true;
        yield return null;
    }

    public IEnumerator Idle()
    {
        int random2 = Random.Range(0, 3);
        stateMachine.Monster.Animator.SetFloat("groundLocomotion", 0f);
        yield return new WaitForSecondsRealtime(1f);
        if(stateMachine.Monster.health<(stateMachine.Monster.Data.hp-(stateMachine.Monster.Data.hp/2)))
        {
            if(stateMachine.Monster.Flyok)
            {
                stateMachine.Monster.FlyUp();
                StopCoroutine(co_my_coroutine);
                stateMachine.Monster.Animator.SetFloat("groundLocomotion", 0f);
                stateMachine.ChangeState(stateMachine.FlyState);
            }
        }
        else
        {
            switch (random2)
            {
                case 0:
                    damageStart = stateMachine.Monster.monster_Attack1.attackStart;
                    damageExit = stateMachine.Monster.monster_Attack1.attackExit;
                    ChangeState(Attack1());
                    break;
                case 1:
                    damageStart = stateMachine.Monster.monster_Attack2.attackStart;
                    damageExit = stateMachine.Monster.monster_Attack2.attackExit;
                    ChangeState(Attack2());
                    break;
                case 2:
                    damageStart = stateMachine.Monster.monster_Attack3.attackStart;
                    damageExit = stateMachine.Monster.monster_Attack3.attackExit;
                    ChangeState(Attack3());
                    break;
                default:
                    ChangeState(Deley());
                    break;
            }
        }
    }
    public IEnumerator Walk()
    {
        Debug.Log("걷다");
        if (stateMachine.Monster.PlayerCheck() > stateMachine.Monster.nmAgent.stoppingDistance
            && stateMachine.Monster.PlayerCheck() != 12345678)
        {
            stateMachine.Monster.Animator.SetFloat("groundLocomotion", 1f);
            stateMachine.Monster.ChasingPlayer();
        }
        else 
        {
            stateMachine.Monster.Animator.SetFloat("groundLocomotion", 0f);
            ChangeState(Check());
        }
        yield return null;
    }

    public IEnumerator Attack1()
    {
        Debug.Log("물기");
        Attack_on = false;
        damageStart(30f);
        stateMachine.Monster.Animator.SetTrigger("attack1");
        Attack_on = true;
        yield return new WaitForSecondsRealtime(1f);
        damageExit();
        ChangeState(Check());
        yield return null;
    }

    public IEnumerator Attack2()
    {
        Debug.Log("몸부림");
        Attack_on = false;
        damageStart(30f);
        stateMachine.Monster.Animator.SetTrigger("attack2");
        Attack_on = true;
        yield return new WaitForSecondsRealtime(2f);
        damageExit();
        ChangeState(Check());
        yield return null;
    }

    public IEnumerator Attack3()
    {
        Debug.Log("브레스");
        Attack_on = false;
        damageStart(40f);
        stateMachine.Monster.Animator.SetTrigger("fireBreath");
        Attack_on = true;
        yield return new WaitForSecondsRealtime(3f);
        damageExit();
        ChangeState(Check());
        yield return null;
    }

    public IEnumerator GoFly()
    {
        stateMachine.Monster.Animator.SetFloat("groundLocomotion", 0f);
        stateMachine.ChangeState(stateMachine.FlyState);
        yield return null;
    }

    public void ChangeState(IEnumerator s)
    {
        enumerator = s;
    }

}
