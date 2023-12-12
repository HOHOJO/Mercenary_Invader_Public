using UnityEngine;
using UnityEngine.AI;

public class AilenStateMachine : StateMachine, IHealthObserver
{
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public CharacterController Controller { get; private set; }
    [field: SerializeField] public NavMeshAgent NavAgent { get; private set; }
    [field: SerializeField] public WeaponDamage[] Weapons { get; private set; }
    [field: SerializeField] public HealthSystem Health { get; private set; }
    [field: SerializeField] public Ragdoll Ragdoll { get; private set; }
    [field: SerializeField] public float MoveSpeed { get; private set; }
    [field: SerializeField] public float PlayerDetectionRange { get; private set; }
    [field: SerializeField] public float PlayerAttackRange { get; private set; }
    [field: SerializeField] public int AttackDamage { get; private set; }
    [field: SerializeField] public GameObject Meteor { get; private set; }
    [field: SerializeField] private Material lowHealthMaterial;

    public GameObject Player { get; private set; }

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Controller = GetComponent<CharacterController>();

        NavAgent.updatePosition = false;
        NavAgent.updateRotation = false;

        ChangeState(new AilenIdleState(this));
    }

    private void OnEnable()
    {
        Health.OnTakeDamage += HandleTakeDamage;
        Health.SubscribeToHealthChanges(this);
    }

    private void OnDisable()
    {
        Health.OnTakeDamage -= HandleTakeDamage;
        Health.UnsubscribeFromHealthChanges(this);
    }

    private void HandleTakeDamage()
    {
        ChangeState(new AilenImpactState(this));
    }

    public void OnHealthStateChanged(HealthState newState)
    {
        if (newState == HealthState.Low)
        {
            HandleLowHealth();
        }
        else if (newState == HealthState.Critical)
        {
            HandleCriticalHealth();
        }
    }

    private void HandleLowHealth()
    {
        ChangeState(new AilenShootingState(this));
        NavAgent.speed = 3.0f;
        Meteor.SetActive(true);

        ChangeSkin();
    }

    private void HandleCriticalHealth()
    {
        ChangeState(new AilenDeadState(this));
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, PlayerDetectionRange);
    }

    private void ChangeSkin()
    {
        Renderer[] modelRenderers = GetComponentsInChildren<Renderer>();
        if (modelRenderers.Length > 0)
        {
            foreach (Renderer modelRenderer in modelRenderers)
            {
                Material[] materials = modelRenderer.materials;
                for (int i = 0; i < materials.Length; i++)
                {
                    materials[i] = lowHealthMaterial;
                }
                modelRenderer.materials = materials;
            }
        }
    }
}
