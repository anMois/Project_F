using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour, IDamageable
{
    public enum State
    {
        Idle, Trace, Guard, Dead, SIZE
    }
    [SerializeField] State curState;
    protected BaseState[] states = new BaseState[(int)State.SIZE];

    [Header("Enemy")]
    [SerializeField] protected Transform target;

    [Header("Range")]
    [Tooltip("Visual Range: cyan\n" +
        "Trace Range: green\n" +
        "Guard Range: yellow\n" +
        "Attack Max Range: magenta\n" +
        "Attack Range: red")]
    [SerializeField] protected bool visualization;
    [SerializeField] protected float visualRange;
    [SerializeField] protected float traceRange;
    [SerializeField] protected float guardRange;
    [SerializeField] protected float attackMaxRange;
    [SerializeField] protected float attackRange;

    [Header("Attributes")]
    [SerializeField] protected int monsterID;
    [SerializeField] protected int maxHp;
    [SerializeField] protected int curHp;
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float attackCoolDown;
    [SerializeField] protected int dropRate;

    protected Rigidbody rb;

    protected void Awake()
    {
        states[(int)State.Idle] = new IdleState(this);
        states[(int)State.Trace] = new TraceState(this);
        states[(int)State.Guard] = new GuardState(this);
        states[(int)State.Dead] = new DeadState(this);

        rb = GetComponent<Rigidbody>();

        curHp = maxHp;

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

    public void TakeHit(int dmg)
    {
        curHp -= dmg;
        if(curHp < 0)
        {
            ChangeState(State.Dead);
        }
    }

    protected virtual void Attack()
    {
        throw new NotImplementedException();
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
            float distance = Vector3.Distance(monster.transform.position, monster.target.position);

            if(distance <= monster.visualRange)
            {
                monster.ChangeState(State.Trace);
            }
        }
    }

    protected class TraceState : MonsterState
    {
        public TraceState(Monster monster) : base(monster) { }

        public override void StateUpdate()
        {
            // Trace
            // Move to traceRange with gazing target
            Vector3 direction = (monster.target.position - monster.transform.position).normalized;
            monster.transform.LookAt(monster.target);
            monster.rb.velocity = monster.moveSpeed * direction;

            // Transition
            float distance = Vector3.Distance(monster.transform.position, monster.target.position);

            if (distance <= monster.traceRange)
            {
                monster.ChangeState(State.Guard);
            }
        }
    }

    protected class GuardState : MonsterState
    {
        public GuardState(Monster monster) : base(monster) { }

        public override void StateEnter()
        {
            attackCoroutine = CoroutineHelper.StartCoroutine(AttackRoutine());
        }

        public override void StateUpdate()
        {
            // Guard
            // Move horizontally with gazing target
            monster.transform.LookAt(monster.target);
            Guard();

            // Transition
            float distance = Vector3.Distance(monster.transform.position, monster.target.position);

            if (distance > monster.attackMaxRange)
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
        }

        protected void Guard()
        {
            float angle = monster.moveSpeed * 0.01f;
            monster.transform.RotateAround(monster.target.position, monster.target.up, angle);
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

        public override void StateUpdate()
        {
            // Dead
            Die();
        }

        protected void Die()
        {
            Destroy(monster.gameObject);
        }
    }
    #endregion

    protected void OnDrawGizmosSelected()
    {
        if (visualization == false)
            return;

        // Visualize detection range
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, visualRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, traceRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, guardRange);
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, attackMaxRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
