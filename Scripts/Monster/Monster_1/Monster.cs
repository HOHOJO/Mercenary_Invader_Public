using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Monster : MonoBehaviour
{
    [field: Header("References")]
    [field: SerializeField] public Monster_1_Data Data { get; private set; } // ���� �⺻ ������
    [field: Header("Animations & Sound")]
    [field: SerializeField] public MonsterAnimationsData AnimationData { get; private set; } // �ִϸ��̼� �ؽ��ڵ� ����
    public Animator Animator { get; private set; } // �ִϸ�����
    private Rigidbody _rigidbody;// �浹ü
    public SoundManager soundManager { get; private set; }
    public MonsterSound monsterSound { get; private set; }

    [field: Header("State")]
    private Monster_1_StateMachine stateMachine; // ���¸ӽ�
    public static int num {  get; private set; } // ���� ���� 
    public static MonsterHealth monsterHealth { get; private set; } // ü�°���(�ǰ�����)
    public int health;
    public MonsterAnimTime AnimTime { get; private set; }
    public bool ScreamOK = true;
    public bool Flyok = true;



    [field: Header("Find")]
    [SerializeField] bool DebugMode = true; // ����ȭ�鿡�� �þ� ���̱� �Ⱥ��̱�
    [Range(0f, 360f)][SerializeField] float ViewAngle = 0f; // �þ߰���
    [SerializeField] float ViewRadius = 1f; // �þ� ����(�ݰ�)
    [SerializeField] LayerMask TargetMask; // Ÿ�� ���̾�
    [SerializeField] LayerMask ObstacleMask; // ���ع� ���̾�
    [SerializeField] Transform Target; // Ÿ��

    [field: Header("NAv")]
    public NavMeshAgent nmAgent; // �׺���̼�
    public float lostDistance; // �߰� ���� 

    [field: Header("Attack")]
    public Monster_1_Damage monster_Attack1;
    public Monster_1_Damage monster_Attack2;
    public Monster_1_Damage monster_Attack3;
    public Monster_1_Damage monster_Attack4;
    public GameObject attackFire;
    public GameObject[] FireSpawn = new GameObject[3];
    private GameObject[] Fire = new GameObject[3];

    List<Collider> hitTargetList = new List<Collider>(); // Ÿ�� ����Ʈ

    private void Awake()
    {
        Monster_1_BaseState.monoInstance = new GameObject("Monster").AddComponent<Monster_1_BaseState>();
        AnimationData.Initialize();
        Target = null;
        nmAgent = GetComponent<NavMeshAgent>();
        Animator = GetComponentInChildren<Animator>();
        stateMachine = new Monster_1_StateMachine(this);
        monsterHealth= GetComponent<MonsterHealth>();
        AnimTime=GetComponent<MonsterAnimTime>();
        _rigidbody = GetComponentInChildren<Rigidbody>();
        monsterSound = GetComponentInChildren<MonsterSound>();

        num = 0;
        health = monsterHealth.health;
    }
    void Start()
    {
        soundManager=SoundManager.FindAnyObjectByType<SoundManager>();
        stateMachine.ChangeState(stateMachine.IdlingState); // ���� ����
        monsterHealth.OnDie += OnDie; // ��������
    }

    void Update()
    {
        stateMachine.HandleInput();
        stateMachine.Update();
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        if(Target==null)
        {
            raycastPlayer();
        }
    }
    private void FixedUpdate()
    {
        stateMachine.PhysicsUpdate(); // ����ȿ�� ������Ʈ
    }

    void OnDie() // �׾����� Ʈ���� �ߵ�
    {
        Animator.SetTrigger("Die");
        enabled = false;

        GameManager.Instance.GameOver(true);
    }

    void OnCollisionEnter(Collision col) // �浹ü �̸�
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            // ���� ĳ���͸� ������Ű��
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

    public void lostTarget() // Ÿ�� ���ֱ�
    {
        Target = null;
    }

    public bool FindTarget() // Ÿ���� �ֳ�
    {
        if(Target == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public bool AttackTarget()// Ÿ�ٰ� �Ÿ��� �ſ� ����ﶧ
    {
        if (Target == null) return false;

        if (nmAgent.remainingDistance <= nmAgent.stoppingDistance&&0<nmAgent.remainingDistance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void FollowTarget()// �÷��̾� �ٶ󺸱�
    {
        //if (Target != null)
        //{
        //    //Vector3 dir = Target.transform.position - this.transform.position;
        //    //float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //    //this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * Data.speed);
        //    //transform.eulerAngles = Vector3.up * angle;

        //    //Vector2 forward = new Vector2(transform.position.z, transform.position.x);
        //    //Vector2 steeringTarget = new Vector2(nmAgent.steeringTarget.z, nmAgent.steeringTarget.x);

        //    ////������ ���� ��, ���Լ��� ���� ���Ѵ�.
        //    //Vector2 dir = steeringTarget - forward;
        //    //float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        //    ////���� ����
        //    //nmAgent.transform.eulerAngles = Vector3.up * angle;
        //    //transform.LookAt(Target);
        //}

        Vector3 dir = Target.transform.position - this.transform.position;

        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 10f);
    }

    public bool FollowCheck() // �÷��̾� ������ �´��� Ȯ��
    {
        if (Target != null)
        {
            Vector2 dir = Target.transform.position - this.transform.position;
            float k = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
            if (k <=-90||k<=91)
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

    public void StopAgent()// ���ӵ� ����
    {
        nmAgent.velocity = Vector3.zero;
        nmAgent.isStopped = true;
    }

    public void ChasingPlayer()// �÷��̾� ���� ���
    {
        nmAgent.isStopped = false;
        nmAgent.SetDestination(Target.position);
    }

    public void MonsterWalk(Vector3 p)// �̵����
    {
        nmAgent.isStopped = false;
        nmAgent.SetDestination(p);
    }

    public float PlayerCheck() // �÷��̾� �Ÿ�üũ
    {
        if( Target != null ) 
        {
            return Vector3.Distance(Target.transform.position, this.transform.position);
        }
        return 1234567f;
    }

    public void TriggerAttack() // Attack ���ÿø���
    {
        num++;
    }

    public bool IsAttacking()  // ���� ���� �Ǻ�
    {
        if ((num % 3) == 0&&num!=0)
        { return true; }
        else
        { return false; }   
    }

    public bool IsAttacking2()  // ���� ���� �Ǻ�
    {
        if ((num % 2) == 0 && num != 0)
        { return true; }
        else
        { return false; }
    }

    public bool IsAttacking3()  // ���� ���� �Ǻ�
    {
        if ((num % 5) == 0 && num != 0)
        { return true; }
        else
        { return false; }
    }

    public void SpawnFire()
    {
        Fire[0] = Instantiate(attackFire, FireSpawn[0].transform.position, FireSpawn[0].transform.rotation);
        Fire[0].transform.parent = FireSpawn[0].transform;
        Fire[1] = Instantiate(attackFire, FireSpawn[1].transform.position, FireSpawn[1].transform.rotation);
        Fire[1].transform.parent = FireSpawn[1].transform;
        Fire[2] = Instantiate(attackFire, FireSpawn[2].transform.position, FireSpawn[2].transform.rotation);
        Fire[2].transform.parent = FireSpawn[2].transform;
    }

    public void DistroyFire()
    {
        Destroy(Fire[0]);
        Destroy(Fire[1]);
        Destroy(Fire[2]);
    }

    public void FlyUp()
    {
        Flyok = false;
    }

    public void FlyDown()
    {
        Flyok = true;
    }







    private void OnDrawGizmos() // ������ �÷��̾� ã�� �˰���
    {
        if (!DebugMode) return;
        Vector3 myPos = transform.position + Vector3.up * 0.5f;

        float lookingAngle = transform.eulerAngles.y;  //ĳ���Ͱ� �ٶ󺸴� ������ ����
        Vector3 rightDir = AngleToDir(transform.eulerAngles.y + ViewAngle * 0.5f);
        Vector3 leftDir = AngleToDir(transform.eulerAngles.y - ViewAngle * 0.5f);
        Vector3 lookDir = AngleToDir(lookingAngle);

        Debug.DrawRay(myPos, rightDir * ViewRadius, Color.blue);//������ ����
        Debug.DrawRay(myPos, leftDir * ViewRadius, Color.blue);// ���� ����
        Debug.DrawRay(myPos, lookDir * ViewRadius, Color.cyan);// �߽�

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
        float lookingAngle = transform.eulerAngles.y;  //ĳ���Ͱ� �ٶ󺸴� ������ ����
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
