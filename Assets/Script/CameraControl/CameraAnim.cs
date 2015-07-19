using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class CameraAnim : MonoBehaviour
{
    private static readonly int HASH_INTRO = Animator.StringToHash("Intro");

    [SerializeField]
    private Animator m_Ani = null;

    private ScreenOverlay mScreenOverlay = null;

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
        m_Ani.SetTrigger(HASH_INTRO);
    }

    private void OnSpawnMonster()
    {
        EnemySpawnManager.Instance.SpawnIntroEnemy();
    }

    private void OnStartGame()
    {
        m_Ani.enabled = false;
        GameManager.Instance.StartGame();
    }
}
