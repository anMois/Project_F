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
    private BaseState[] states = new BaseState[(int)State.SIZE];

    [Header("Enemy")]
    [SerializeField] GameObject target;

    [Header("Range")]
    [Tooltip("Visual Range: cyan\n" +
        "Trace Range: green\n" +
        "Guard Range: yellow\n" +
        "Attack Max Range: magenta\n" +
        "Attack Range: red")]
    [SerializeField] bool visualization;
    [SerializeField] float visualRange;
    [SerializeField] float traceRange;
    [SerializeField] float guardRange;
    [SerializeField] float attackMaxRange;
    [SerializeField] float attackRange;

    [Header("Attributes")]
    [SerializeField] int monsterID;
    [SerializeField] int maxHp;
    [SerializeField] int curHp;
    [SerializeField] float moveSpeed;
    [SerializeField] float attackCoolDown;
    [SerializeField] int dropRate;

    //[Header("Projectile")]
    //[SerializeField] GameObject projectilePrefab;
    //[SerializeField] float projectileSpeed;

    private Rigidbody rb;

    private void Awake()
    {
        states[(int)State.Idle] = new IdleState(this);
        states[(int)State.Trace] = new TraceState(this);
        states[(int)State.Guard] = new GuardState(this);
        states[(int)State.Dead] = new DeadState(this);
    }

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");

        rb = GetComponent<Rigidbody>();

        curHp = maxHp;

        curState = State.Idle;
        states[(int)curState].StateEnter();
    }

    private void OnDestroy()
    {
        states[(int)curState].StateExit();
    }

    private void Update()
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

    public virtual void Attack()
    {
        throw new NotImplementedException();
    }

    #region MonsterState
    private class MonsterState : BaseState
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

    private class IdleState : MonsterState
    {
        public IdleState(Monster monster) : base(monster) { }

        public override void StateUpdate()
        {
            // Idle
            // Keep position

            // Transition
            float distance = Vector3.Distance(monster.transform.position, monster.target.transform.position);

            if(distance <= monster.visualRange)
            {
                monster.ChangeState(State.Trace);
            }
        }
    }

    private class TraceState : MonsterState
    {
        public TraceState(Monster monster) : base(monster) { }

        public override void StateUpdate()
        {
            // Trace
            // Move to traceRange
            Vector3 direction = (monster.target.transform.position - monster.transform.position).normalized;
            monster.rb.velocity = monster.moveSpeed * direction;

            // Transition
            float distance = Vector3.Distance(monster.transform.position, monster.target.transform.position);

            if (distance <= monster.traceRange)
            {
                monster.ChangeState(State.Guard);
            }
        }
    }

    private class GuardState : MonsterState
    {
        public GuardState(Monster monster) : base(monster) { }

        public override void StateEnter()
        {
            attackCoroutine = CoroutineHelper.StartCoroutine(AttackRoutine());
        }

        public override void StateUpdate()
        {
            // Guard
            // Move horizontally
            Guard();

            // Transition
            float distance = Vector3.Distance(monster.transform.position, monster.target.transform.position);

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

        private void Guard()
        {
            float angle = monster.moveSpeed * 0.1f;
            monster.transform.RotateAround(monster.target.transform.position, monster.target.transform.up, angle);
        }

        Coroutine attackCoroutine;
        IEnumerator AttackRoutine()
        {
            while (true)
            {
                Attack();
                yield return new WaitForSeconds(monster.attackCoolDown);
            }
        }

        private void Attack()
        {
            Debug.Log($"{monster.name} Attack!");
        }
    }

    private class DeadState : MonsterState
    {
        public DeadState(Monster monster) : base(monster) { }

        public override void StateUpdate()
        {
            // Dead
            Die();
        }

        private void Die()
        {
            Destroy(monster.gameObject);
        }
    }
    #endregion

    private void OnDrawGizmosSelected()
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
