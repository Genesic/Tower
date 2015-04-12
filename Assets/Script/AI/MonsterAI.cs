using UnityEngine;
using System.Collections;

public class MonsterAI : MonoBehaviour
{
    [SerializeField]
    protected float m_DetectInterval = 0.5f;

    [SerializeField]
    protected Transform m_TargetTs = null;

    protected NavMeshAgent mAgent = null;

    void Awake()
    {
        mAgent = GetComponent<NavMeshAgent>();

        StartDetect();
    }

    private void StartDetect()
    {
        StartCoroutine(LoopDetect());
    }

    private IEnumerator LoopDetect()
    {
        while (true)
        {
            Follow();

            yield return new WaitForSeconds(m_DetectInterval);
        }
    }

    private void Follow()
    {
        if (m_TargetTs != null)
            mAgent.SetDestination(m_TargetTs.position);
    }

    public void SetTarget(Transform targetTs)
    {
        m_TargetTs = targetTs;
    }
}
