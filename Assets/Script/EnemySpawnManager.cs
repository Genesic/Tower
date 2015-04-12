using UnityEngine;
using System.Collections;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField]
    private Transform m_SpawnTs = null;

    [SerializeField]
    private Transform m_TargetTs = null;

    void Awake()
    {
        StartSpawnEnemy();
    }

    private void StartSpawnEnemy()
    {
        StartCoroutine(CoStartSpawnEnemy());
    }

    private IEnumerator CoStartSpawnEnemy()
    {
        while (true)
        {
            CreateEnemy();

            yield return new WaitForSeconds(1f);
        }
    }

    private void StopSpawnEnemy()
    {

    }


    private void CreateEnemy()
    {
        var prefab = Resources.Load<GameObject>("Monster/tufu");
        var enemyGo = Instantiate(prefab) as GameObject;
        var enemyTs = enemyGo.transform;
        enemyTs.SetParent(m_SpawnTs);
        enemyTs.position = m_SpawnTs.position;
        enemyTs.rotation = m_SpawnTs.rotation;

        var monsterAI = enemyTs.GetComponent<MonsterAI>();
        monsterAI.SetTarget(m_TargetTs);

    }
}
