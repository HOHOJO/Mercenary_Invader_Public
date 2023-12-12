using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_2_FlyState : Monster_2_BaseState
{

    private bool Attack_on = true;

    public delegate void DamageStart(float n); // 델리게이트
    DamageStart damageStart;

    public delegate void DamageExit(); // 델리게이트
    DamageExit damageExit;

    IEnumerator enumerator;  // 코루틴 상태 저장용
    Coroutine co_my_coroutine; // 코루틴 종료용

    int turn = 0;

    public Monster_2_FlyState(Monster_2_StateMachine monster_2_StateMachine) : base(monster_2_StateMachine)
    {
    }
    public override void Enter()
    {
        base.Enter();
        turn = 15;
        stateMachine.Monster.Animator.SetFloat("airLocomotion", 0f);
        stateMachine.Monster.Animator.SetTrigger("FlyGlide");
        stateMachine.Monster.UpMonster();
        Debug.Log("상태: 날기공격");
        enumerator = Check();
        StartCoroutine(Fly_StateMachine());
    }
    public override void Exit()
    {
        base.Exit();
        stateMachine.Monster.DownMonster();
        stateMachine.Monster.Animator.SetFloat("airLocomotion", 0f);
        if (co_my_coroutine != null)
        {
            StopCoroutine(co_my_coroutine);
        }
    }
    public override void Update()
    {
        if (Attack_on)
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

    public IEnumerator Check()
    {
        Attack_on = false;
        stateMachine.Monster.Animator.SetFloat("airLocomotion", 0f);
        if (stateMachine.Monster.PlayerCheck() > stateMachine.Monster.nmAgent.stoppingDistance
    && stateMachine.Monster.PlayerCheck() != 12345678)
        {
            Debug.Log("날기이동");
            ChangeState(Fly());
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
        int random2 = Random.Range(0, 2);
        stateMachine.Monster.Animator.SetFloat("airLocomotion", 0f);
        yield return new WaitForSecondsRealtime(1f);
        if (stateMachine.Monster.health <= (stateMachine.Monster.Data.hp - (stateMachine.Monster.Data.hp / 2)- (stateMachine.Monster.Data.hp / 4)-1)&&turn<=0)
        {
            StopCoroutine(co_my_coroutine);
            stateMachine.Monster.Animator.SetFloat("airLocomotion", 0f);
            stateMachine.ChangeState(stateMachine.AttackState);
        }
        else
        {
            switch (random2)
            {
                case 0:
                    damageStart = stateMachine.Monster.monster_Attack2.attackStart;
                    damageExit = stateMachine.Monster.monster_Attack2.attackExit;
                    ChangeState(Attack1());
                    break;
                case 1:
                    damageStart = stateMachine.Monster.monster_Attack3.attackStart;
                    damageExit = stateMachine.Monster.monster_Attack3.attackExit;
                    ChangeState(Attack2());
                    break;
            }
        }

    }
    public IEnumerator Fly()
    {
        if (stateMachine.Monster.PlayerCheck() > stateMachine.Monster.nmAgent.stoppingDistance
            && stateMachine.Monster.PlayerCheck() != 12345678)
        {
            stateMachine.Monster.Animator.SetFloat("airLocomotion", 2f);
            stateMachine.Monster.ChasingPlayer();
            ChangeState(Check());
        }
        else
        {
            stateMachine.Monster.Animator.SetFloat("airLocomotion", 0f);
            ChangeState(Idle());
        }
        yield return null;
    }

    public IEnumerator Attack1()
    {
        Attack_on = false;
        damageStart(30f);
        stateMachine.Monster.Animator.SetTrigger("flyAttack");
        yield return new WaitForSecondsRealtime(2f);
        Attack_on = true;
        damageExit();
        turn++;
        ChangeState(Check());
        yield return null;
    }

    public IEnumerator Attack2()
    {
        Attack_on = false;
        damageStart(30f);
        stateMachine.Monster.Animator.SetFloat("airLocomotion", 0f);
        stateMachine.Monster.Animator.SetTrigger("flyFireBreath");
        yield return new WaitForSecondsRealtime(3f);
        Attack_on = true;
        damageExit();
        turn++;
        ChangeState(Check());
        yield return null;
    }

    public IEnumerator GoGround()
    {
        stateMachine.Monster.Animator.SetFloat("airLocomotion", 0f);
        stateMachine.ChangeState(stateMachine.AttackState);
        yield return null;
    }


    public void ChangeState(IEnumerator s)
    {
        enumerator = s;
    }
}
