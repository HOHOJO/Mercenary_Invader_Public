using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Monster_1_IdleState : Monster_1_BaseState
{
    public Monster_1_IdleState(Monster_1_StateMachine monster_1_StateMachine) : base(monster_1_StateMachine)
    {
    }

    private bool reSet;
    private float waittimes;
    private float MaxWanderTime;
    private float minWanderDistance;
    private float maxWanderDistance;
    private bool startWalk;
    IEnumerator enumerator;  // 코루틴 상태 저장용
    Coroutine coroutine;

    public override void Enter()
    {
        if (stateMachine.Monster.soundManager != null)
        {
            stateMachine.Monster.soundManager.SFXPlay("idel", stateMachine.Monster.monsterSound.audioClips[0]);
        }

        stateMachine.Monster.StopAgent();
        stateMachine.MovementSpeedModifier = 0f;
        base.Enter();

        StartAnimation(stateMachine.Monster.AnimationData.GroundParameterHash);
        StartAnimation(stateMachine.Monster.AnimationData.IdleParameterHash);

        waittimes = 0f;
        MaxWanderTime = 0.05f;
        minWanderDistance = 20f;
        maxWanderDistance = 50f;

        startWalk = true;
        reSet =true;
        enumerator = Walk();

        Monster_1_BaseState.StartCoroutine(Walk_StateMachine());
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Monster.AnimationData.GroundParameterHash);
        StopAnimation(stateMachine.Monster.AnimationData.IdleParameterHash);
        StopAnimation(stateMachine.Monster.AnimationData.WalkParameterHash);
        StartAnimation(stateMachine.Monster.AnimationData.RunParameterHash);
    }

    public override void Update()
    { 
        if(stateMachine.Monster.PlayerCheck() <= stateMachine.Monster.lostDistance && stateMachine.Monster.PlayerCheck() != 1234567f)
        {
            if (stateMachine.Monster.FindTarget()) // 플레이어를 찾으면 추적상태로 변경한다.
            {
                if(stateMachine.Monster.ScreamOK)
                {
                    StartAnimation(stateMachine.Monster.AnimationData.ScreamParameterHash);
                    stateMachine.Monster.ScreamOK = false;
                }
                stateMachine.ChangeState(stateMachine.ChasingState);
                if(coroutine!=null)
                {
                    StopCoroutine(coroutine);
                }
                return;
            }
        }
    }

    public void FixedUpdate()
    {

    }

    public IEnumerator Walk_StateMachine()
    {
        while (stateMachine.Monster.health > 0)
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
            }
            yield return coroutine = StartCoroutine(enumerator);
        }
    }

    IEnumerator Walk()
    {
        while(reSet) // 정찰 가능인가.
        {
            if (startWalk)
            {
                StopAnimation(stateMachine.Monster.AnimationData.IdleParameterHash);
                StartAnimation(stateMachine.Monster.AnimationData.WalkParameterHash);
                stateMachine.Monster.MonsterWalk(GetWanderLocation());
                startWalk = false;
            }

            yield return new WaitForSecondsRealtime(1);
            if (MaxWanderTime < (waittimes += Time.deltaTime) || (stateMachine.Monster.PlayerCheck() - 0.01f) <= (stateMachine.Monster.nmAgent.stoppingDistance-8))
            {
                waittimes = 0f;
                stateMachine.Monster.StopAgent();
                StartAnimation(stateMachine.Monster.AnimationData.IdleParameterHash);
                StopAnimation(stateMachine.Monster.AnimationData.WalkParameterHash);
                yield return new WaitForSecondsRealtime(10);
                startWalk = true;
                enumerator = Walk();
            }
        }
    }

    Vector3 GetWanderLocation()
    {
        NavMeshHit hit;
        NavMesh.SamplePosition(stateMachine.Monster.transform.position + (Random.onUnitSphere * Random.Range(minWanderDistance, maxWanderDistance)), out hit, maxWanderDistance, NavMesh.AllAreas);
        Debug.Log(hit.position);
        return hit.position;
    }
}