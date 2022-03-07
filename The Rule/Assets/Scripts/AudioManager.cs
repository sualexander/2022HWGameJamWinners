using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static AudioManager instance;
    void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    public void PlayAudio(AudioClip ac)
    {
        StartCoroutine(playAudioCoroutine(ac));
    }

    IEnumerator playAudioCoroutine(AudioClip ac)
    {
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = ac;
        Debug.Log(LawManager.instance && LawManager.instance.GetLaw() == LawManager.Law.COVER_EARS);
        if (LawManager.instance && LawManager.instance.GetLaw() == LawManager.Law.COVER_EARS) audioSource.volume = 0.005f;
        audioSource.Play();
        while (audioSource.isPlaying)
        {
            yield return new WaitForEndOfFrame();
        }
        Destroy(audioSource);
    }
}
