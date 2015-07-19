using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class CameraAnim : MonoBehaviour
{
    private static readonly int HASH_INTRO = Animator.StringToHash("Intro");
    private static readonly int HASH_SKIP_INTRO = Animator.StringToHash("SkipIntro");
    
    [SerializeField]
    private Animator m_Ani = null;

    private ScreenOverlay mScreenOverlay = null;

    private bool mIsAnimComplete = false;

    void Awake()
    {
        mScreenOverlay = GetComponentInChildren<ScreenOverlay>();

        SetScreenBlack();
    }

    private void SetScreenBlack()
    {
        mScreenOverlay.intensity = 0f;
    }

    public void PlayIntroAnim()
    {
        if (mIsAnimComplete)
            return;

        m_Ani.SetTrigger(HASH_INTRO);
    }

    private void OnSpawnMonster()
    {
        EnemySpawnManager.Instance.SpawnIntroEnemy();
    }

    public void SkipIntroAnim()
    {
        if (mIsAnimComplete)
            return;

        mIsAnimComplete = true;

        if (EnemySpawnManager.Instance.MonsterAliveNum == 0)
            EnemySpawnManager.Instance.SpawnIntroEnemy();

        m_Ani.SetTrigger(HASH_SKIP_INTRO);
    }
    private void OnStartGame()
    {
        m_Ani.enabled = false;

        GameManager.Instance.StartGame();
    }

}
