using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MonsterAnimationsData
{
    [SerializeField] private string groundParameterName = "@Ground";
    [SerializeField] private string idleParameterName = "Idle";
    [SerializeField] private string ScreamParameterName = "Scream";
    [SerializeField] private string walkParameterName = "Walk";
    [SerializeField] private string runParameterName = "Run";

    [SerializeField] private string airParameterName = "@Air";
    [SerializeField] private string flyParameterName = "Fly";
    [SerializeField] private string flyOnParameterName = "FlyOn";
    [SerializeField] private string flyIdleParameterName = "FlyIdle";
    [SerializeField] private string flyFireParameterName = "FlyFire";
    [SerializeField] private string flyAttackParameterName = "FlyAttack";
    [SerializeField] private string flyForwardkParameterName = "FlyForward";

    //°ø°Ýµé
    [SerializeField] private string attackParameterName = "@Attack";
    [SerializeField] private string attack_1ParameterName = "Attack_1";
    [SerializeField] private string attack_2ParameterName = "Attack_2";
    [SerializeField] private string attack_3ParameterName = "Attack_3";


    public int GroundParameterHash { get; private set; }
    public int IdleParameterHash { get; private set; }
    public int ScreamParameterHash { get; private set; }
    public int WalkParameterHash { get; private set; }
    public int RunParameterHash { get; private set; }

    public int AirParameterHash { get; private set; }
    public int FlyParameterHash { get; private set; }
    public int FlyOnParameterHash { get; private set; }
    public int FlyIdleParameterHash { get; private set; }
    public int FlyFireParameterHash { get; private set; }
    public int FlyAttackParameterHash { get; private set; }
    public int FlyForwardParameterHash { get; private set; }

    public int AttackParameterHash { get; private set; }
    public int Attack_1ParameterHash { get; private set; }
    public int Attack_2ParameterHash { get; private set; }
    public int Attack_3ParameterHash { get; private set; }

    public void Initialize()
    {
        GroundParameterHash = Animator.StringToHash(groundParameterName);
        IdleParameterHash = Animator.StringToHash(idleParameterName);
        ScreamParameterHash = Animator.StringToHash(ScreamParameterName);
        WalkParameterHash = Animator.StringToHash(walkParameterName);
        RunParameterHash = Animator.StringToHash(runParameterName);

        AirParameterHash = Animator.StringToHash(airParameterName);
        FlyParameterHash = Animator.StringToHash(flyParameterName);
        FlyOnParameterHash = Animator.StringToHash(flyOnParameterName);
        FlyIdleParameterHash = Animator.StringToHash(flyIdleParameterName);
        FlyFireParameterHash = Animator.StringToHash(flyFireParameterName);
        FlyAttackParameterHash = Animator.StringToHash(flyAttackParameterName);
        FlyForwardParameterHash = Animator.StringToHash(flyForwardkParameterName);

        AttackParameterHash = Animator.StringToHash(attackParameterName);
        Attack_1ParameterHash = Animator.StringToHash(attack_1ParameterName);
        Attack_2ParameterHash = Animator.StringToHash(attack_2ParameterName);
        Attack_3ParameterHash = Animator.StringToHash(attack_3ParameterName);

    }
}
