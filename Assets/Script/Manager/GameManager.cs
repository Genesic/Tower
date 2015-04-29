using UnityEngine;
using System.Collections;

/// <summary>遊戲管理類別</summary>
public class GameManager : MonoSingleTon<GameManager>
{
    public MapManager StageMgr = null;

    protected override void Awake()
    {
        base.Awake();

        LoadStage();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }

    /// <summary>讀取場景</summary>
    private void LoadStage()
    {
        SceneManager.LoadStage(1);
    }

    public void LevelLoadComplete(MapManager stageMgr)
    {
        StageMgr = stageMgr;
    }
}
