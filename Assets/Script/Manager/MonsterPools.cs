using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonsterPools : ObjectPools<MonsterPools, MonsterAI>
{
    protected override Transform ContainerTs { get { return mGameMgr.MapMgr.MonsterContainerTs; } }

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }

    public static void Retrieve(MonsterAI obj)
    {
        Instance.InsRetrieve(obj);
    }

    public override MonsterAI CreateNew(string id)
    {
        var path = string.Format("Monster/{0}", id);
        var prefab = Resources.Load<GameObject>(path);
        var enemyGo = Instantiate(prefab) as GameObject;
        var enemyTs = enemyGo.transform;
        enemyTs.SetParent(ContainerTs);

        var monsterAI = enemyTs.GetComponent<MonsterAI>();

        return monsterAI;
    }
}

/*
public class MonsterPools : MonoSingleTon<MonsterPools>
{
    private Transform MonsterContainerTs { get { return mGameMgr.StageMgr.MonsterContainerTs; } }

    private GameManager mGameMgr = null;

    private Dictionary<string, Queue<MonsterAI>> mMonsterDict = new Dictionary<string, Queue<MonsterAI>>();

    protected override void Awake()
    {
        base.Awake();

        mGameMgr = GetComponent<GameManager>();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }

    public static void Retrieve(MonsterAI monsterAI)
    {
        Instance.InsRetrieve(monsterAI);
    }

    public void InsRetrieve(MonsterAI monsterAI)
    {
        string id = monsterAI.MonsterID;

        if (!mMonsterDict.ContainsKey(id))
            mMonsterDict[id] = new Queue<MonsterAI>();

        monsterAI.SetDisable();

        var queue = mMonsterDict[id];
        queue.Enqueue(monsterAI);
    }

    public MonsterAI Obtain(string monsterID)
    {
        Queue<MonsterAI> queue;

        if (mMonsterDict.TryGetValue(monsterID, out queue))
        {
            if (queue.Count > 0)
                return queue.Dequeue();
        }

        return CreateMonster(monsterID);
    }

    public MonsterAI CreateMonster(string monsterID)
    {
        var path = string.Format("Monster/{0}", monsterID);
        var prefab = Resources.Load<GameObject>(path);
        var enemyGo = Instantiate(prefab) as GameObject;
        var enemyTs = enemyGo.transform;
        enemyTs.SetParent(MonsterContainerTs);

        var monsterAI = enemyTs.GetComponent<MonsterAI>();

        return monsterAI;
    }
}
*/