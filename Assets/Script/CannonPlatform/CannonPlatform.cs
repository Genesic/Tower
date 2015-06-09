using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>砲台管理類別</summary>
public class CannonPlatform : MonoBehaviour
{
    private static readonly Vector3 OFFSET = new Vector3(0f, 0.5f, 0f);

    // private ICannon mCannon = null;

	private Tower use_tower = null;

    private Transform mTs = null;

    public Vector3 Position { get { return mTs.position + OFFSET; } }

    public bool IsEmpty { get { return use_tower == null; } }
    public bool HasCannon { get { return !IsEmpty; } }

    public string ID { get { return name; } }

    private StatusManager statusManager;
	private UIManager uiManager;
	private ErrMessage errMsg;

	public int getLevel { get { return use_tower.Level; } }
	public string getName { get { return use_tower.towerName; } }
	public int getAtk { get { return use_tower.Damage; } }
	public float getSpd { get { return use_tower.Speed; } }
	public int getPrice { get { return use_tower.Price; } }
	public int getCost { get { return use_tower.Cost; } }
	public int getMaxLevel { get { return use_tower.maxLevel; } }

    void Awake()
    {
        mTs = transform;

        GameObject gameManager = GameObject.Find("GameManager");

        if (gameManager == null)
        {
            return;
        }

        statusManager = gameManager.GetComponent<StatusManager>();
		uiManager = gameManager.GetComponent<UIManager> ();
		errMsg = uiManager.getErrMsg ();
    }
    /// <summary>檢查可否建置砲塔</summary>
    public bool checkBuildCannon(Tower check_tower)
    {
        if (HasCannon)
        {
            errMsg.show_message("Here Already Build Tower!!");
            Debug.LogErrorFormat("位置:{0} 已放置砲塔:{1}", name, ID);
            return false;
        }
		
        return true;
    }

    /// <summary>建置砲塔</summary>
	public void BuildCannon(Tower check_tower)
    {
		if (checkBuildCannon(check_tower))
			use_tower = check_tower;        
    }


    /// <summary>賣掉砲塔</summary>
    public void SellCannon()
    {
        if (IsEmpty)
        {
            Debug.LogErrorFormat("位置:{0} 沒有砲塔可移除", name);
            return;
        }

		use_tower.destroy();
		use_tower = null;
    }

	public void LevelUpCannon()
	{
		if (IsEmpty)
		{
			Debug.LogErrorFormat("位置:{0} 沒有砲塔可升級", name);
			return;
		}

		use_tower.level_up();
	}

	void OnMouseDown() {
		uiManager.setMouseDownCubePanel (this);
	}
}
