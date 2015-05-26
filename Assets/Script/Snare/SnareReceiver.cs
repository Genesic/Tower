using UnityEngine;
using System.Collections;

/// <summary>陷阱接收基底類別</summary>
public abstract class SnareReceiver : MonoBehaviour, ISnareEvent
{
    [SerializeField]
    protected GameObject m_FallStonePrefab = null;

    protected abstract string EmitObjectID { get; }


    protected virtual void Awake() { }

    public abstract void OnSnareTrigger();
}
