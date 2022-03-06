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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAudio(AudioClip ac)
    {
        StartCoroutine(playAudioCoroutine(ac));
    }

    IEnumerator playAudioCoroutine(AudioClip ac)
    {
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = ac;
        audioSource.Play();
        while (audioSource.isPlaying)
        {
            yield return new WaitForEndOfFrame();
        }
        Destroy(audioSource);
    }
}
