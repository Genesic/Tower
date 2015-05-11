using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public abstract class MonsterAI : IPool
{
    public static readonly int IDLE_HASH = Animator.StringToHash("idle");
    public static readonly int SPAWN_HASH = Animator.StringToHash("spawn");
    public static readonly int DEATH_HASH = Animator.StringToHash("death");
    public static readonly int ATTACK_HASH = Animator.StringToHash("attack");
    public static readonly int DANCE_HASH = Animator.StringToHash("dance");
    public static readonly int WALK_HASH = Animator.StringToHash("walk");

    protected string RebornEffect { get { return "MagicAuras/Prefabs/Shadow/GroundShadow"; } }
    protected bool IsAlive { get { return m_UseParam.HP > 0; } }
    protected bool IsDeath { get { return !IsAlive; } }

    private AudioSource mAudio = null;
    protected AudioSource Audio
    {
        get
        {
            if (mAudio == null)
            {
                mAudio = gameObject.AddComponent<AudioSource>();
                mAudio.playOnAwake = false;
                mAudio.spatialBlend = 1f;
                mAudio.minDistance = 10;
                mAudio.maxDistance = 20f;
            }

            return mAudio;
        }
    }

    public float RemainDistance { get; private set; }

    protected StatusManager StatusMgr;

    [SerializeField]
    protected int m_money;

    [SerializeField]
    protected int m_score;

    [SerializeField]
    protected float m_DetectInterval = 0.5f;

    [SerializeField]
    protected string m_MonsterID = "Unknow";

    [SerializeField]
    protected MonsterParam m_OriginParam = null;

    [SerializeField]
    protected MonsterParam m_UseParam = null;

    protected Transform mTs = null;

    protected Transform mTargetTs = null;

    protected Vector3 mAttackPoint = Vector3.zero;

    protected Animator mAni = null;

    protected NavMeshAgent mAgent = null;

    private Coroutine mLoopCoroutine = null;

    private MonsterHUD mMonsterHUD = null;
    
    private MonsterAction mMonsterAction = MonsterAction.Idle;

    private Dictionary<MonsterAction, System.Func<IEnumerator>> mMonsterActionDict = new Dictionary<MonsterAction, System.Func<IEnumerator>>();

    protected virtual void Awake()
    {
        mTs = transform;

        mID = m_MonsterID;

        mAgent = GetComponent<NavMeshAgent>();
        mAni = GetComponentInChildren<Animator>();

        StatusMgr = GameManager.Instance.StatusMgr;

        mMonsterActionDict.Add(MonsterAction.Spawn, SpawnHandle);
        mMonsterActionDict.Add(MonsterAction.Idle, IdleHandle);
        mMonsterActionDict.Add(MonsterAction.Attack, AttackHandle);
        mMonsterActionDict.Add(MonsterAction.Move, MoveHandle);
        mMonsterActionDict.Add(MonsterAction.Death, DeathHandle);
        mMonsterActionDict.Add(MonsterAction.Dance, DanceHandle);
    }

    protected virtual void OnDestroy()
    {

    }

    protected virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1)) SetMonsterAction(MonsterAction.Spawn);
        if (Input.GetKeyDown(KeyCode.Keypad2)) SetMonsterAction(MonsterAction.Idle);
        if (Input.GetKeyDown(KeyCode.Keypad3)) SetMonsterAction(MonsterAction.Move);
        if (Input.GetKeyDown(KeyCode.Keypad4)) SetMonsterAction(MonsterAction.Attack);
        if (Input.GetKeyDown(KeyCode.Keypad5)) SetMonsterAction(MonsterAction.Death);
        if (Input.GetKeyDown(KeyCode.Keypad6)) SetMonsterAction(MonsterAction.Dance);

        if (Input.GetKeyDown(KeyCode.Keypad7)) FollowStart();
        if (Input.GetKeyDown(KeyCode.Keypad8)) FollowEnd();
        if (Input.GetKeyDown(KeyCode.Keypad9)) PlaySfx("attack_01");
    }

    public override void SetEnable()
    {
        base.SetEnable();

        InitParam();
    }

    public override void SetDisable()
    {
        base.SetDisable();

        HUDManager.Retrieve(mMonsterHUD);

        mMonsterHUD = null;
    }

    private void InitParam()
    {
        m_UseParam.SetTo(m_OriginParam);

        mAgent.speed = m_UseParam.MoveSpeed;

        mAttackPoint = Vector3.zero;

        mMonsterHUD = HUDManager.Instance.Obtain("MonsterHUD");
        mMonsterHUD.SetEnable();
        mMonsterHUD.SetTarget(mTs);
        mMonsterHUD.SetHealth(GetPercentHP());

        SetMonsterAction(MonsterAction.Spawn);

        RunLoop();
    }

    protected void RunLoop()
    {
        mLoopCoroutine = StartCoroutine(Loop());
    }

    protected IEnumerator Loop()
    {
        while (true)
        {
            yield return StartCoroutine(mMonsterActionDict[mMonsterAction]());
        }
    }

    protected void SetMonsterAction(MonsterAction monsterAction)
    {
        mMonsterAction = monsterAction;
        //Debug.LogFormat("SetMonsterAction:{0}", mMonsterAction);
    }

    protected void FollowStart()
    {
        if (mTargetTs == null)
            return;

        mAgent.ResetPath();
        mAgent.SetDestination(mAttackPoint);
    }

    protected void FollowEnd()
    {
        mAgent.Stop();
    }

    public void SetPosition(Vector3 position)
    {
        mTs.position = position;
    }

    public void SetRotation(Quaternion rotation)
    {
        mTs.rotation = rotation;
    }

    public void SetTarget(Transform targetTs)
    {
        mTargetTs = targetTs;
        mAttackPoint = mTargetTs.position + MathUtility.GetRandomRadiusPoint(4f);

        SetRemain();
    }

    public int getHp()
    {
        return m_UseParam.HP;
    }

    public void Damage(int damage)
    {
        m_UseParam.HP = Mathf.Max(0, m_UseParam.HP - damage);

        mMonsterHUD.SetHealth(GetPercentHP());

        if (IsDeath)
        {
            SetMonsterAction(MonsterAction.Death);
        }
    }

    private float GetPercentHP()
    {
        return (float)m_UseParam.HP / (float)m_OriginParam.HP;
    }

    private void SetRemain()
    {
        NavMeshPath path = new NavMeshPath();

        mAgent.CalculatePath(mAttackPoint, path);

        //Debug.Log("corners:" + path.corners.Length);

        float remain = 0f;

        for (int i = 0; i < path.corners.Length - 1; i++)
        {
            remain += Vector3.Distance(path.corners[i], path.corners[i + 1]);
        }

        RemainDistance = remain;
    }

    #region Monster State

    protected IEnumerator SpawnHandle()
    {
        SetAnimTrigger(MonsterAI.SPAWN_HASH);

        yield return new WaitForSeconds(0.1f);

        PlayEffect(RebornEffect, new Vector3(0f, 0.2f, 0f));

        yield return new WaitForSeconds(1f);

        if (IsAlive)
        {
            SetMonsterAction(MonsterAction.Idle);
        }
    }

    protected IEnumerator IdleHandle()
    {
        SetAnimTrigger(MonsterAI.IDLE_HASH);

        SetMonsterAction(MonsterAction.Move);

        yield return null;
    }

    protected IEnumerator AttackHandle()
    {
        mTs.forward = (mTargetTs.position - mTs.position).normalized;

        while (mMonsterAction == MonsterAction.Attack)
        {
            PlayAttackAnim();

            yield return new WaitForSeconds(1.5f);

            //Debug.Log("Call CoreBase");
        }
    }
    
    protected IEnumerator MoveHandle()
    {
        SetAnimBool(MonsterAI.WALK_HASH, true);
        FollowStart();

        while (mMonsterAction == MonsterAction.Move)
        {
            SetRemain();

            if (RemainDistance <= mAgent.stoppingDistance)
            {
                SetMonsterAction(MonsterAction.Attack);
            }

            yield return new WaitForSeconds(1f);
        }

        SetAnimBool(MonsterAI.WALK_HASH, false);
        FollowEnd();
    }

    protected IEnumerator DeathHandle()
    {
        SetAnimTrigger(MonsterAI.DEATH_HASH);

        FollowEnd();

        StatusMgr.updateKill(1);
        StatusMgr.updateMoney(m_money);
        StatusMgr.updateScore(m_score);

        yield return new WaitForSeconds(5f);

        MonsterPools.Retrieve(this);
    }

    protected IEnumerator DanceHandle()
    {
        yield return null;
    }

    #endregion

    #region Animation

    protected void PlayAttackAnim()
    {
        PlaySfx("attack_01");
        SetAnimTrigger(MonsterAI.ATTACK_HASH);
    }

    protected void PlayDanceAnim()
    {
        SetAnimTrigger(MonsterAI.DANCE_HASH);
    }

    protected void SetAnimTrigger(int hash)
    {
        mAni.SetTrigger(hash);
    }

    protected void SetAnimBool(int hash, bool value)
    {
        mAni.SetBool(hash, value);
    }

    #endregion

    #region Effect

    protected void PlayEffect(string id)
    {
        PlayEffect(id, Vector3.zero);
    }

    protected void PlayEffect(string id, Vector3 offset)
    {
        var setting = EffectManager.Instance.Obtain(id);
        setting.SetPosition(mTs.position + offset);
        setting.SetRotation(mTs.rotation);
        setting.SetEnable();
    }

    #endregion

    #region sfx

    private void PlaySfx(string id)
    {
        Audio.PlayOneShot(SoundManager.GetAudioClip("attack_01"));
    }

    #endregion

    #region Data

    [System.Serializable]
    public class MonsterParam
    {
        public int HP = 0;
        public float MoveSpeed = 2f;

        public void SetTo(MonsterParam param)
        {
            HP = param.HP;
            MoveSpeed = param.MoveSpeed;
        }
    }

    public enum MonsterAction
    {
        Spawn,
        Idle,
        Move,
        Attack,
        Death,
        Dance
    }

    #endregion

    /*void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(mAttackPoint, 1f);
    }*/

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(mAttackPoint, 1f);
    }
}