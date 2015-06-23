using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class EnemySpawnManager : MonoSingleTon<EnemySpawnManager>
{
    private Transform SpawnTs { get { return mGameMgr.MapMgr.SpawnTs; } }

    private Transform TargetTs { get { return mGameMgr.MapMgr.TargetTs; } }

    [SerializeField]
    private Vector2 m_SpawnOffset = Vector2.zero;

    [SerializeField, Tooltip("每波間隔時間")]
    private float m_SpawnInterval = 2f;

    [SerializeField, Tooltip("每波怪物出生最小數量")]
    private int m_SpawnNumMin = 1;

    [SerializeField, Tooltip("每波怪物出生最大數量")]
    private int m_SpawnNumMax = 3;

    [SerializeField]
    private MonsterPools m_MonsterPools = null;

    private GameManager mGameMgr = null;

    private Coroutine mSpwanCoroutine = null;

    private Queue<MonsterAI> mAliveMonsterQueue = new Queue<MonsterAI>();

    protected override void Awake()
    {
        base.Awake();

        mGameMgr = GetComponent<GameManager>();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }

    public void StartSpawnEnemy()
    {
        mSpwanCoroutine = StartCoroutine(CoStartSpawnEnemy());
    }

    private IEnumerator CoStartSpawnEnemy()
    {
        while (true)
        {
            SpawnMultiEnemy();

            yield return new WaitForSeconds(m_SpawnInterval);
        }
    }

    private void SpawnMultiEnemy()
    {
        int num = Random.Range(m_SpawnNumMin, m_SpawnNumMax);

        for (int i = 0; i < num; i++)
            SpawnEnemy();
    }

    public void SpawnEnemy()
    {
        var monsterAI = m_MonsterPools.Obtain("tufu");
        monsterAI.SetPosition(GetSpawnPosition());
        monsterAI.SetRotation(SpawnTs.rotation);
        monsterAI.SetEnable();
        monsterAI.SetTarget(TargetTs);

        mAliveMonsterQueue.Enqueue(monsterAI);
    }

    /// <summary>取得最接近總部塔的怪物</summary>
    public MonsterAI GetCloseToBaseMonster()
    {
        Debug.Log("Count:" + mAliveMonsterQueue.Count);

        List<MonsterPathData> list = new List<MonsterPathData>();

        foreach (var monster in mAliveMonsterQueue)
        {
            list.Add(new MonsterPathData { Monster = monster, RemainDistance = monster.RemainDistance });
        }

        var result = list.OrderBy(data => data.RemainDistance);

        var nearest = result.FirstOrDefault();
        if (nearest != null)
        {
            return nearest.Monster;
        }

        return null;
    }

    private void StopSpawnEnemy()
    {
        if (mSpwanCoroutine != null)
        {
            StopCoroutine(mSpwanCoroutine);
            mSpwanCoroutine = null;
        }
    }

    private Vector3 GetSpawnPosition()
    {
        //return new Vector3(9f, 1f, -4f);
        //return new Vector3(45f, 12f, -5f);
        //return new Vector3(8f, 2f, 25f);

        var origin = SpawnTs.position;
        var randomPos = SpawnTs.rotation * new Vector3(Random.Range(-m_SpawnOffset.x, m_SpawnOffset.x), 0f, Random.Range(-m_SpawnOffset.y, m_SpawnOffset.y));

        return origin + randomPos;
    }

    public void MonsterAllDie()
    {
        while (mAliveMonsterQueue.Count > 0)
        {
            MonsterAI monster = mAliveMonsterQueue.Dequeue();
            monster.Damage(999999);
        }
    }

    public class MonsterPathData
    {
        public MonsterAI Monster = null;
        public float RemainDistance = 0f;
    }
}
