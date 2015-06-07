using UnityEngine;
using UnityEngine.Events;

using System.Collections;
using System.Collections.Generic;

using System.Linq;

public class BuffSystem
{
    public event UnityAction<float> OnMoveSpeedChanged = null;

    private float mMoveSpeed;
    public float MoveSpeed
    {
        get { return mMoveSpeed; }
        private set
        {
            if (mMoveSpeed != value)
            {
                mMoveSpeed = value;

                if (OnMoveSpeedChanged != null)
                    OnMoveSpeedChanged(MoveSpeed);
            }
        }
    }

    /// <summary>Buff 容器(所有中的 Buff 都會儲存在這)</summary>
    private List<BuffData> mBuffs = new List<BuffData>();

    /// <summary>需要被移除的 Buff</summary>
    private List<BuffData> mRemoveBuffs = new List<BuffData>();

    /// <summary有更動到的 Buff 類型</summary>
    private HashSet<BuffType> mRemoveBuffTypes = new HashSet<BuffType>();

    public void Init(MonsterAI.MonsterParam param)
    {
        MoveSpeed = param.MoveSpeed;
    }

    /// <summary>加入一個 Buff</summary>
    public void ApplyBuff(BuffData buffData)
    {
        if (IsApply(buffData))
        {
            mBuffs.Add(buffData);

            Apply();
        }
    }

    /// <summary>判定該 Buff 是否可以被加入</summary>
    private bool IsApply(BuffData buffData)
    {
        //先簡單的判斷已經中過的 Buff 就不能在中
        return !mBuffs.Exists(buff => buff.BuffType == buffData.BuffType);
    }

    public void Update()
    {
        //計算時間
        BuffUpdate();
    }

    private void BuffUpdate()
    {
        for (int i = 0; i < mBuffs.Count; i++)
        {
            BuffData data = mBuffs[i];
            data.RemainTime = Mathf.Max(0f, data.RemainTime - Time.deltaTime);

            if (data.IsBuffEnd)
            {
                mBuffs.Remove(data);
                mRemoveBuffs.Add(data);
            }
        }

        while (mRemoveBuffs.Count > 0)
        {
            BuffData data = mRemoveBuffs[0];
            //Debug.Log("Remove Buff:" + data.BuffType + " Count:" + mRemoveBuffs.Count);

            mRemoveBuffs.Remove(data);
            mRemoveBuffTypes.Add(data.BuffType);
        }

        if (mRemoveBuffTypes.Count > 0)
        {
            Apply();

            mRemoveBuffTypes.Clear();
        }
    }

    private void Apply()
    {
        //平均速度
        MoveSpeed = mBuffs.Count == 0 ? 1f : mBuffs.Where(data => data.BuffType == BuffType.MoveSpeed).Average(data => data.BuffValue);
    }

}
