using UnityEngine;
using System.Collections;

public class BuffData
{
    /// <summary>Buff 類型</summary>
    public BuffType BuffType = BuffType.None;

    /// <summary>維持時間</summary>
    public float RemainTime = 0f;

    /// <summary>效果是否結束</summary>
    public bool IsBuffEnd { get { return RemainTime <= 0f; } }

    /// <summary>目前 效果的值</summary>
    public float BuffValue { get; protected set; }

    public BuffData(BuffType buffType, float remainTime, float buffValue)
    {
        BuffType = buffType;
        RemainTime = remainTime;
        BuffValue = buffValue;
    }
}

[System.Flags]
public enum BuffType
{
    None = 1 << 0,
    MoveSpeed = 1 << 1,

}

/*public abstract class BuffData<T> : BuffData
{
    public T BuffValue { get; protected set; }

    public BuffData(BuffType type, float remainTime, T buffValue)
        : base(type, remainTime)
    {
        BuffValue = buffValue;
    }

    //public override float GetFloatValue()
    //{
    //    return System.Convert.ToSingle(BuffValue);
    //}
}*/
