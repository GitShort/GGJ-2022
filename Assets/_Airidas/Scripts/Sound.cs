using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    [SerializeField] string name;

    [SerializeField] AudioClip clip;
    [SerializeField] AudioMixerGroup audioMixer;

    [Range(0f, 1f)]
    [SerializeField] float volume = 1f;
    [Range(.1f, 3f)]
    [SerializeField] float pitch = 1f;

    [SerializeField] bool loop;

    [Range(0f, 1f)]
    [SerializeField] float spatialBlend = 1f;

    [SerializeField] float maxSoundDistance = 100f;
    //[SerializeField] AudioRolloffMode rolloffMode = AudioRolloffMode.Custom;


    [HideInInspector]
    public AudioSource source;

    public string SoundName
    {
        get { return name; }
    }

    public AudioClip SoundClip
    {
        get { return clip; }
    }

    public AudioMixerGroup SoundAudioMixer
    {
        get { return audioMixer; }
    }

    public float SoundVolume
    {
        get { return volume; }
    }

    public float SoundPitch
    {
        get { return pitch; }
    }

    public bool SoundLoop
    {
        get { return loop; }
    }

    public float SoundSpatialBlend
    {
        get { return spatialBlend; }
    }

    public float SoundMaxDistance
    {
        get { return maxSoundDistance; }
    }

}
