using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizedAudio : MonoBehaviour
{
    [SerializeField] AudioClip[] audioClips;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayRandomAudio()
    {
        int r = Random.Range(0, audioClips.Length);
        AudioManager.instance.PlayAudio(audioClips[r]);
        Debug.Log("Playing " + audioClips[r].name);
    }
}
