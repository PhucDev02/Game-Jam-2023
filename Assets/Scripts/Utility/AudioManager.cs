using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    void Awake()
    {
        Instance = this;
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = 1;
            s.source.loop = s.loop;
        }
        musicOn = PlayerPrefs.GetInt("Music", 1) == 1;
        soundOn = PlayerPrefs.GetInt("Sound", 1) == 1;
    }

    public Sound[] sounds;

    public bool musicOn;
    public bool soundOn;

    public void Play(string name)
    {
        

        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        if (!soundOn && !s.isMusic) return;
        if (!musicOn && !s.isMusic) return;

        s.source.Play();
    }
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Stop();
    }
    public void ClickSound()
    {
        Play("ButtonClick");
    }

    public void ToggleMusic()
    {
        musicOn = !musicOn;
        if (musicOn)
        {
            Play("BackgroundMusic");
        }
        else
        {
            StopAllMusic();
        }
        PlayerPrefs.SetInt("Music", musicOn ? 1 : 0);
    }

    public void ToggleSound()
    {
        soundOn = !soundOn;
        if (!soundOn) StopAllSound();
        PlayerPrefs.SetInt("Sound", musicOn ? 1 : 0);
    }

    public void StopAllSound()
    {
        foreach (var s in sounds)
        {
            if (!s.isMusic)
            {
                s.source.Stop();
            }
        }
    }

    public void StopAllMusic()
    {
        foreach (var s in sounds)
        {
            if (s.isMusic)
            {
                s.source.Stop();
            }
        }
    }

}