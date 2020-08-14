using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private Track[] sounds = null;

    [SerializeField]
    private Track[] musics = null;

    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
        else 
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        foreach (Track s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.volume = GameData.instance.IsSound ? s.volume : 0f;
            s.source.clip = s.clip;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

        foreach (Track m in musics)
        {
            m.source = gameObject.AddComponent<AudioSource>();
            m.source.volume = GameData.instance.IsMusic ? m.volume : 0f;
            m.source.clip = m.clip;
            m.source.pitch = m.pitch;
            m.source.loop = m.loop;
        }
    }

    public void PlaySound(string name)
    {
        Track s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.Play();
    }

    public void PlayMusic(string name)
    {
        Track m = Array.Find(musics, music => music.name == name);
        if (m == null)
        {
            Debug.LogWarning("Music: " + name + " not found!");
            return;
        }

        m.source.Play();
    }

    public void StopMusic(string name)
    {
        Track m = Array.Find(musics, music => music.name == name);
        if (m == null)
        {
            Debug.LogWarning("Music: " + name + " not found!");
            return;
        }

        m.source.Stop();
    }

    public void OffVolumeSounds(bool isVolume)
    {
        foreach (Track s in sounds)
        {
            s.source.volume = isVolume ? s.volume : 0f;
        }
    }

    public void OffVolumeMusic(bool isVolume)
    {
        foreach (Track m in musics)
        {
            m.source.volume = isVolume ? m.volume : 0f;
        }
    }
}