using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_2_BaseState : MonoBehaviour, IState
{
    protected Monster_2_StateMachine stateMachine;
    protected readonly Monster_1_GroundData groundData;
    public Monster_2_BaseState(Monster_2_StateMachine monster_2_StateMachine)
    {
        stateMachine = monster_2_StateMachine;
        groundData = stateMachine.Monster.Data.GroundedData;
    }
    public virtual void Enter() // 상태 진입
    {

    }

    public virtual void Exit() // 상태 종료
    {

    }

    public virtual void HandleInput()// 입력
    {

    }

    public virtual void PhysicsUpdate()
    {

    }

    public virtual void Update()
    {

    }

    protected void StartAnimation(int animationHash) // 애니메이션 시작
    {
        stateMachine.Monster.Animator.SetBool(animationHash, true);
    }

    protected void StopAnimation(int animationHash) // 애니메이션 종료
    {
        stateMachine.Monster.Animator.SetBool(animationHash, false);
    }

    public static MonoBehaviour monoInstance;

    public static void Initializer()
    {
        //monoInstance = new GameObject($"[{nameof(Monster_1_BaseState)}]").AddComponent<Monster_1_BaseState>();
        Debug.Log("시작함");
        //DontDestroyOnLoad(monoInstance.gameObject);
        //monoInstance =new GameObject("Monster").AddComponent<Monster_1_BaseState>();
    }

    public static new Coroutine StartCoroutine(IEnumerator coroutine)
    {
        return monoInstance.StartCoroutine(coroutine);
    }

    public static new Coroutine StartCoroutine(string coroutine)
    {
        return monoInstance.StartCoroutine(coroutine);
    }

    public static new void StopCoroutine(Coroutine coroutine)
    {
        monoInstance.StopCoroutine(coroutine);
    }
}
