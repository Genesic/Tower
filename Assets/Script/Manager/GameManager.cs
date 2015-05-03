using UnityEngine;
using System.Collections;

/// <summary>遊戲管理類別</summary>
public class GameManager : MonoSingleTon<GameManager>
{
    #region Field

    public MapManager MapMgr { get; private set; }

    public EnemySpawnManager EnemySpawnMgr { get; private set; }

    public SoundManager SoundMgr { get; private set; }

    public StatusManager StatusMgr { get; private set; }

    #endregion

    #region Unity Event

    protected override void Awake()
    {
        base.Awake();

        LoadStage();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }

    #endregion

    #region 初始化

    /// <summary>讀取場景</summary>
    private void LoadStage()
    {
        SceneManager.LoadStage(1);
    }

    public void LevelLoadComplete(MapManager stageMgr)
    {
        MapMgr = stageMgr;

        InitComponent();

        StartCoroutine(UpdateEnvironment());
    }

    /// <summary>初始化元件</summary>
    private void InitComponent()
    {
        EnemySpawnMgr = GetComponent<EnemySpawnManager>();
        SoundMgr = GetComponent<SoundManager>();
        StatusMgr = GetComponent<StatusManager>();

        gameObject.AddComponent<EffectManager>();
    }

    private IEnumerator UpdateEnvironment()
    {
        yield return new WaitForSeconds(1f);

        DynamicGI.UpdateEnvironment();
    }

    #endregion

    #region 測試用

    void OnGUI()
    {
        if (GUILayout.Button("產生單隻怪物")) EnemySpawnMgr.SpawnEnemy();
        if (GUILayout.Button("怪物腳本開始")) EnemySpawnMgr.StartSpawnEnemy();
        if (GUILayout.Button("讓怪物全死亡")) EnemySpawnMgr.MonsterAllDie();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) EnemySpawnMgr.SpawnEnemy();

        if (Input.GetKeyDown(KeyCode.Alpha1)) DynamicGI.UpdateEnvironment();
    }

    #endregion

}
