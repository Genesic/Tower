using UnityEngine;
using System.Collections;

/// <summary>砲台管理類別</summary>
public class CannonPlatform : MonoBehaviour
{
    private ICannon mCannon = null;

    public bool IsEmpty { get { return mCannon == null; } }
    public bool HasCannon { get { return !IsEmpty; } }

    public string ID { get { return name; } }

    public bool PutCannon(ICannon cannon)
    {
        if (HasCannon)
        {
            Debug.LogErrorFormat("位置:{0} 已放置砲塔:{1}", name, ID);
            return false;
        }

        mCannon = cannon;

        return true;
    }

    public ICannon RemoveCannon()
    {
        if (IsEmpty)
        {
            Debug.LogErrorFormat("位置:{0} 沒有砲塔可移除", name);
            return null;
        }

        var cannon = mCannon;

        mCannon = null;

        return cannon;
    }

}
