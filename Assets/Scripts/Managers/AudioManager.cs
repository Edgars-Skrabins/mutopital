using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[System.Serializable]
public class AudioSFX
{
    public string name;
    public AudioClip audioClip;
    [Range(0,1)]public float volume = 0.8f;
    public bool isMusic;
    public bool isUISFX;
    public bool randomizePitch;
    public bool playOnAwake = false;
    public bool loop = false;
}

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private List<AudioSFX> m_audioSFXList;
    [SerializeField] private AudioMixer m_mixer;
    [SerializeField] private AudioMixerGroup m_musicMixer, m_sfxMixer, m_uiMixer;
    [SerializeField] private Slider[] m_masterSlider, m_musicSlider, m_sfxSlider, m_uiSlider;
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
                newAudioSource.volume = sfx.volume;
                newAudioSource.clip = sfx.audioClip;
                if (sfx.playOnAwake)
                {
                    newAudioSource.playOnAwake = sfx.playOnAwake;
                    newAudioSource.Play();
                }
                newAudioSource.loop = sfx.loop;

                if(sfx.isMusic)
                {
                    newAudioSource.outputAudioMixerGroup = m_musicMixer;
                }
                else if (sfx.isUISFX)
                {
                    newAudioSource.outputAudioMixerGroup = m_uiMixer;
                }
                else
                {
                    newAudioSource.outputAudioMixerGroup = m_sfxMixer;
                }

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

    private bool IsRandomized(string _name)
    {
        foreach (AudioSFX sfx in m_audioSFXList)
        {
            if(sfx.name == _name)
            {
                return sfx.randomizePitch;
            }
        }

        return false;
    }

    public void PlaySound(string _name)
    {
        AudioSource source = GetAudioSource(_name);

        if(IsRandomized(_name))
        {
            source.pitch = Random.Range(0f, 0.5f);
        }

        if (source && !source.isPlaying)
        {
            source.Play();
        }
    }

    public void StopSound(string _name)
    {
        AudioSource source = GetAudioSource(_name);

        if (source && !source.isPlaying)
        {
            source.Stop();
        }
    }

    public void ResetSettings()
    {
        SetMastVol();
        SetMusicVol();
        SetSfxVol();
        SetUIVol();
    }

    private void GetSettings()
    {
        audioMixer.SetFloat("Master", PlayerPrefs.GetFloat("Master"));
        audioMixer.SetFloat("Music", PlayerPrefs.GetFloat("Music"));
        audioMixer.SetFloat("Sfx", PlayerPrefs.GetFloat("SFX"));
        audioMixer.SetFloat("UI", PlayerPrefs.GetFloat("UI"));
    }

    public AudioMixer audioMixer;

    public void SetMastVol()
    {
        float volume = 0f;

        foreach (Slider slider in m_masterSlider)
        {
            if(slider.gameObject.activeInHierarchy)
            {
                volume = slider.value;
            }
        }
        audioMixer.SetFloat("Master", volume);
        PlayerPrefs.SetFloat("Master", volume);
        foreach (var slider in m_masterSlider)
        {
            slider.value = volume;
        }
    }

    public void SetMusicVol()
    {
        float volume = 0f;

        foreach (Slider slider in m_musicSlider)
        {
            if (slider.gameObject.activeInHierarchy)
            {
                volume = slider.value;
            }
        }
        audioMixer.SetFloat("Music", volume);
        PlayerPrefs.SetFloat("Music", volume);
        foreach (var slider in m_musicSlider)
        {
            slider.value = volume;
        }
    }

    public void SetSfxVol()
    {
        float volume = 0f;

        foreach (Slider slider in m_sfxSlider)
        {
            if (slider.gameObject.activeInHierarchy)
            {
                volume = slider.value;
            }
        }
        audioMixer.SetFloat("SFX", volume);
        PlayerPrefs.SetFloat("SFX", volume);
        foreach (var slider in m_sfxSlider)
        {
            slider.value = volume;
        }
    }

    public void SetUIVol()
    {
        float volume = 0f;

        foreach (Slider slider in m_uiSlider)
        {
            if (slider.gameObject.activeInHierarchy)
            {
                volume = slider.value;
            }
        }
        audioMixer.SetFloat("UI", volume);
        PlayerPrefs.SetFloat("UI", volume);
        foreach (var slider in m_uiSlider)
        {
            slider.value = volume;
        }
    }
}
