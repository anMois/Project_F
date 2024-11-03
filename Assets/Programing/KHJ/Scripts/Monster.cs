using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Monster : MonoBehaviour, IDamageable
{
    public UnityAction OnAlarm;
    public UnityAction<Monster> OnDead;

    public enum State
    {
        Idle, Trace, Guard, Dead, SIZE
    }
    [SerializeField] State curState;
    protected BaseState[] states = new BaseState[(int)State.SIZE];

    [Header("Enemy")]
    [SerializeField] protected Transform target;
    [SerializeField] protected LayerMask targetLayerMask;

    [Header("Range")]
    [Tooltip("Guard Range: yellow\n" +
        "Attack Max Range: red")]
    [SerializeField] protected bool visualization;
    [SerializeField] protected float attackMaxRange;
    [SerializeField] protected float guardRange;

    [Header("Attributes")]
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
        Vector3 direction = (target.position - transform.position).normalized;
        Ray vision = new Ray(transform.position, direction);

        Physics.Raycast(vision, out RaycastHit hit, guardRange, targetLayerMask);

        return (hit.transform != null) ? hit.transform.CompareTag("Player") : false;
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

        animator.SetTrigger("Impact");
    }

    protected virtual void Attack()
    {
        animator.SetTrigger("Attack");
        animator.SetInteger("Attack Motion", UnityEngine.Random.Range(0, 2));
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
            monster.agent.speed = monster.moveSpeed;
            monster.agent.destination = monster.target.position;

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
        }

        public override void StateUpdate()
        {
            // Dead
            Die();
        }

        protected void Die()
        {
            monster.OnDead?.Invoke(monster);

            monster.OnAlarm -= monster.Trace;

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
