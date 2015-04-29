using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
