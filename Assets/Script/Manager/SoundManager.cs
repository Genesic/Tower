using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    #region Unity Event

    void Awake()
    {
        PlaySound();
    }

    #endregion

    #region Music

    [SerializeField]
    private AudioSource m_AudioSource = null;

    [SerializeField]
    private AudioClip[] m_AudioClips = null;

    private Coroutine mCoroutine = null;

    private int mIndex = 0;

    private void PlaySound()
    {
        mCoroutine = StartCoroutine(CoPlaySound());
    }

    private void StopSound()
    {
        StopCoroutine(mCoroutine);
    }

    private IEnumerator CoPlaySound()
    {
        m_AudioSource.clip = m_AudioClips[mIndex];
        //m_AudioSource.time = 95f;
        m_AudioSource.Play();

        while (m_AudioSource.isPlaying)
        {
            yield return null;
        }

        NextSound();
    }

    private void NextSound()
    {
        int count = m_AudioClips.Length;

        mIndex = (mIndex + 1 + count) % count;

        PlaySound();
    }

    #endregion

    #region Sfx

    private const string SFX_PATH = "Sound/Sfx/{0}";

    public static AudioClip GetAudioClip(string id)
    {
        return Resources.Load<AudioClip>(string.Format(SFX_PATH, id));
    }

    [ContextMenu("PlaySound")]
    public void Test_PlaySound()
    {
        PlaySound();
    }

    #endregion

}
