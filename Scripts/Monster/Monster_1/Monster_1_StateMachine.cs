using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Monster_1_StateMachine : StateMachine // 몬스터 상태 머신
{
    public Monster Monster { get; }

    public Monster_1_IdleState IdlingState { get; } // 대기상태
    public Monster_1_ChasingState ChasingState { get; } // 추적상태
    public Monster_1_AttackState AttackState { get; }// 공격 상태
    public Monster_1_FlyState FlyState { get; } // 공중 상태

    public Vector2 MovementInput { get; set; } 
    public float MovementSpeed { get; private set; }
    public float RotationDamping { get; private set; }
    public float MovementSpeedModifier { get; set; } = 1f;

    public Monster_1_StateMachine(Monster monster)
    {
        Monster = monster;

        IdlingState =new Monster_1_IdleState(this);
        ChasingState = new Monster_1_ChasingState(this);
        AttackState = new Monster_1_AttackState(this);
        FlyState = new Monster_1_FlyState(this);

        MovementSpeed = monster.Data.GroundedData.BaseSpeed;
        RotationDamping = monster.Data.GroundedData.BaseRotationDamping;
    }
}
