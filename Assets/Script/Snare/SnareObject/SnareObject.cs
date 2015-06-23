using UnityEngine;
using System.Collections;

public abstract class SnareObject : IPool
{
    public Transform Ts { get; private set; }

    protected virtual void Awake()
    {
        Ts = transform;
    }

    public virtual void SetParam(string id)
    {
        mID = id;
    }

    protected abstract void ResetParams();
}
