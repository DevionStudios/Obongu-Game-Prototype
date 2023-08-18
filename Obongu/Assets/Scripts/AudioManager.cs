using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] private Sound[] sounds;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this);
    }
    private void Start()
    {
        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            if (s.name == "theme")
                s.source.Play();
        }
    }
    public void PlaySound(string name, bool shouldOverwriteIsPlaying = true)
    {
        Sound s = System.Array.Find(sounds, sound => sound.name == name);
        if (s == null)
            return;
        if(!s.source.isPlaying || shouldOverwriteIsPlaying)
            s.source.Play();
    }
    public void PlayOneShot(string name, bool shouldOverwriteIsPlaying = true)
    {
        Sound s = System.Array.Find(sounds, sound => sound.name == name);
        if(!s.source.isPlaying || shouldOverwriteIsPlaying)
            s.source.PlayOneShot(s.clip);
    }

    // default it is used to change background themes
    public void ChangeAudioClip(AudioClip clip, string name="theme")
    {
        Sound s = System.Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
        s.source.clip = clip;
        s.source.Play();
    }
}
