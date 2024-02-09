using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioSFX
{
    public string name;
    public AudioClip audioClip;
    //public bool isMusic;
    //public bool isUISFX;
    //public bool randomizePitch;
    public bool playOnAwake = false;
    public bool loop = false;
}

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private List<AudioSFX> m_audioSFXList;

    private List<GameObject> m_audioSourceObjects;

    protected override void Awake()
    {
        base.Awake();
        InitializeAudioSources();
    }

    private void InitializeAudioSources()
    {
        if (m_audioSFXList.Count > 0)
        {
            m_audioSourceObjects = new List<GameObject>();

            foreach (AudioSFX sfx in m_audioSFXList)
            {
                GameObject sfxObject = new GameObject(sfx.name, typeof(AudioSource));
                sfxObject.transform.SetParent(this.transform);

                AudioSource newAudioSource = sfxObject.GetComponent<AudioSource>();
                newAudioSource.clip = sfx.audioClip;
                newAudioSource.playOnAwake = sfx.playOnAwake;
                newAudioSource.loop = sfx.loop;

                m_audioSourceObjects.Add(sfxObject);
            }
        }
    }

    private AudioSource GetAudioSource(string _name)
    {
        foreach (GameObject obj in m_audioSourceObjects)
        {
            if(obj.name == _name)
            {
                return obj.GetComponent<AudioSource>();
            }
        }
        return null;
    }

    public void PlaySound(string _name)
    {
        GetAudioSource(_name).Play();
    }


    public void StopSound(string _name)
    {
        GetAudioSource(_name).Stop();
    }
}
