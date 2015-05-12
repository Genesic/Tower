using UnityEngine;
using UnityEngine.UI;
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

    private StatusManager statusManager;
    private ErrMessage errMsg;

    public int getLevel { get { return mCannon.Level; } }
    public string getName { get { return mCannon.towerName; } }
    public int getAtk { get { return mCannon.Damage; } }
    public float getSpd { get { return mCannon.Speed; } }
    public int getPrice { get { return mCannon.Price; } }

    void Awake()
    {
        mTs = transform;

        GameObject status = GameObject.Find("GameManager");

        if (status == null)
        {
            return;
        }

        statusManager = status.GetComponent<StatusManager>();

        GameObject ErrMsg = GameObject.FindGameObjectWithTag("ErrMsg");
        if (ErrMsg)
            errMsg = ErrMsg.GetComponent<ErrMessage>();
    }
    /// <summary>檢查可否建置砲塔</summary>
    public bool checkBuildCannon(ICannon cannon)
    {
        if (HasCannon)
        {
            errMsg.show_message("Here Already Build Tower!!");
            Debug.LogErrorFormat("位置:{0} 已放置砲塔:{1}", name, ID);
            return false;
        }

        int cost = cannon.Cost;
        if (statusManager.getMoney < cost)
        {
            errMsg.show_message("Need More Money!!");
            return false;
        }

        return true;
    }

    /// <summary>建置砲塔</summary>
    public void BuildCannon(ICannon cannon)
    {
        if (checkBuildCannon(cannon))
        {
            int cost = cannon.Cost;
            statusManager.updateMoney(-cost);
            mCannon = cannon;
        }
    }


    /// <summary>賣掉砲塔</summary>
    public void SellCannon()
    {
        if (IsEmpty)
        {
            Debug.LogErrorFormat("位置:{0} 沒有砲塔可移除", name);
            return;
        }

        int price = mCannon.Price;
        statusManager.updateMoney(price);
        mCannon.destroy();
        mCannon = null;
    }

	void OnMouseDown() {
	}
}
