using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DaikonForge.Tween;
using DaikonForge.Tween.Interpolation;

/// <summary>遊戲管理類別</summary>
public class GameManager : MonoSingleTon<GameManager>
{
    #region Field

    [SerializeField]
    private bool m_UseSnare = true;
    public bool UseSnare { get { return m_UseSnare; } }

    public MapManager MapMgr { get; private set; }

	public HeroManager HeroMgr { get; private set; }

    public EnemySpawnManager EnemySpawnMgr { get; private set; }

    public SoundManager SoundMgr { get; private set; }

    public StatusManager StatusMgr { get; private set; }

    public HUD BaseHUD { get; private set; }

    public BaseCore CoreTarget { get; private set; }
    
    [SerializeField]
    private Canvas m_UICanvas = null;
    public Canvas UICanvas
    {
        get
        {
            if (m_UICanvas == null)
                m_UICanvas = GameObject.FindObjectOfType<Canvas>();
            return m_UICanvas;
        }
    }
    
    public Transform MoveCamTs { get; private set; }

    public Transform MainCamTs { get; private set; }

    private TweenGroup mCameraTween = null;
    private Tween<Vector3> mMoveCamTweenPos = null;
    private Tween<Vector3> mMoveCamTweenRot = null;

    private CameraAnim mCameraAnim = null;

    [SerializeField]
    private State mState = State.Ready;
    public State GameState { get { return mState; } }

    #endregion

    #region Unity Event

    protected override void Awake()
    {
        base.Awake();

        MoveCamTs = GameObject.Find("MoveCamera").transform;
        MainCamTs = MoveCamTs.GetComponentInChildren<Camera>().transform;
        mCameraAnim = MoveCamTs.GetComponent<CameraAnim>();

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

    public void LevelLoadComplete(MapManager mapMgr)
    {
        MapMgr = mapMgr;

        InitComponent();
        InitCameraTween();

        StartCoroutine(UpdateEnvironment());
    }

    /// <summary>初始化元件</summary>
    private void InitComponent()
    {
		HeroMgr = GetComponent<HeroManager>();
        EnemySpawnMgr = GetComponent<EnemySpawnManager>();
        SoundMgr = GetComponent<SoundManager>();
        StatusMgr = GetComponent<StatusManager>();

        gameObject.AddComponent<EffectManager>();
        gameObject.AddComponent<SnarePools>();
        gameObject.AddComponent<HUDManager>();
        CoreTarget = MapMgr.TargetTs.gameObject.AddComponent<BaseCore>();
        BaseHUD = UICanvas.GetComponent<HUD>();
    }

    private IEnumerator UpdateEnvironment()
    {
        yield return new WaitForSeconds(1f);

        DynamicGI.UpdateEnvironment();

        mCameraAnim.PlayIntroAnim();
    }

    #endregion

    #region 特寫最接近保壘的怪相關處裡

    private void InitCameraTween()
    {
        float duration = 0.5f;

        mMoveCamTweenPos = Tween<Vector3>.Obtain()
        .SetDuration(duration)
        .OnExecute(pos =>
        {
            MoveCamTs.position = pos;
        });

        mMoveCamTweenRot = Tween<Vector3>.Obtain()
        .SetDuration(duration)
        .OnExecute(forward =>
        {
            MoveCamTs.forward = forward;
        });

        mCameraTween = new TweenGroup();
        mCameraTween.SetMode(TweenGroupMode.Concurrent);
        mCameraTween.AppendTween(mMoveCamTweenPos, mMoveCamTweenRot);
    }
    
    public void StartGame()
    {
        mState = State.Start;

        EnemySpawnMgr.StartSpawnEnemy(5f);
    }

    public void LookAtNearestMonster()
    {
        var monster = EnemySpawnMgr.GetCloseToBaseMonster();
        if (monster == null)
            return;

        var monsterTs = monster.transform;
        var monsterPos = monsterTs.position;
        var monsterRot = monsterTs.rotation;

        Quaternion rot = monsterRot * Quaternion.AngleAxis(30f, Vector3.left);

        Vector3 camPos = monsterPos + rot * Vector3.forward * 10f;

        Vector3 camToMonster = (monsterPos - camPos).normalized;
        camToMonster.y = 0f;

        mMoveCamTweenPos.OnSyncStartValue(() => MoveCamTs.position);
        mMoveCamTweenPos.OnSyncEndValue(() => camPos);

        mMoveCamTweenRot.OnSyncStartValue(() => MoveCamTs.forward);
        mMoveCamTweenRot.OnSyncEndValue(() => camToMonster);
        
        mCameraTween.Play();
    }

    #endregion

    #region 測試用

    void OnGUI()
    {
        if (GUILayout.Button("產生單隻怪物")) EnemySpawnMgr.SpawnEnemy(MapMgr.SpawnTs.position);
        if (GUILayout.Button("怪物腳本開始")) EnemySpawnMgr.StartSpawnEnemy();
        if (GUILayout.Button("讓怪物全死亡")) EnemySpawnMgr.MonsterAllDie();
        if (GUILayout.Button("切換控制")) HeroMgr.CreateHero();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) LookAtNearestMonster();

        //if (Input.GetKeyDown(KeyCode.Alpha1)) EnemySpawnMgr.SpawnEnemy();
        /*if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            var point = MapMgr.TargetTs.position + MathUtility.GetRandomRadiusPoint(4f);

            Debug.DrawLine(MapMgr.TargetTs.position, point, Color.red, 1f);
        }*/
    }

    /*void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        //Gizmos.DrawWireSphere(mTestPos, 1f);
    }*/

    #endregion


    public enum State
    {
        Ready,
        Start,
        End
    }
}
