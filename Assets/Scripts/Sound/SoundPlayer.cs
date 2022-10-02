using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundPlayer : MonoBehaviour
{
    [Header("Sound settings")]
    [SerializeField] private List<AudioClip> clips;

    private AudioSource audio;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    public void PlaySound(string name)
    {
        for (int i = 0; i < clips.Count; i++)
        {
            if (clips[i].name == name)
            {
                audio.clip = clips[i];
                audio.Play();
            }
        }
    }

    public void StopSound()
    {
        audio.Stop();
    }
}
