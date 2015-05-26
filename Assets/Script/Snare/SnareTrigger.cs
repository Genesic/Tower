using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;

/// <summary>陷阱觸發器</summary>
public class SnareTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject m_StartupEffectPrefab = null;

    [SerializeField]
    private BoxCollider m_BoxCD = null;

    [SerializeField]
    private TriggerType m_TriggerType = TriggerType.Enter;

    [SerializeField]
    private float m_StartupTime = 10f;

    [SerializeField]
    private GameObject[] m_SnareListenerList = null;

    private GameObject mStartupEffectGo = null;
    private GameObject StartupEffectGo
    {
        get
        {
            if (mStartupEffectGo == null)
            {
                mStartupEffectGo = Instantiate<GameObject>(m_StartupEffectPrefab);
                var ts = mStartupEffectGo.transform;
                ts.SetParent(transform);
                ts.localPosition = new Vector3(0f, 0.1f, 0f);
                ts.localRotation = Quaternion.identity;
            }

            return mStartupEffectGo;
        }
    }

    void Awake()
    {
        StartupReady();
    }

    void OnTriggerEnter(Collider other)
    {
        if (m_TriggerType != TriggerType.Enter)
            return;

        TriggerSnare();
    }

    void OnTriggerExit(Collider other)
    {
        if (m_TriggerType != TriggerType.Exit)
            return;

        TriggerSnare();
    }

    //void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireCube(transform.position, m_BoxCD.size * 2f);
    //}

    private void StartupReady()
    {
        Stop();
        StartCoroutine(CoStartupReady());
    }

    private IEnumerator CoStartupReady()
    {
        yield return new WaitForSeconds(m_StartupTime);

        Startup();
    }

    private void Startup()
    {
        m_BoxCD.enabled = true;

        PlayIdleEffect(true);
    }

    private void Stop()
    {
        m_BoxCD.enabled = false;

        PlayIdleEffect(false);
    }

    private void TriggerSnare()
    {
        StartupReady();

        SendEvent();
    }

    private void SendEvent()
    {
        foreach (var listenerGo in m_SnareListenerList)
        {
            ExecuteEvents.Execute<ISnareEvent>(listenerGo, null, (x, y) => x.OnSnareTrigger());
        }
    }

    private void PlayIdleEffect(bool _active)
    {
        StartupEffectGo.SetActive(_active);
    }

    protected void PlayEffect(string id, Vector3 offset)
    {
        var setting = EffectManager.Instance.Obtain(id);
        setting.SetPosition(transform.position + offset);
        setting.SetRotation(transform.rotation);
        setting.SetEnable();
    }

    public enum TriggerType
    {
        Enter,
        Exit
    }
}
