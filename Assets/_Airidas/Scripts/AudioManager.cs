using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
public class AudioManager : MonoBehaviour
{
    [SerializeField] Sound[] Sounds;

    List<AudioSource> audioList = new List<AudioSource>();
    int index = 0;

    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
    }
    
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Update()
    {
        if (index < audioList.Count)
        {
            
            AudioSource audioRef = audioList[index];
            if (!audioRef.isPlaying)
            {
                audioList.Remove(audioRef);
                Destroy(audioRef);
            }
            index++;
        }
        else
        {
            index = 0;
        }
    }
    
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        for (int i = audioList.Count-1; i > 0; i--)
        {
            if (audioList[i] == null)
            {
                audioList.Remove(audioList[i]);
            }
        }

        if (index >= audioList.Count && audioList.Count > 0)
        {
            index = audioList.Count - 1;
        }
        else if (index >= audioList.Count && audioList.Count == 0)
        {
            index = 0;
        }
    }


    public void PlaySound(string name, GameObject go)
    {
        Sound s = Array.Find(Sounds, sound => sound.SoundName == name);
        AudioSource temp = null;
        if (s != null)
            temp = audioList.Find(source => source.clip == s.SoundClip);
        if (s != null && (temp == null || temp.gameObject != go))
        {
            AudioSource source = go.AddComponent<AudioSource>();
            audioList.Add(source);
            source.clip = s.SoundClip;
            source.outputAudioMixerGroup = s.SoundAudioMixer;
            source.volume = s.SoundVolume;
            source.pitch = s.SoundPitch;
            source.loop = s.SoundLoop;
            source.spatialBlend = s.SoundSpatialBlend;
            source.maxDistance = s.SoundMaxDistance;
            source.Play();
        }
    }

    public void StopSound(string name, GameObject go)
    {
        Sound s = Array.Find(Sounds, sound => sound.SoundName == name);
        AudioSource temp = null;
        if (s != null)
        {
            temp = audioList.Find(source => source.clip == s.SoundClip);
        }
        if (temp != null && temp.isPlaying)
        {
            temp.Stop();
        }
    }

    public void SetVolume(string name, float value)
    {
        Sound s = Array.Find(Sounds, sound => sound.SoundName == name);
        AudioSource audioCheck = null;
        if (s != null)
        {
            audioCheck = audioList.Find(source => source.clip == s.SoundClip);
        }
        if (audioCheck != null && audioCheck.isPlaying)
        {
            audioCheck.volume = value;
        }
    }
}
