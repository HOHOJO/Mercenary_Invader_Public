using System;
using System.Collections;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;
using Random = UnityEngine.Random;

public class Monster_1_AttackState : Monster_1_BaseState
{

    //private bool alreadyAppliedForce;
    //private bool alreadyAppliedDealing;

    public Monster_1_AttackState(Monster_1_StateMachine monster_1_StateMachine) : base(monster_1_StateMachine)
    {
    }

    public delegate void Attack(); // 델리게이트
    Attack attack;

    public delegate void DamageStart(float n); // 델리게이트
    DamageStart damageStart;

    public delegate void DamageExit(); // 델리게이트
    DamageExit damageExit;

    public static bool attack_on; // 공격 트리거 전체 공격 설정 on/off
    public static bool attackTrigger_2; // 공격 트리거, 공격중 on/off
    Coroutine co_my_coroutine; // 코루틴 종료용

    IEnumerator enumerator;  // 코루틴 상태 저장용
    float curAnimationTime; // 애니메이션 시간
    string s;
    float damageNum;
    int SoundNum;

    public override void Enter()
    {
        Debug.Log("다시들어옴");
        stateMachine.MovementSpeedModifier = 0; //일단 정지
        stateMachine.Monster.StopAgent();
        base.Enter();
        StartAnimation(stateMachine.Monster.AnimationData.AttackParameterHash);// 어택 해쉬
        stateMachine.MovementSpeedModifier = 0;
        attack_on = true;
        enumerator = DELEY();

        attackTrigger_2 = true;

        s = "";
        damageNum = 0;
        SoundNum = 3;
        StartCoroutine(Attack_StateMachine());


        //alreadyAppliedForce = false;
        //alreadyAppliedDealing = false;
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Monster.AnimationData.AttackParameterHash);
        StopAnimation(stateMachine.Monster.AnimationData.Attack_1ParameterHash);
        StopAnimation(stateMachine.Monster.AnimationData.Attack_2ParameterHash);
        StopAnimation(stateMachine.Monster.AnimationData.Attack_3ParameterHash);
        StopAnimation(stateMachine.Monster.AnimationData.IdleParameterHash);

    }

    public override void Update()
    {
        if (attack_on)
        {
            stateMachine.Monster.FollowTarget();
        }
    }

    public IEnumerator Attack_StateMachine()
    {
        while (stateMachine.Monster.health>0)
        {
            if(co_my_coroutine!=null)
            {
                StopCoroutine(co_my_coroutine);
            }
            yield return co_my_coroutine=StartCoroutine(enumerator);
        }
    }

    IEnumerator DELEY()
    {
        attack = Attackdeley;
        yield return new WaitForSecondsRealtime(1);
        if (attack_on)
        {
            if (stateMachine.Monster.IsAttacking3())
            {
                if (co_my_coroutine != null)
                {
                    StopCoroutine(co_my_coroutine);
                }
                stateMachine.ChangeState(stateMachine.FlyState);
            }
            else
            {
                if (stateMachine.Monster.IsAttacking())
                {
                    s = "Claw Attack";
                    attack = AttackAnim_1;
                    damageNum = 20;
                    SoundNum = 3;
                    damageStart = stateMachine.Monster.monster_Attack2.attackStart;
                    damageExit = stateMachine.Monster.monster_Attack2.attackExit;
                }
                else
                {
                    if (stateMachine.Monster.IsAttacking2())
                    {
                        attackTrigger_2 = false;
                        attack = AttackAnim_2;
                        s = "Flame Attack";
                        damageNum = 20;
                        SoundNum = 4;
                        damageStart = stateMachine.Monster.monster_Attack3.attackStart;
                        damageExit = stateMachine.Monster.monster_Attack3.attackExit;

                    }
                    else
                    {
                        attackTrigger_2 = true;
                        attack = AttackAnim_3;
                        s = "Basic Attack";
                        damageNum = 10;
                        SoundNum = 5;
                        damageStart = stateMachine.Monster.monster_Attack1.attackStart;
                        damageExit = stateMachine.Monster.monster_Attack1.attackExit;
                    }
                }
            }
            stateMachine.Monster.TriggerAttack();
        }
        attack_on = false;


        ChangeState(ATTACK1());
        yield return null;
    }

    IEnumerator ATTACK1()
    {
        damageStart(damageNum);
        attack(); // 공격 모션 시작

        if (stateMachine.Monster.soundManager != null) // 음악재생
        {
            stateMachine.Monster.soundManager.SFXPlay("Attack", stateMachine.Monster.monsterSound.audioClips[SoundNum]);
        }

        //애니메이션 재생시간
        yield return new WaitForSecondsRealtime(0.1f);
        curAnimationTime = stateMachine.Monster.AnimTime.AnimationTime(s);
        yield return new WaitForSecondsRealtime(curAnimationTime);
        damageExit(); // 데이미 종료

        if (attack==AttackAnim_2) // 불 프리팹 삭제
        {
            stateMachine.Monster.DistroyFire();
        }

        curAnimationTime = 0f;
        attack = Attackdeley;
        attack();
        
        yield return new WaitForSecondsRealtime(1);
        attack_on=true;

        ChangeState(CHECK());
        yield return null;
    }

    IEnumerator CHECK()
    {
        if(attack_on)
        {

            if (attackTrigger_2 && stateMachine.Monster.PlayerCheck() <= (stateMachine.Monster.lostDistance-15f)
                && stateMachine.Monster.PlayerCheck() != 1234567f && attack_on&&stateMachine.Monster.IsAttacking2())
            {
                ChangeState(DELEY());
            }
            else 
            { 
                if ((stateMachine.Monster.PlayerCheck() - 0.5f) > stateMachine.Monster.nmAgent.stoppingDistance && stateMachine.Monster.PlayerCheck() != 1234567f && attack_on)
                {
                    if (co_my_coroutine != null)
                    {
                        StopCoroutine(co_my_coroutine);
                    }
                    stateMachine.ChangeState(stateMachine.ChasingState);
                }
                attack = null;

                ChangeState(DELEY());
            }
        }
        yield return null;
    }

    public void ChangeState(IEnumerator s)
    {
        enumerator = s;
    }

    public void AttackAnim_1()
    {
        StartAnimation(stateMachine.Monster.AnimationData.AttackParameterHash);
        StartAnimation(stateMachine.Monster.AnimationData.Attack_1ParameterHash);
        StopAnimation(stateMachine.Monster.AnimationData.Attack_2ParameterHash);
        StopAnimation(stateMachine.Monster.AnimationData.Attack_3ParameterHash);
        StopAnimation(stateMachine.Monster.AnimationData.IdleParameterHash);
        StopAnimation(stateMachine.Monster.AnimationData.GroundParameterHash);
        
    }

    public void AttackAnim_2()
    {
        StartAnimation(stateMachine.Monster.AnimationData.AttackParameterHash);
        StartAnimation(stateMachine.Monster.AnimationData.Attack_2ParameterHash);
        StopAnimation(stateMachine.Monster.AnimationData.Attack_1ParameterHash);
        StopAnimation(stateMachine.Monster.AnimationData.Attack_3ParameterHash);
        StopAnimation(stateMachine.Monster.AnimationData.IdleParameterHash);
        StopAnimation(stateMachine.Monster.AnimationData.GroundParameterHash);
        stateMachine.Monster.SpawnFire();
    }

    public void AttackAnim_3()
    {
        StartAnimation(stateMachine.Monster.AnimationData.AttackParameterHash);
        StartAnimation(stateMachine.Monster.AnimationData.Attack_3ParameterHash);
        StopAnimation(stateMachine.Monster.AnimationData.Attack_2ParameterHash);
        StopAnimation(stateMachine.Monster.AnimationData.Attack_1ParameterHash);
        StopAnimation(stateMachine.Monster.AnimationData.IdleParameterHash);
        StopAnimation(stateMachine.Monster.AnimationData.GroundParameterHash);
    }

    public void Attackdeley()
    {
        StartAnimation(stateMachine.Monster.AnimationData.IdleParameterHash);
        StopAnimation(stateMachine.Monster.AnimationData.Attack_1ParameterHash);
        StopAnimation(stateMachine.Monster.AnimationData.Attack_2ParameterHash);
        StopAnimation(stateMachine.Monster.AnimationData.Attack_3ParameterHash);
        StopAnimation(stateMachine.Monster.AnimationData.GroundParameterHash);

    }
}