using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//using TMPro;

public class Settings : Singleton<Settings>
{
    private bool isWebGL;
    [SerializeField] public GameObject canvas;
    [SerializeField] private Slider[] sliders;

    //[SerializeField] private Toggle toggle;
    //[SerializeField] private TMP_Text versionTxt;
    private void Start()
    {
        GetSettings();
        //PlayerPref get then set all values for audio
        //versionTxt.text = Application.version;
    }

    private void SetDisplayUI(bool UIToggle)
    {
        PlayerPrefs.SetInt("UIToggle", UIToggle ? 1 : 0);
    }

    public void ResetSettings()
    {
        SetMastVol(0);
        SetMusicVol(0);
        SetSfxVol(0);
        SetUIVol(0);
        SetDisplayUI(true);
    }

    private void GetSettings()
    {
        audioMixer.SetFloat("Master", PlayerPrefs.GetFloat("Master"));
        audioMixer.SetFloat("Music", PlayerPrefs.GetFloat("Music"));
        audioMixer.SetFloat("Sfx", PlayerPrefs.GetFloat("Sfx"));
        audioMixer.SetFloat("UI", PlayerPrefs.GetFloat("UI"));
        /*if (PlayerPrefs.GetInt("UIToggle") == 0)
        {
            toggle.isOn = false;
        }
        else { toggle.isOn = true; }*/
    }
    
    #region Audio

    public AudioMixer audioMixer;

    public void SetMastVol(float volume)
    {
        audioMixer.SetFloat("Master", volume);
        PlayerPrefs.SetFloat("Master", volume);
        sliders[1].value = PlayerPrefs.GetFloat("Master");
    }

    public void SetMusicVol(float volume)
    {
        audioMixer.SetFloat("Music", volume);
        PlayerPrefs.SetFloat("Music", volume);
        sliders[2].value = PlayerPrefs.GetFloat("Music");
    }

    public void SetSfxVol(float volume)
    {
        audioMixer.SetFloat("Sfx", volume);
        PlayerPrefs.SetFloat("Sfx", volume);
        sliders[3].value = PlayerPrefs.GetFloat("Sfx");
    }

    public void SetUIVol(float volume)
    {
        audioMixer.SetFloat("UI", volume);
        PlayerPrefs.SetFloat("UI", volume);
        sliders[4].value = PlayerPrefs.GetFloat("UI");
    }

    #endregion
    
    #region Application Settings

    public void GetPlatform()
    {
        isWebGL = Application.platform == RuntimePlatform.WebGLPlayer;
    }

    public void SetResolution()
    {
        if(isWebGL)
        {
            Screen.SetResolution(1280, 720, true);
        }
        else
        {
            Screen.SetResolution(1920, 1080, true);
        }
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void ResetLevel()
    {
        PlayerPrefs.Save();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SwitchScene(string sceneName)
    {
        PlayerPrefs.Save();
        SceneManager.LoadScene(sceneName);
    }

    public void CloseGame()
    {
        PlayerPrefs.Save();
        Application.Quit();
    }

    #endregion
}
