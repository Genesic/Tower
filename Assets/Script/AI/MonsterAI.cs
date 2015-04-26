using UnityEngine;
using System.Collections;

public abstract class MonsterAI : MonoBehaviour
{
    public static readonly int IDLE_HASH = Animator.StringToHash("idle");
    public static readonly int SPAWN_HASH = Animator.StringToHash("spawn");
    public static readonly int DEATH_HASH = Animator.StringToHash("death");
    public static readonly int ATTACK_HASH = Animator.StringToHash("attack");
    public static readonly int DANCE_HASH = Animator.StringToHash("dance");
    public static readonly int WALK_HASH = Animator.StringToHash("walk");

    [SerializeField]
    protected float m_DetectInterval = 0.5f;

    protected Transform mTs = null;

    protected Transform mTargetTs = null;

    protected Animator mAni = null;

    protected NavMeshAgent mAgent = null;

    protected int HP = 100;

    private Coroutine mDetectCoroutine = null;

    protected bool IsAlive { get { return HP > 0; } }
    protected bool IsDeath { get { return !IsAlive; } }

    protected virtual void Awake()
    {
        mTs = transform;

        mAgent = GetComponent<NavMeshAgent>();
        mAni = GetComponentInChildren<Animator>();

        SetSpawn();
    }
    
    protected virtual void OnDestroy()
    {
        
    }

    protected virtual void Update()
    {
        WalkUpdate();
    }

    protected void StartDetect()
    {
        StopDetect();

        mDetectCoroutine = StartCoroutine(LoopDetect());
    }

    protected IEnumerator LoopDetect()
    {
        while (true)
        {
            Follow();

            yield return new WaitForSeconds(m_DetectInterval);
        }
    }

    protected void Follow()
    {
        if (mTargetTs != null)
            mAgent.SetDestination(mTargetTs.position);
    }

    protected void StopDetect()
    {
        if (mDetectCoroutine != null)
        {
            StopCoroutine(mDetectCoroutine);
            mDetectCoroutine = null;

            mAgent.Stop();
        }
    }

    public void SetTarget(Transform targetTs)
    {
        mTargetTs = targetTs;
    }

	public int getHp()
	{
		return HP;
	}

    public void Damage(int damage)
    {
        HP = Mathf.Max(0, HP - damage);

        if (IsDeath)
            SetDeath();
    }

    protected void SetSpawn()
    {
        StartCoroutine(SpawnHandle());
    }

    protected IEnumerator SpawnHandle()
    {
        StopDetect();
        SetAnimTrigger(MonsterAI.SPAWN_HASH);

        yield return new WaitForSeconds(1f);

        if (IsAlive)
            SetIdle();
    }

    protected void SetIdle()
    {
        StartCoroutine(IdleHandle());
    }

    protected IEnumerator IdleHandle()
    {
        StopDetect();
        SetAnimTrigger(MonsterAI.IDLE_HASH);

        yield return new WaitForSeconds(0.1f);

        if (IsAlive)
            StartDetect();
    }

    protected void SetDeath()
    {
        StartCoroutine(DeathHandle());
    }

    protected IEnumerator DeathHandle()
    {
        StopDetect();
        SetAnimTrigger(MonsterAI.DEATH_HASH);

        yield return new WaitForSeconds(0.1f);

        //call 回收
    }

    protected void SetAttack()
    {
        StopDetect();
        SetAnimTrigger(MonsterAI.ATTACK_HASH);
    }

    protected void SetDance()
    {
        StopDetect();
        SetAnimTrigger(MonsterAI.DANCE_HASH);
    }

    protected void WalkUpdate()
    {
        bool isWalk = !mAgent.velocity.normalized.Equals(Vector3.zero);

        SetAnimBool(MonsterAI.WALK_HASH, isWalk);
    }

    protected void SetAnimTrigger(int hash)
    {
        mAni.SetTrigger(hash);
    }

    protected void SetAnimBool(int hash, bool value)
    {
        mAni.SetBool(hash, value);
    }

}
