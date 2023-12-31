using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Monster2 : MonoBehaviour
{
    [field: Header("References")]
    [field: SerializeField] public Monster_1_Data Data { get; private set; } // 몬스터 기본 데이터
    [field: Header("Animations & Sound")]
    [field: SerializeField] public MonsterAnimationsData AnimationData { get; private set; } // 애니메이션 해쉬코드 모음
    public Animator Animator { get; private set; } // 애니메이터
    private Rigidbody _rigidbody;// 충돌체
    public SoundManager soundManager { get; private set; }
    public MonsterSound monsterSound { get; private set; }

    [field: Header("State")]
    private Monster_2_StateMachine stateMachine; // 상태머신
    public static int num { get; private set; } // 어택 스택 
    public static MonsterHealth monsterHealth { get; private set; } // 체력관리(피격판정)
    public int health;
    public MonsterAnimTime AnimTime { get; private set; }
    public bool Flyok = true;


    [field: Header("Find")]
    [SerializeField] bool DebugMode = true; // 제작화면에서 시야 보이기 안보이기
    [Range(0f, 360f)][SerializeField] float ViewAngle = 0f; // 시야각도
    [SerializeField] float ViewRadius = 1f; // 시약 지름(반경)
    [SerializeField] LayerMask TargetMask; // 타겟 레이어
    [SerializeField] LayerMask ObstacleMask; // 방해물 레이어
    [SerializeField] Transform Target; // 타겟

    [field: Header("NAv")]
    public NavMeshAgent nmAgent; // 네비게이션
    public float lostDistance; // 추격 중지 

    [field: Header("Attack")]
    public Monster_2_Damage monster_Attack1;
    public Monster_2_Damage monster_Attack2;
    public Monster_2_Damage monster_Attack3;
    [field: Header("FlyAttack")]
    public Monster_2_Damage monster_FlyAttack1;
    public Monster_2_Damage monster_FlyAttack2;

    List<Collider> hitTargetList = new List<Collider>(); // 타겟 리스트

    private void Awake()
    {
        Monster_2_BaseState.monoInstance = new GameObject("Monster").AddComponent<Monster_2_BaseState>();
        AnimationData.Initialize();
        Target = null;
        //nmAgent = GetComponent<NavMeshAgent>();
        Animator = GetComponentInChildren<Animator>();
        stateMachine = new Monster_2_StateMachine(this);
        monsterHealth = GetComponent<MonsterHealth>();
        AnimTime = GetComponent<MonsterAnimTime>();
        _rigidbody = GetComponentInChildren<Rigidbody>();
        monsterSound = GetComponentInChildren<MonsterSound>();

        num = 0;
        health = monsterHealth.health;
    }
    void Start()
    {
        soundManager = SoundManager.FindAnyObjectByType<SoundManager>();
        stateMachine.ChangeState(stateMachine.IdlingState); // 상태 시작
        monsterHealth.OnDie += OnDie; // 죽음판정
    }

    void Update()
    {
        stateMachine.HandleInput();
        stateMachine.Update();
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        if (Target == null)
        {
            raycastPlayer();
        }
        if(health<=-0)
        {
            OnDie();
        }
    }
    private void FixedUpdate()
    {
        stateMachine.PhysicsUpdate(); // 물리효과 업데이트
    }

    void OnDie() // 죽었으면 트리거 발동
    {
        Animator.SetTrigger("collapse");
        enabled = false;

        GameManager.Instance.GameOver(true);
    }

    void OnCollisionEnter(Collision col) // 충돌체 이름
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            // 보스 캐릭터를 고정시키기
            _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            _rigidbody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
        }
    }

    public void lostTarget() // 타겟 없애기
    {
        Target = null;
    }

    public bool FindTarget() // 타겟이 있나
    {
        if (Target == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public bool AttackTarget()// 타겟과 거리가 매우 가까울때
    {
        if (Target == null) return false;

        if (nmAgent.remainingDistance <= nmAgent.stoppingDistance && 0 < nmAgent.remainingDistance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void FollowTarget()// 플레이어 바라보기
    {
        Vector3 dir = Target.transform.position - this.transform.position;

        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 10f);
    }

    public bool FollowCheck() // 플레이어 방향이 맞는지 확인
    {
        if (Target != null)
        {
            Vector2 dir = Target.transform.position - this.transform.position;
            float k = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
            if (k <= -90 || k <= 91)
            {
                return true;
            }
            else
            {
                FollowTarget();
            }
        }
        return false;
    }

    public void StopAgent()// 가속도 제거
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        nmAgent.velocity = Vector3.zero;
        nmAgent.isStopped = true;
    }

    public void ChasingPlayer()// 플레이어 추적 명령
    {
        nmAgent.isStopped = false;
        nmAgent.SetDestination(Target.position);
    }

    public void MonsterWalk(Vector3 p)// 이동명령
    {
        nmAgent.isStopped = false;
        nmAgent.SetDestination(p);
    }

    public float PlayerCheck() // 플레이어 거리체크
    {
        if (Target != null)
        {
            return Vector3.Distance(Target.transform.position, this.transform.position);
        }
        return 1234567f;
    }

    public void UpMonster()
    {
        nmAgent.baseOffset = 1.2f;
    }

    public void DownMonster()
    {
        nmAgent.baseOffset = 0f;
    }

    public void FlyUp()
    {
        Flyok = false;
    }

    public void FlyDown()
    {
        Flyok = true;
    }




    private void OnDrawGizmos() // 몬스터의 플레이어 찾기 알고리즘
    {
        if (!DebugMode) return;
        Vector3 myPos = transform.position + Vector3.up * 0.5f;

        float lookingAngle = transform.eulerAngles.y;  //캐릭터가 바라보는 방향의 각도
        Vector3 rightDir = AngleToDir(transform.eulerAngles.y + ViewAngle * 0.5f);
        Vector3 leftDir = AngleToDir(transform.eulerAngles.y - ViewAngle * 0.5f);
        Vector3 lookDir = AngleToDir(lookingAngle);

        Debug.DrawRay(myPos, rightDir * ViewRadius, Color.blue);//오른쪽 각도
        Debug.DrawRay(myPos, leftDir * ViewRadius, Color.blue);// 왼쪽 각도
        Debug.DrawRay(myPos, lookDir * ViewRadius, Color.cyan);// 중심

        hitTargetList.Clear();
        Collider[] Targets = Physics.OverlapSphere(myPos, ViewRadius, TargetMask);

        if (Targets.Length == 0) return;
        foreach (Collider EnemyColli in Targets)
        {
            Vector3 targetPos = EnemyColli.transform.position;
            Vector3 targetDir = (targetPos - myPos).normalized;
            float targetAngle = Mathf.Acos(Vector3.Dot(lookDir, targetDir)) * Mathf.Rad2Deg;
            if (targetAngle <= ViewAngle * 0.5f && !Physics.Raycast(myPos, targetDir, ViewRadius, ObstacleMask))
            {
                hitTargetList.Add(EnemyColli);
                if (DebugMode) Debug.DrawLine(myPos, targetPos, Color.red);
                Target = EnemyColli.transform;
            }
        }

        Gizmos.DrawWireSphere(myPos, ViewRadius);
    }

    private void raycastPlayer()
    {
        
        float lookingAngle = transform.eulerAngles.y;  //캐릭터가 바라보는 방향의 각도
        Vector3 myPos = transform.position + Vector3.up * 0.5f;
        Vector3 lookDir = AngleToDir(lookingAngle);

        hitTargetList.Clear();
        Collider[] Targets = Physics.OverlapSphere(myPos, ViewRadius, TargetMask);

        if (Targets.Length == 0) return;
        foreach (Collider EnemyColli in Targets)
        {
            Vector3 targetPos = EnemyColli.transform.position;
            Vector3 targetDir = (targetPos - myPos).normalized;
            float targetAngle = Mathf.Acos(Vector3.Dot(lookDir, targetDir)) * Mathf.Rad2Deg;
            if (targetAngle <= ViewAngle * 0.5f && !Physics.Raycast(myPos, targetDir, ViewRadius, ObstacleMask))
            {
                hitTargetList.Add(EnemyColli);
                if (DebugMode) Debug.DrawLine(myPos, targetPos, Color.red);
                Target = EnemyColli.transform;
            }
        }
    }

    Vector3 AngleToDir(float angle)
    {
        float radian = angle * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(radian), 0f, Mathf.Cos(radian));
    }
}
