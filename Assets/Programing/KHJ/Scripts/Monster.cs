using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Monster : MonoBehaviour, IDamageable
{
    public UnityAction OnAlarm;
    public UnityAction<Monster> OnDead;

    public enum MonsterType
    {
        Goblin, Golem, FlyingEye, Serpent
    }

    public enum State
    {
        Idle, Trace, Guard, Dead, SIZE
    }
    [SerializeField] State curState;
    protected BaseState[] states = new BaseState[(int)State.SIZE];

    [Header("Enemy")]
    [SerializeField] protected Transform target;

    [Header("Range")]
    [Tooltip("Guard Range: yellow\n" +
        "Attack Max Range: red")]
    [SerializeField] protected bool visualization;
    [SerializeField] protected float attackMaxRange;
    [SerializeField] protected float guardRange;

    [Header("Attributes")]
    [SerializeField] private MonsterType monsterType;
    [SerializeField] protected int monsterID;
    [SerializeField] protected int maxHp;
    [SerializeField] protected int curHp;
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float guardSpeed;
    [SerializeField] protected int attackDamage;
    [SerializeField] protected float attackCoolDown;
    [SerializeField] protected int dropRate;

    protected Rigidbody rb;
    protected NavMeshAgent agent;
    protected Animator animator;

    private bool isColliderReaction = false;

    protected void Awake()
    {
        states[(int)State.Idle] = new IdleState(this);
        states[(int)State.Trace] = new TraceState(this);
        states[(int)State.Guard] = new GuardState(this);
        states[(int)State.Dead] = new DeadState(this);

        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        curHp = maxHp;
        OnAlarm += Trace;

        curState = State.Idle;
        states[(int)curState].StateEnter();
    }

    protected void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    protected void OnDestroy()
    {
        states[(int)curState].StateExit();
    }

    protected void Update()
    {
        states[(int)curState].StateUpdate();
    }

    public void ChangeState(State state)
    {
        states[(int)curState].StateExit();
        curState = state;
        states[(int)curState].StateEnter();
    }

    public bool GazePlayer()
    {
        Vector3 offset = new Vector3(0, 0.3f, 0);

        Vector3 direction = (target.position - transform.position).normalized;
        Ray vision = new Ray(transform.position + offset, direction);

        Physics.Raycast(vision, out RaycastHit hit, 1.5f * guardRange);
        
        return (hit.transform != null) ? hit.transform.CompareTag("Player") : true;
    }

    public void Trace()
    {
        if (curState != State.Idle)
            return;

        states[(int)curState].StateExit();
        curState = State.Trace;
        states[(int)curState].StateEnter();
    }

    public void TakeHit(int dmg)
    {
        OnAlarm?.Invoke();

        curHp -= dmg;
        if(curHp <= 0)
        {
            ChangeState(State.Dead);
        }

        // Prevent overlapping
        if (isColliderReaction)
            return;

        animator.SetTrigger("Impact");

        PlayHitSound();
    }

    protected virtual void Attack()
    {
        int attackMotion = UnityEngine.Random.Range(1, 3);

        animator.SetTrigger("Attack");
        animator.SetInteger("Attack Motion", attackMotion);

        PlayAttackSound();
    }

    private void PlayAttackSound()
    {
        switch (monsterType)
        {
            case MonsterType.Goblin:
                if (this is not RangedMonster)
                    SoundManager.Instance.GoblinAttackSound();
                break;
            case MonsterType.Golem:
                SoundManager.Instance.GolemAttackSound();
                break;
            case MonsterType.FlyingEye:
                SoundManager.Instance.EyeMonsterAttackSound();
                break;
            case MonsterType.Serpent:
                SoundManager.Instance.SnakeHumanAttackSound();
                break;
            default:
                break;
        }
    }

    private void PlayHitSound()
    {
        isColliderReaction = true;

        switch (monsterType)
        {
            case MonsterType.Goblin:
                SoundManager.Instance.GoblinHitSound();
                break;
            case MonsterType.Golem:
                SoundManager.Instance.GolemHitSound();
                break;
            case MonsterType.FlyingEye:
                SoundManager.Instance.EyeMonsterHitSound();
                break;
            case MonsterType.Serpent:
                SoundManager.Instance.SnakeHumanHitSound();
                break;
            default:
                break;
        }

        StartCoroutine(TurnOffColliderReaction());
    }

    private void PlayDieSound()
    {
        switch (monsterType)
        {
            case MonsterType.Goblin:
                SoundManager.Instance.GoblinDieSound();
                break;
            case MonsterType.Golem:
                SoundManager.Instance.GolemDieSound();
                break;
            case MonsterType.FlyingEye:
                SoundManager.Instance.EyeMonsterDieSound();
                break;
            case MonsterType.Serpent:
                SoundManager.Instance.SnakeHumanDieSound();
                break;
            default:
                break;
        }
    }

    IEnumerator TurnOffColliderReaction()
    {
        yield return new WaitForSeconds(0.1f);
        isColliderReaction = false;
    }

    #region MonsterState
    protected class MonsterState : BaseState
    {
        public Monster monster;
        public MonsterState(Monster monster) => this.monster = monster;

        public override void StateEnter() { }
        public override void StateExit() { }

        public override void StateUpdate()
        {
            throw new System.NotImplementedException();
        }
    }

    protected class IdleState : MonsterState
    {
        public IdleState(Monster monster) : base(monster) { }

        public override void StateUpdate()
        {
            // Idle
            // Keep position

            // Transition
            // Trace() method handles "Idle -> Trace" using event
        }
    }

    protected class TraceState : MonsterState
    {
        public TraceState(Monster monster) : base(monster) { }

        public override void StateEnter()
        {
            monster.agent.isStopped = false;

            monster.animator.SetTrigger("Trace");
            monster.animator.SetInteger("Guard Direction", 0);
        }

        public override void StateUpdate()
        {
            // Trace
            // Move to traceRange with gazing target
            monster.agent.destination = monster.target.position;
            if (monster.agent.hasPath)
            {
                float remainingDistance = monster.agent.remainingDistance;
                float slowDownDistance = 5 * monster.guardRange;
                float minSpeed = monster.moveSpeed / 5;

                // Slow down
                if (remainingDistance < slowDownDistance)
                    monster.agent.speed = Mathf.Lerp(minSpeed, monster.moveSpeed, remainingDistance / slowDownDistance);
                else if (remainingDistance < monster.guardRange)
                    monster.agent.speed = 0f;
                else
                    monster.agent.speed = monster.moveSpeed;
            }

            // Transition
            float distance = Vector3.Distance(monster.transform.position, monster.target.position);

            // In guard range and could see player
            if (distance <= monster.guardRange && monster.GazePlayer())
            {
                monster.agent.isStopped = true;
                monster.ChangeState(State.Guard);
            }
        }
    }

    protected class GuardState : MonsterState
    {
        private int sign;

        public GuardState(Monster monster) : base(monster) { }

        public override void StateEnter()
        {
            sign = (UnityEngine.Random.Range(0, 2) == 0) ? -1 : 1;
            attackCoroutine = CoroutineHelper.StartCoroutine(AttackRoutine());

            monster.animator.SetBool("Guard", true);
            monster.animator.SetInteger("Guard Direction", sign);
            monster.animator.SetTrigger("Draw");
        }

        public override void StateUpdate()
        {
            // Guard
            // Move horizontally with gazing target
            monster.transform.LookAt(monster.target);
            Guard();

            // Transition
            float distance = Vector3.Distance(monster.transform.position, monster.target.position);

            if (distance > monster.attackMaxRange || !monster.GazePlayer())
            {
                monster.ChangeState(State.Trace);
            }
        }

        public override void StateExit()
        {
            if (attackCoroutine != null)
            {
                CoroutineHelper.StopCoroutine(attackCoroutine);
                attackCoroutine = null;
            }

            monster.animator.SetBool("Guard", false);
        }

        protected void Guard()
        {
            float angle = monster.guardSpeed * 0.01f;
            monster.transform.RotateAround(monster.target.position, monster.target.up, sign * angle);
        }

        Coroutine attackCoroutine;
        IEnumerator AttackRoutine()
        {
            WaitForSeconds attackDelay = new WaitForSeconds(monster.attackCoolDown);

            while (true)
            {
                yield return attackDelay;
                monster.Attack();
            }
        }
    }

    protected class DeadState : MonsterState
    {
        public DeadState(Monster monster) : base(monster) { }

        public override void StateEnter()
        {
            monster.animator.SetTrigger("Death");

            // Dead
            Die();
        }

        public override void StateUpdate()
        {
        }

        protected void Die()
        {
            monster.OnDead?.Invoke(monster);

            monster.OnAlarm -= monster.Trace;

            monster.PlayDieSound();

            Destroy(monster.gameObject, 1f);
        }
    }
    #endregion

    protected void OnDrawGizmosSelected()
    {
        if (visualization == false)
            return;

        // Visualize detection range
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, guardRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackMaxRange);
    }
}
