using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class SceneManager : MonoBehaviour
{
    public const string SCENE_MENU = "MenuScene";
    public const string SCENE_GAME = "game";
    public const string SCENE_STAGE = "Map_";


    public static SceneManager mInstance = null;
    public static SceneManager Instance
    {
        get
        {
            if (mInstance == null) CreateInstance();
            return mInstance;
        }
    }

    private static SceneManager CreateInstance()
    {
        var go = new GameObject("_SceneManager");
        var sceneManager = go.AddComponent<SceneManager>();
        return sceneManager;
    }

    void Awake()
    {
        mInstance = this;

        DontDestroyOnLoad(gameObject);
    }

    void OnDestroy()
    {
        mInstance = null;
    } 

    public static void LoadStage(int index)
    {
        LoadLevelAdditive(string.Format("{0}{1}", SCENE_STAGE, index));
    }

    public static void LoadLevelAdditive(string levelName)
    {
        Instance.StartCoroutine(Instance.InsLoadLevelAdditiveAsync(levelName));
    }

    private IEnumerator InsLoadLevelAdditiveAsync(string levelName)
    {
        Debug.Log("讀取地圖:" + levelName);

        var sync = Application.LoadLevelAdditiveAsync(levelName);

        yield return sync;

        Debug.Log("切換地圖:" + levelName);
    }
}
