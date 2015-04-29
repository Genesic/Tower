using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource m_AudioSource = null;

    [SerializeField]
    private AudioClip[] m_AudioClips = null;

    private Coroutine mCoroutine = null;

    private int mIndex = 0;

    void Awake()
    {
        PlaySound();
    }

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
        Debug.Log(mIndex);
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
}
