using UnityEngine;
using System.Collections;

/// <summary>砲台管理類別</summary>
public class CannonPlatform : MonoBehaviour
{
    private static readonly Vector3 OFFSET = new Vector3(0f, 0.5f, 0f);

    private ICannon mCannon = null;

    private Transform mTs = null;

    public Vector3 Position { get { return mTs.position + OFFSET; } }

    public bool IsEmpty { get { return mCannon == null; } }
    public bool HasCannon { get { return !IsEmpty; } }

    public string ID { get { return name; } }

    void Awake()
    {
        mTs = transform;
    }

    /// <summary>建置砲塔</summary>
    public void BuildCannon(ICannon cannon)
    {
        if (HasCannon)
        {
            Debug.LogErrorFormat("位置:{0} 已放置砲塔:{1}", name, ID);
            return;
        }

        mCannon = cannon;
    }

    /// <summary>賣掉砲塔</summary>
    public void SellCannon()
    {
        if (IsEmpty)
        {
            Debug.LogErrorFormat("位置:{0} 沒有砲塔可移除", name);
            return;
        }

        mCannon = null;
    }
}
