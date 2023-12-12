using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_2_StateMachine : StateMachine
{
    public Monster2 Monster { get; }

    public Monster_2_IdleState IdlingState { get; } // ������
    public Monster_2_ChasingState ChasingState { get; } // ��������
    public Monster_2_AttackState AttackState { get; }// ���� ����
    public Monster_2_FlyState FlyState { get; } // ���� ����

    public Vector2 MovementInput { get; set; }
    public float MovementSpeed { get; private set; }
    public float RotationDamping { get; private set; }
    public float MovementSpeedModifier { get; set; } = 1f;

    public Monster_2_StateMachine(Monster2 monster)
    {
        Monster = monster;

        IdlingState = new Monster_2_IdleState(this);
        ChasingState = new Monster_2_ChasingState(this);
        AttackState = new Monster_2_AttackState(this);
        FlyState = new Monster_2_FlyState(this);

        MovementSpeed = monster.Data.GroundedData.BaseSpeed;
        RotationDamping = monster.Data.GroundedData.BaseRotationDamping;
    }
}
