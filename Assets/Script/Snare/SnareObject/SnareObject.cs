using UnityEngine;
using System.Collections;

public abstract class SnareObject : IPool
{
    public virtual void SetParam(string id)
    {
        mID = id;
    }

    protected abstract void ResetParams();
}
