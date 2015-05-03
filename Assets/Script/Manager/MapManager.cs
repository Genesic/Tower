using UnityEngine;
using System.Collections;

/// <summary>舞台管理類別</summary>
public class MapManager : MonoBehaviour
{
    public Transform SpawnTs = null;
    public Transform TargetTs = null;
    public Transform MonsterContainerTs = null;
    public Transform EffectContainerTs = null;

    void Awake()
    {
        transform.SetAsLastSibling();

        SetSkybox();

        LevelLoadComplete();
    }

    private void SetSkybox()
    {
        StartCoroutine(AsyncLoadSkybox());
    }

    private IEnumerator AsyncLoadSkybox()
    {
        var request = Resources.LoadAsync<Material>("Materials/Skybox");

        yield return request;

        RenderSettings.skybox = request.asset as Material;
    }

    private void LevelLoadComplete()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.LevelLoadComplete(this);
        }
        else
        {
            Debug.Log("GameManager.Instance == null");
        }
    }
}
